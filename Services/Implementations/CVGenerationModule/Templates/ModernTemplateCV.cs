using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.CVGenerationModule;
using System.Text;

namespace Services.Implementations.CVGenerationModule.Templates
{
    public class ModernTemplateCV(IAttachmentService _attachmentService , IWebHostEnvironment _env) : ICVTemplate
    {
        public string TemplateName => "modern";

        public byte[] GeneratePdf(CVResponseDTO cv)
        {
            return new Pdf.ModernPdfDocumentCV(cv, _env).GeneratePdf();
        }

        public async Task <string> GenerateHtml(CVResponseDTO cv)
        {
            AttachmentDownloadDTO? profilePicture = null;

            if (cv.ProfilePictureId is not null)
            {
                profilePicture = await _attachmentService.GetAsync(Abstraction.Enums.AttachmentContext.ProfilePicture, cv.PersonalDataId, cv.ProfilePictureId.Value);

            }

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

body {{
    font-family: 'Cairo', sans-serif;
    background: #f8fafc;
    padding: 20px;
    direction: rtl;
}}

.cv-wrap {{
    background: #fff;
    max-width: 960px;
    margin: 0 auto;
    box-shadow: 0 4px 32px rgba(25,53,90,0.1);
    border-radius: 8px;
    overflow: hidden;
}}

/* ================= HEADER ================= */
.cv-header {{
    background: #19355a;
    padding: 40px;
    color: #fff;
}}

.cv-header .name {{
    font-size: 2.2rem;
    font-weight: 800;
}}

.cv-header .job-title {{
    color: #b38e19;
    font-weight: 600;
    font-size: 1.1rem;
}}

.cv-header .meta-row {{
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    font-size: 0.9rem;
    color: #cbd5e1;
}}

/* ================= BODY ================= */
.cv-body {{
    display: flex;
    flex-wrap: wrap;
}}

/* sidebar */
.cv-sidebar {{
    flex: 0 0 32%;
    background: #f0f4f8;
    padding: 28px;
    border-left: 1px solid #e2e8f0;
}}

/* main */
.cv-main {{
    flex: 1 1 60%;
    min-width: 280px;
    padding: 28px;
}}

/* ================= COMMON ================= */
.section-title {{
    border-right: 4px solid #b38e19;
    padding-right: 10px;
    margin: 22px 0 10px 0;
}}

.section-title span {{
    color: #19355a;
    font-weight: 700;
    font-size: 0.95rem;
}}

.bio-text {{
    font-size: 0.88rem;
    color: #374151;
    line-height: 1.7;
}}

.entry {{
    border-bottom: 1px solid #e2e8f0;
    padding-bottom: 10px;
    margin-bottom: 10px;
    font-size: 0.88rem;
}}

.entry-title {{
    font-weight: 700;
    color: #19355a;
}}

.entry-meta {{
    color: #64748b;
    font-size: 0.8rem;
}}

.contact-row {{
    font-size: 0.83rem;
}}

.skills-wrap {{
    display: flex;
    flex-wrap: wrap;
    gap: 4px;
}}

.skill-pill {{
    background: #fff;
    color: #19355a;
    border-radius: 9999px;
    padding: 2px 12px;
    font-size: 0.78rem;
    border: 1px solid #c9d8e8;
}}

.social-row {{
    font-size: 0.78rem;
    word-break: break-all;
}}

/* ================= RESPONSIVE ================= */

/* Tablet */
@media (max-width: 768px) {{
    .cv-header {{
        padding: 24px;
    }}

    .cv-header .name {{
        font-size: 1.6rem;
    }}

    .cv-body {{
        flex-direction: column;
    }}

    .cv-sidebar,
    .cv-main {{
        flex: 1 1 100%;
        width: 100%;
        padding: 20px;
        border-left: none;
    }}

    .cv-sidebar {{
        border-bottom: 1px solid #e2e8f0;
    }}
}}

/* Mobile */
@media (max-width: 480px) {{
    body {{
        padding: 10px;
    }}

    .cv-header {{
        padding: 18px;
    }}

    .cv-header .name {{
        font-size: 1.3rem;
    }}

    .cv-header .job-title {{
        font-size: 0.9rem;
    }}

    .cv-header .meta-row {{
        font-size: 0.75rem;
    }}

    .cv-sidebar,
    .cv-main {{
        padding: 14px;
    }}

    .section-title span {{
        font-size: 0.8rem;
    }}

    .bio-text,
    .entry,
    .contact-row {{
        font-size: 0.75rem;
    }}

    .entry-meta {{
        font-size: 0.7rem;
    }}

    .skill-pill {{
        font-size: 0.65rem;
        padding: 2px 8px;
    }}
}}

/* Very small screens */
@media (max-width: 360px) {{
    .cv-header .name {{
        font-size: 1.1rem;
    }}

    .cv-header {{
        padding: 14px;
    }}

    .cv-sidebar,
    .cv-main {{
        padding: 10px;
    }}

    .section-title {{
        margin: 16px 0 8px;
    }}

    .skill-pill {{
        font-size: 0.6rem;
        padding: 2px 6px;
    }}
}}
    </style>
</head>
<body>
    <div class='cv-wrap'>
        <div class='cv-header'>
            <div class='name'>{cv.NameAr}</div>
            {(cv.Title != null ? $"<div class='job-title'>{cv.Title.ValueAr}</div>" : "")}
            <div class='meta-row'>
                {(cv.Department != null ? $"<span>{cv.Department}</span>" : "")}
                {(cv.Department != null && cv.University != null ? "<span class='dot'>·</span>" : "")}
                {(cv.University != null ? $"<span>{cv.University.ValueAr}</span>" : "")}
                {(cv.Authority != null ? $"<span class='dot'>·</span><span>{cv.Authority.ValueAr}</span>" : "")}
                {(cv.BirthDate.HasValue ? $"<span class='dot'>·</span><span>تاريخ الميلاد: {cv.BirthDate.Value:yyyy/MM/dd}</span>" : "")}
            </div>
        </div>

        <div class='cv-body'>");

            sb.Append("<div class='cv-sidebar'>");
            sb.Append("<div class='section-title'><span>بيانات الاتصال</span></div>");
            if (!string.IsNullOrEmpty(cv.OfficialEmail)) sb.Append($"<div class='contact-row'><span class='contact-label'>البريد: </span>{cv.OfficialEmail}</div>");
            if (!string.IsNullOrEmpty(cv.MainPhoneNumber)) sb.Append($"<div class='contact-row'><span class='contact-label'>الهاتف: </span>{cv.MainPhoneNumber}</div>");
            if (!string.IsNullOrEmpty(cv.WorkPhoneNumber)) sb.Append($"<div class='contact-row'><span class='contact-label'>هاتف العمل: </span>{cv.WorkPhoneNumber}</div>");
            if (!string.IsNullOrEmpty(cv.FaxNumber)) sb.Append($"<div class='contact-row'><span class='contact-label'>الفاكس: </span>{cv.FaxNumber}</div>");

            if (cv.Skills?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المهارات</span></div><div class='skills-wrap'>");
                foreach (var s in cv.Skills) sb.Append($"<span class='skill-pill'>{s}</span>");
                sb.Append("</div>");
            }

            if (cv.PersonalWebsite?.Any() == true || cv.LinkedIn?.Any() == true || cv.GoogleScholar?.Any() == true || cv.Scopus?.Any() == true || cv.YouTube?.Any() == true || cv.Facebook?.Any() == true || cv.Instagram?.Any() == true || cv.X?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>التواصل الاجتماعي</span></div>");
                if (!string.IsNullOrEmpty(cv.PersonalWebsite)) sb.Append($"<div class='social-row'><span class='social-label'>الموقع الشخصي: </span>{cv.PersonalWebsite}</div>");
                if (!string.IsNullOrEmpty(cv.LinkedIn)) sb.Append($"<div class='social-row'><span class='social-label'>LinkedIn: </span>{cv.LinkedIn}</div>");
                if (!string.IsNullOrEmpty(cv.GoogleScholar)) sb.Append($"<div class='social-row'><span class='social-label'>Scholar: </span>{cv.GoogleScholar}</div>");
                if (!string.IsNullOrEmpty(cv.Scopus)) sb.Append($"<div class='social-row'><span class='social-label'>Scopus: </span>{cv.Scopus}</div>");
                if (!string.IsNullOrEmpty(cv.YouTube)) sb.Append($"<div class='social-row'><span class='social-label'>Youtube: </span>{cv.YouTube}</div>");
                if (!string.IsNullOrEmpty(cv.Facebook)) sb.Append($"<div class='social-row'><span class='social-label'>Facebook: </span>{cv.Facebook}</div>");
                if (!string.IsNullOrEmpty(cv.Instagram)) sb.Append($"<div class='social-row'><span class='social-label'>Instagram: </span>{cv.Instagram}</div>");
                if (!string.IsNullOrEmpty(cv.X)) sb.Append($"<div class='social-row'><span class='social-label'>X: </span>{cv.X}</div>");
            }
            sb.Append("</div>"); 

            sb.Append("<div class='cv-main'>");

            // --- BIO ---
            if (!string.IsNullOrEmpty(cv.BioSummary))
            {
                sb.Append($"<div class='section-title'><span>نبذة تعريفية</span></div><p class='bio-text'>{cv.BioSummary}</p>");
            }

            // --- GENERAL EXPERIENCES ---
            if (cv.GeneralExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الخبرات العامة</span></div>");
                foreach (var ge in cv.GeneralExperiences)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ge.ExperienceTitle}</div><div class='entry-meta'>{ge.Authority} · {ge.CountryOrCity} · {ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- TEACHING EXPERIENCES ---
            if (cv.TeachingExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الخبرات التدريسية</span></div>");
                foreach (var te in cv.TeachingExperiences)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{te.CourseName}</div><div class='entry-meta'>{te.AcademicLevel} · {te.UniversityOrFaculty} · {te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
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

            // --- CONFERENCES AND SEMINARS ---
            if (cv.ConferencesAndSeminars?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المؤتمرات والندوات</span></div>");
                foreach (var cs in cv.ConferencesAndSeminars)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{cs.Name}</div><div class='entry-meta'>{cs.RoleOfParticipation?.ValueAr} · {cs.OrganizingAuthority} · {cs.Venue} · {cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- SCIENTTIFIC MISSIONS
            if (cv.ScientificMissions?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المهمات العلمية</span></div>");
                foreach (var sm in cv.ScientificMissions)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{sm.MissionName}</div><div class='entry-meta'>{sm.UniversityOrFaculty} · {sm.CountryOrCity} · {sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- TRAINING AND PROGRAMS
            if (cv.TrainingPrograms?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>البرامج التدريبية</span></div>");
                foreach (var tp in cv.TrainingPrograms)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{tp.TrainingProgramName}</div><div class='entry-meta'>{tp.Venue} · {tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- COMMITTEES AND ASSOCIATIONS
            if (cv.CommitteesAndAssociations?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>اللجان و الجمعيات</span></div>");
                foreach (var cas in cv.CommitteesAndAssociations)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{cas.NameOfCommitteeOrAssociation}</div><div class='entry-meta'>{cas.TypeOfCommitteeOrAssociation?.ValueAr} . {cas.DegreeOfSubscription?.ValueAr} . {cas.StartDate:yyyy/MM/dd} – {(cas.EndDate.HasValue ? cas.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- PARTICIPATION IN MAGAZINES
            if (cv.ParticipationInMagazines?.Any() == true)
            {
                sb.Append("<div class='section-title'><span> المشاركة في المجلات </span></div>");
                foreach (var pim in cv.ParticipationInMagazines)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{pim.NameOfMagazine}</div><div class='entry-meta'>{pim.TypeOfParticipation?.ValueAr} . {pim.WebsiteOfMagazine}</div></div>"); ;
                }
            }

            // --- REVIEWING ARTICLES
            if (cv.ReviewingArticles?.Any() == true)
            {
                sb.Append("<div class='section-title'><span> تحكيم المقالات </span></div>");
                foreach (var ra in cv.ReviewingArticles)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ra.TitleOfArticle}</div><div class='entry-meta'>{ra.Authority} . {ra.ReviewingDate:yyyy/MM/dd}</div></div>"); ;
                }
            }

            // --- PROJECTS ---
            if (cv.Projects?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المشاريع</span></div>");
                foreach (var p in cv.Projects)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{p.NameOfProject}</div><div class='entry-meta'>{p.TypeOfProject?.ValueAr} · {p.ParticipationRole?.ValueAr} · {p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");
                }
            }

            // --- CONTIBUTIONS TO COMMUNITY SERVICE
            if (cv.ContributionsToCommunityService?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المساهمات لخدمة المجتمع</span></div>");
                foreach (var ctcs in cv.ContributionsToCommunityService)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ctcs.ContributionTitle}</div><div class='entry-meta'>{ctcs.DateOfContribution:yyyy/MM/dd}</div></div>");
                }
            }

            // --- CONTIBUTIONS TO UNIVERSITY
            if (cv.ContributionsToUniversity?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المساهمات للجامعة</span></div>");
                foreach (var ctu in cv.ContributionsToUniversity)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{ctu.ContributionTitle}</div><div class='entry-meta'>{ctu.TypeOfContribution?.ValueAr} . {ctu.DateOfContribution:yyyy/MM/dd}</div></div>");

                }
            }

            // --- PARTICIPATION IN QUALITY WORKS
            if (cv.ParticipationInQualityWork?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المشاركة في اعمال الجودة</span></div>");
                foreach (var piqw in cv.ParticipationInQualityWork)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{piqw.ParticipationTitle}</div><div class='entry-meta'>{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>");

                }
            }

            // --- SCIENTIFIC WRITINGS
            if (cv.ScientificWritings?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>المؤلفات العلمية</span></div>");
                foreach (var sw in cv.ScientificWritings)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{sw.Title}</div><div class='entry-meta'>{sw.AuthorRole?.ValueAr} · {sw.PublishingHouse} · ISBN: {sw.ISBN} · {sw.PublishingDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- PATENTS
            if (cv.Patents?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>براءات الاختراع</span></div>");
                foreach (var p in cv.Patents)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{p.NameOfPatent}</div><div class='entry-meta'>{p.AccreditingAuthorityOrCountry} · {p.AccreditationDate:yyyy/MM/dd}</div></div>");
                }
            }

            // --- PRIZES AND REWARDS ---
            if (cv.PrizesAndRewards?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>الجوائز والمكافآت</span></div>");
                foreach (var pr in cv.PrizesAndRewards)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{pr.Prize?.ValueAr}</div><div class='entry-meta'>{pr.AwardingAuthority} · {pr.DateReceived:yyyy/MM/dd}</div></div>");
                }
            }

            // --- MANIFESTATIONS OF SCIENTIFIC APPRECIATIONS ---
            if (cv.ManifestationsOfScientificAppreciation?.Any() == true)
            {
                sb.Append("<div class='section-title'><span>مظاهر التقدير العلمي</span></div>");
                foreach (var msa in cv.ManifestationsOfScientificAppreciation)
                {
                    sb.Append($@"<div class='entry'><div class='entry-title'>{msa.TitleOfAppreciation}</div><div class='entry-meta'>{msa.IssuingAuthority} · {msa.DateOfAppreciation:yyyy/MM/dd}</div></div>");
                }
            }
            
            sb.Append(@"</div></div></div></body>
            </html>");

            return sb.ToString();
        }
    }
}