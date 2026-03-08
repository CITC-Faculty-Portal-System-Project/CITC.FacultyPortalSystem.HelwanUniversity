using Integrations.Exceptions;
using Integrations.HttpClientFactory;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService;
using System.Net;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace Integrations.Services
{
    public class ResearchesDOIandORCIDLoadService(IGenericHTTPClient _hTTPClient 
        , IConfiguration _configuration) : IResearchesDOIandORCIDLoadService
    {
        private string DoiGetEndPointUrl = _configuration["ResearchesExternalURLS:DOI"]!;
        private string OpenAlexLink = _configuration["ResearchesExternalURLS:OpenAlex"]!;


        #region Helpers

        private static string CleanDoi(string raw)
        {
            raw = (raw ?? "").Trim();

            raw = Regex.Replace(raw, @"^https?://(dx\.)?doi\.org/", "", RegexOptions.IgnoreCase);
            raw = Regex.Replace(raw, @"^doi:\s*", "", RegexOptions.IgnoreCase);

            return raw.Trim();
        }

        private static int? ExtractYear(CrossrefMessageDTO msg)
            => GetYear(msg.PublishedPrint)
               ?? GetYear(msg.PublishedOnline)
               ?? GetYear(msg.Issued)
               ?? GetYear(msg.Created);

        private static int? GetYear(CrossrefDatePartsDTO? d)
        {
            var y = d?.DateParts?.FirstOrDefault()?.FirstOrDefault();
            return y is > 0 ? y : null;
        }

        public static string? CleanAbstract(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return raw;

            var noTags = Regex.Replace(raw, "<.*?>", string.Empty, RegexOptions.Singleline);
            noTags = WebUtility.HtmlDecode(noTags).Replace("\u00A0", " ");
            noTags = Regex.Replace(noTags, @"\s+", " ").Trim();

            return noTags;
        }

        private string CleanOrcid(string orcid)
        {
            return orcid
                .Replace("https://orcid.org/", "", StringComparison.OrdinalIgnoreCase)
                .Trim();
        }

        public static string ExtractAuthorFullName(CrossrefAuthorDTO coAuthor)
        {
            var fullName = $"{coAuthor.Given} {coAuthor.Family}".Trim();
            if (!string.IsNullOrWhiteSpace(fullName)) return fullName;
            return (coAuthor.Name ?? "").Trim();   
        }

        public static string? ExtractReferenceTitle(string? unstructured)
        {
            if (string.IsNullOrWhiteSpace(unstructured))
                return null;

            var text = unstructured.Trim();

            var parts = text.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (parts.Length >= 2)
                return parts[1];   

            return parts[0];      
        } 
        
        #endregion


        public async Task<DOIResponseDTO> GetByDoiAsync(string doi, CancellationToken ct = default)
        {
            doi = CleanDoi(doi);

            var link = $"{DoiGetEndPointUrl}{doi}";
            var res = await _hTTPClient.GetAsync<CrossrefEnvelopeDTO>(
                url: link,
                headers: new Dictionary<string, string>
                {
                    ["User-Agent"] = 
                    $"helwan-teaching-heads-portal-system {_configuration["SmtpSettings:UserName"]}"
                },
                ct: ct
            );

            if (!res.IsSuccess || res.Data?.Message is null)
                throw new IntegrationNotFoundException ("An Error Ocurred maybe there is no research with this doi!");

            var msg = res.Data.Message;

            var authors = new List<CrossrefAuthorDTO>();
            foreach (var author in msg.Author!)
            {
                author.Name = ExtractAuthorFullName(author);
                authors.Add(author);
            }

            var title = msg.Title?.FirstOrDefault() ?? "";
            var journal = msg.ContainerTitle?.FirstOrDefault() ?? "";
            var year = ExtractYear(msg);

            var cleaned = msg.DOI ?? doi;
            var doiUrl = $"https://doi.org/{cleaned}";
            var primaryUrl = msg.URL ?? doiUrl;

            return new DOIResponseDTO
            {
                doi = cleaned,
                doi_url = doiUrl,
                url = primaryUrl,
                title = title,
                authors = authors,
                journal = journal,
                publisher = msg.Publisher,
                type = msg.Type,
                year = year,
                volume = msg.Volume,
                issue = msg.Issue,
                pages = msg.Page,
                RelatedResearchLink = ExtractReferenceTitle(msg.Reference!.FirstOrDefault()!.Unstructured),
                Abstract = CleanAbstract(msg.Abstract),
            };
        }

        public async Task<ResearcherDataGetByORCIDResponseDTO?> GetContributorNameByORCIDAsync(string orcid, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(orcid))
                throw new IntegrationNotFoundException("ORCID is required.");

            orcid = CleanOrcid(orcid);

            var link = $"{OpenAlexLink}{orcid}";

            var res = await _hTTPClient.GetAsync<OpenAlexAuthorEnvelopeDTO>(
                url: link,
                headers: new Dictionary<string, string>
                {
                    ["User-Agent"] =
                    $"helwan-teaching-heads-portal-system {_configuration["SmtpSettings:UserName"]}"
                },
                ct: ct
            );

            if (!res.IsSuccess || res.Data?.Results is null || !res.Data.Results.Any())
                throw new IntegrationNotFoundException("No researcher found with this ORCID.");

            var author = res.Data.Results.First();

            return new ResearcherDataGetByORCIDResponseDTO
            {
                Orcid = orcid,
                OpenAlexId = author.Id,
                Name = author.Display_Name
            };
        }
    }
}
