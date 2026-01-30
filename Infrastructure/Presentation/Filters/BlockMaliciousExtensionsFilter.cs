using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace Presentation.Filters
{
    public sealed class BlockMaliciousExtensionsFilter : IActionFilter
    {
        // Prefer whitelist (safe) not blacklist.
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp",
        ".pdf",
        ".docx", ".xlsx",
        ".txt"
    };

        private static readonly HashSet<string> BlockedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".exe", ".dll", ".bat", ".cmd", ".com", ".msi", ".ps1", ".vbs", ".js", ".jar",
        ".scr", ".pif", ".cpl", ".hta", ".wsf", ".lnk",
        ".sh", ".php", ".asp", ".aspx", ".jsp", ".py", ".rb",
        ".zip", ".rar", ".7z"
    };

        private static readonly Dictionary<string, string[]> AllowedMimeByExt = new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = new[] { "image/jpeg" },
            [".jpeg"] = new[] { "image/jpeg" },
            [".png"] = new[] { "image/png" },
            [".gif"] = new[] { "image/gif" },
            [".webp"] = new[] { "image/webp" },
            [".pdf"] = new[] { "application/pdf" },
            [".txt"] = new[] { "text/plain" },
            [".docx"] = new[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            [".xlsx"] = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        };

        private static readonly Regex DoubleExtensionRegex =
            new(@"\.(exe|dll|bat|cmd|com|msi|ps1|vbs|js|jar|scr|pif|cpl|hta|wsf|lnk|sh|php|asp|aspx|jsp|py|rb)$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private const long MaxBytes = 10 * 1024 * 1024; // 10 MB

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg is IFormFile file)
                {
                    if (!TryValidateFile(file, out var error))
                    {
                        context.Result = new BadRequestObjectResult(new { Message = error });
                        return;
                    }
                }
                else if (arg is IEnumerable<IFormFile> files)
                {
                    foreach (var f in files)
                    {
                        if (!TryValidateFile(f, out var error))
                        {
                            context.Result = new BadRequestObjectResult(new { Message = error });
                            return;
                        }
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static bool TryValidateFile(IFormFile file, out string error)
        {
            error = string.Empty;

            if (file is null)
            {
                error = "File is required.";
                return false;
            }

            if (file.Length <= 0)
            {
                error = "Empty file is not allowed.";
                return false;
            }

            if (file.Length > MaxBytes)
            {
                error = $"File too large. Max {MaxBytes} bytes.";
                return false;
            }

            var safeName = Path.GetFileName(file.FileName ?? "");

            if (string.IsNullOrWhiteSpace(safeName))
            {
                error = "Invalid file name.";
                return false;
            }

            if (safeName.Contains("..", StringComparison.Ordinal))
            {
                error = "Invalid file name.";
                return false;
            }

            // If last extension is dangerous: report.pdf.exe
            if (DoubleExtensionRegex.IsMatch(safeName))
            {
                error = "Double-extension files are not allowed.";
                return false;
            }

            // Check all extensions in the name: evil.exe.pdf (still contains .exe)
            var parts = safeName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                error = "File extension is missing.";
                return false;
            }

            var lastExt = "." + parts[^1];

            // strict whitelist for the final extension
            if (!AllowedExtensions.Contains(lastExt))
            {
                error = $"Extension '{lastExt}' is not allowed.";
                return false;
            }

            // block any risky extension anywhere in the name
            foreach (var p in parts.Skip(1))
            {
                var dotExt = "." + p;
                if (BlockedExtensions.Contains(dotExt))
                {
                    error = $"Malicious extension detected: '{dotExt}'.";
                    return false;
                }
            }

            // Content-Type check (helpful but not sufficient alone)
            if (AllowedMimeByExt.TryGetValue(lastExt, out var allowedMimes))
            {
                var mime = (file.ContentType ?? "").Trim();
                if (!allowedMimes.Contains(mime, StringComparer.OrdinalIgnoreCase))
                {
                    error = $"MIME type '{mime}' does not match extension '{lastExt}'.";
                    return false;
                }
            }

            // Magic-bytes signature check
            if (!LooksLikeExpectedFileSignature(file, lastExt))
            {
                error = "File signature does not match the declared type.";
                return false;
            }

            return true;
        }

        private static bool LooksLikeExpectedFileSignature(IFormFile file, string ext)
        {
            Span<byte> header = stackalloc byte[12];

            using var stream = file.OpenReadStream();
            var read = stream.Read(header);
            if (read < 4) return false;

            static bool StartsWith(ReadOnlySpan<byte> buffer, int readCount, params byte[] bytes)
            {
                if (readCount < bytes.Length) return false;
                for (int i = 0; i < bytes.Length; i++)
                    if (buffer[i] != bytes[i]) return false;
                return true;
            }

            return ext.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => StartsWith(header, read, 0xFF, 0xD8, 0xFF),
                ".png" => StartsWith(header, read, 0x89, 0x50, 0x4E, 0x47),
                ".gif" => StartsWith(header, read, 0x47, 0x49, 0x46, 0x38),
                ".pdf" => StartsWith(header, read, 0x25, 0x50, 0x44, 0x46), // %PDF
                ".docx" or ".xlsx" => StartsWith(header, read, 0x50, 0x4B),            // PK (zip)
                ".txt" => true,                                             // can't reliably signature-check
                _ => true
            };
        }
    }
}
