using QuestPDF.Fluent;
using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using System.Text;

namespace Services.Implementations.CVGenerationModule.Templates
{
    public class ModernTemplateCV : ICVTemplate
    {
        public string TemplateName => "modern";

        public byte[] GeneratePdf(CVResponseDTO cv)
        {
            return new Pdf.ModernPdfDocumentCV(cv).GeneratePdf();
        }

        public string GenerateHtml(CVResponseDTO cv)
        {
            var sb = new StringBuilder();

            // 1. Header & CSS
            sb.Append($@"
<!doctype html>
<html dir='rtl' lang='ar'>
<head>
    <meta charset='UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <link href='https://fonts.googleapis.com/css2?family=Cairo:wght@400;600;700;800;900&display=swap' rel='stylesheet' />
    <style>
        * {{ box-sizing: border-box; margin: 0; padding: 0; }}
        body {{ font-family: 'Cairo', sans-serif; background: #f8fafc; padding: 20px; direction: rtl; }}
        .cv-wrap {{ background: #fff; max-width: 960px; margin: 0 auto; box-shadow: 0 4px 32px rgba(25,53,90,0.1); border-radius: 8px; overflow: hidden; }}
        .cv-header {{ background: #19355a; padding: 40px; color: #fff; }}
        .cv-header .name {{ font-size: 2.2rem; font-weight: 800; margin-bottom: 4px; }}
        .cv-header .job-title {{ color: #b38e19; font-weight: 600; font-size: 1.1rem; margin-bottom: 8px; }}
        .cv-header .meta-row {{ display: flex; flex-wrap: wrap; gap: 8px; font-size: 0.9rem; color: #cbd5e1; }}
        .cv-header .meta-row span {{ display: inline-block; }}
        .cv-header .meta-row .dot {{ opacity: 0.5; }}
        .cv-body {{ display: flex; flex-wrap: wrap; }}
        .cv-main {{ flex: 1 1 60%; min-width: 280px; padding: 28px; border-left: 1px solid #e2e8f0; }}
        .cv-sidebar {{ flex: 0 0 32%; background: #f0f4f8; padding: 28px; }}
        .section-title {{ border-right: 4px solid #b38e19; padding-right: 10px; margin: 22px 0 10px 0; }}
        .section-title span {{ color: #19355a; font-weight: 700; font-size: 0.95rem; text-transform: uppercase; letter-spacing: 0.06em; }}
        .bio-text {{ font-size: 0.88rem; color: #374151; line-height: 1.7; margin-bottom: 14px; }}
        .entry {{ border-bottom: 1px solid #e2e8f0; padding-bottom: 10px; margin-bottom: 10px; font-size: 0.88rem; color: #1e293b; line-height: 1.6; }}
        .entry-title {{ font-weight: 700; color: #19355a; margin-bottom: 2px; }}
        .entry-meta {{ color: #64748b; font-size: 0.8rem; }}
        .contact-row {{ font-size: 0.83rem; margin-bottom: 5px; }}
        .contact-label {{ color: #64748b; font-weight: bold; }}
        .skills-wrap {{ display: flex; flex-wrap: wrap; gap: 4px; margin-top: 4px; }}
        .skill-pill {{ display: inline-block; background: #fff; color: #19355a; border-radius: 9999px; padding: 2px 12px; font-size: 0.78rem; font-weight: 600; border: 1px solid #c9d8e8; }}
        .social-row {{ font-size: 0.78rem; margin-bottom: 5px; word-break: break-all; }}
        .social-label {{ color: #b38e19; font-weight: 700; }}
    </style>
</head>
<body>
    <div class='cv-wrap'>
        <div class='cv-header'>
            <div class='name'>{cv.Name}</div>
            {(cv.Title != null ? $"<div class='job-title'>{cv.Title.ValueAr}</div>" : "")}
            <div class='meta-row'>
                {(cv.Department != null ? $"<span>{cv.Department.ValueAr}</span>" : "")}
                {(cv.Department != null && cv.University != null ? "<span class='dot'>·</span>" : "")}
                {(cv.University != null ? $"<span>{cv.University.ValueAr}</span>" : "")}
                {(cv.Authority != null ? $"<span class='dot'>·</span><span>{cv.Authority.ValueAr}</span>" : "")}
                {(cv.BirthDate.HasValue ? $"<span class='dot'>·</span><span>تاريخ الميلاد: {cv.BirthDate.Value:yyyy/MM/dd}</span>" : "")}
            </div>
        </div>

        <div class='cv-body'>
            <div class='cv-main'>");

            // --- BIO ---
            if (!string.IsNullOrEmpty(cv.BioSummary))
            {
                sb.Append($"<div class='section-title'><span>نبذة تعريفية</span></div><p class='bio-text'>{cv.BioSummary}</p>");
            }

            // --- ACADEMIC QUALIFICATIONS ---
            if (cv.AcademicQualifications?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المؤهلات العلمية</span></div>");
                foreach (var aq in cv.AcademicQualifications)
                {
                    sb.Append($@"<div class='entry'>
                        <div class='entry-title'>{aq.Qualification?.ValueAr}{(string.IsNullOrEmpty(aq.Specialization) ? "" : " — " + aq.Specialization)}</div>
                        <div class='entry-meta'>{aq.Grade?.ValueAr} · {aq.UniversityOrFaculty} · {aq.CountryOrCity} · {aq.DateOfObtainingTheQualification:yyyy/MM/dd}</div>
                    </div>");
                }
            }

            // --- JOB RANKS ---
            if (cv.JobRanks?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الدرجات الوظيفية</span></div>");
                foreach (var jr in cv.JobRanks)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{jr.JobRank?.ValueAr}</div><div class='entry-meta'>{jr.DateOfJobRank:yyyy/MM/dd}</div></div>");
                }
            }

            // --- ADMINISTRATIVE POSITIONS ---
            if (cv.AdministrativePositions?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المناصب الإدارية</span></div>");
                foreach (var ap in cv.AdministrativePositions)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ap.Position}</div><div class='entry-meta'>{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- GENERAL EXPERIENCES ---
            if (cv.GeneralExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الخبرات العامة</span></div>");
                foreach (var ge in cv.GeneralExperiences)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ge.ExperienceTitle}</div><div class='entry-meta'>{ge.Authority} · {ge.CountryOrCity} · {ge.StartDate:yyyy/MM/dd} – {ge.EndDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- TEACHING EXPERIENCES ---
            if (cv.TeachingExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الخبرات التدريسية</span></div>");
                foreach (var te in cv.TeachingExperiences)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{te.CourseName}</div><div class='entry-meta'>{te.AcademicLevel} · {te.UniversityOrFaculty} · {te.StartDate:yyyy/MM/dd} – {te.EndDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- CONFERENCES AND SEMINARS ---
            if (cv.ConferencesAndSeminars?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المؤتمرات والندوات</span></div>");
                foreach (var cs in cv.ConferencesAndSeminars)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{cs.Name}</div><div class='entry-meta'>{cs.RoleOfParticipation?.ValueAr} · {cs.OrganizingAuthority} · {cs.Venue} · {cs.StartDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- SCIENTIFIC WRITINGS (Books) ---
            if (cv.ScientificWritings?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المؤلفات العلمية</span></div>");
                foreach (var sw in cv.ScientificWritings)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{sw.Title}</div><div class='entry-meta'>{sw.AuthorRole?.ValueAr} · {sw.PublishingHouse} · ISBN: {sw.ISBN} · {sw.PublishingDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- PROJECTS ---
            if (cv.Projects?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المشاريع</span></div>");
                foreach (var p in cv.Projects)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{p.NameOfProject}</div><div class='entry-meta'>{p.TypeOfProject?.ValueAr} · {p.ParticipationRole?.ValueAr} · {p.StartDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- PRIZES AND REWARDS ---
            if (cv.PrizesAndRewards?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الجوائز والمكافآت</span></div>");
                foreach (var pr in cv.PrizesAndRewards)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{pr.Prize}</div><div class='entry-meta'>{pr.AwardingAuthority} · {pr.DateReceived:yyyy/MM/dd}</div></div>");
                }
            }

            // (يمكنك إضافة بقية الأقسام مثل براءات الاختراع واللجان بنفس النمط هنا)

            sb.Append("</div>");

            // SIDEBAR
            sb.Append("<div class='cv-sidebar'>");

            // --- CONTACT ---
            sb.Append("<div class='section-title'><span>بيانات الاتصال</span></div>");
            if (!string.IsNullOrEmpty(cv.OfficialEmail)) sb.Append($"<div class='contact-row'><span class='contact-label'>البريد: </span>{cv.OfficialEmail}</div>");
            if (!string.IsNullOrEmpty(cv.MainPhoneNumber)) sb.Append($"<div class='contact-row'><span class='contact-label'>الهاتف: </span>{cv.MainPhoneNumber}</div>");
            if (!string.IsNullOrEmpty(cv.WorkPhoneNumber)) sb.Append($"<div class='contact-row'><span class='contact-label'>هاتف العمل: </span>{cv.WorkPhoneNumber}</div>");

            // --- SKILLS ---
            if (cv.Skills?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المهارات</span></div><div class='skills-wrap'>");
                foreach (var s in cv.Skills) sb.Append($"<span class='skill-pill'>{s}</span>");
                sb.Append("</div>");
            }

            // --- SOCIAL ---
            sb.Append("<div class='section-title'><span>التواصل الاجتماعي</span></div>");
            if (!string.IsNullOrEmpty(cv.LinkedIn)) sb.Append($"<div class='social-row'><span class='social-label'>LinkedIn: </span>{cv.LinkedIn}</div>");
            if (!string.IsNullOrEmpty(cv.GoogleScholar)) sb.Append($"<div class='social-row'><span class='social-label'>Scholar: </span>{cv.GoogleScholar}</div>");
            if (!string.IsNullOrEmpty(cv.PersonalWebsite)) sb.Append($"<div class='social-row'><span class='social-label'>الموقع: </span>{cv.PersonalWebsite}</div>");

            sb.Append(@"</div></div></div></body>
            </html>");

            return sb.ToString();
        }
    }
}