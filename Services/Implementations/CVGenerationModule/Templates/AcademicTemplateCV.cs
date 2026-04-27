using QuestPDF.Fluent;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.CVGenerationModule;
using System.Text;

namespace Services.Implementations.CVGenerationModule.Templates
{
    public class AcademicTemplateCV(IAttachmentService _attachmentService) : ICVTemplate
    {
        public string TemplateName => "academic";

        public byte[] GeneratePdf(CVResponseDTO cv)
        {
            return new Pdf.AcademicPdfDocumentCV(cv).GeneratePdf();
        }

        public async Task <string> GenerateHtml(CVResponseDTO cv)
        {
            AttachmentDownloadDTO? profilePicture = null;

            if (cv.ProfilePictureId is not null)
            {
                profilePicture = await _attachmentService.GetAsync(Abstraction.Enums.AttachmentContext.ProfilePicture, cv.PersonalDataId, cv.ProfilePictureId.Value);

            }

            var sb = new StringBuilder();
            sb.Append($@"
<!doctype html>
<html dir='rtl' lang='ar'>
<head>
    <meta charset='UTF-8' /><style>
✅ النسخة الـ Responsive
* {{ box-sizing: border-box; margin: 0; padding: 0; }}

body {{
    font-family: 'Cairo', sans-serif;
    background: #f8fafc;
    padding: 20px;
    direction: rtl;
}}

.cv-wrap {{
    background: #fff;
    max-width: 900px;
    margin: 0 auto;
    box-shadow: 0 4px 32px rgba(25,53,90,0.1);
    border-radius: 8px;
    overflow: hidden;
}}

.top-bar {{ height: 10px; background: #b38e19; }}

/* ================= HEADER ================= */
.cv-header {{
    display: flex;
    flex-wrap: wrap;
    background: #19355a;
}}

.header-left {{
    flex: 1 1 55%;
    padding: 36px;
    border-left: 1px solid rgba(255, 255, 255, 0.15);
}}

.header-right {{
    flex: 0 1 42%;
    padding: 36px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    gap: 8px;
}}

.cv-header .name {{
    font-size: 2rem;
    font-weight: 900;
    color: #fff;
}}

.cv-header .job-title {{
    color: #b38e19;
    font-weight: 600;
    font-size: 1.05rem;
}}

.cv-header .bio-short {{
    color: #cbd5e1;
    font-size: 0.84rem;
    line-height: 1.6;
}}

/* ================= BODY ================= */
.cv-body {{
    padding: 28px;
    background: #fdf8ec;
}}

.section-title {{
    display: flex;
    align-items: center;
    gap: 8px;
    margin-top: 24px;
    margin-bottom: 14px;
}}

.section-title .bar {{
    width: 32px;
    height: 3px;
    background: #b38e19;
}}

.section-title .line {{
    flex: 1;
    height: 1px;
    background: #e2e8f0;
}}

.section-title span {{
    color: #19355a;
    font-weight: 800;
    font-size: 0.9rem;
}}

.entry-card {{
    background: #f8fafc;
    border: 1px solid #e2e8f0;
    border-right: 3px solid #b38e19;
    border-radius: 6px;
    padding: 14px;
    margin-bottom: 10px;
}}

/* ================= TABLE ================= */
.table-row {{
    display: flex;
    gap: 14px;
    font-size: 0.84rem;
    padding: 4px 0;
    border-bottom: 1px solid #e2e8f0;
}}

.table-row .t-label {{
    color: #b38e19;
    font-weight: 700;
    min-width: 110px;
}}

.table-row .t-value {{
    color: #1e293b;
    line-height: 1.5;
}}

/* ================= RESPONSIVE ================= */

/* Tablets */
@media (max-width: 768px) {{
    .cv-header {{
        flex-direction: column;
    }}

    .header-left,
    .header-right {{
        flex: 1 1 100%;
        padding: 20px;
        border: none;
    }}

    .cv-header .name {{
        font-size: 1.6rem;
    }}

    .cv-body {{
        padding: 20px;
    }}
}}

/* Small phones */
@media (max-width: 480px) {{
    body {{
        padding: 10px;
    }}

    .cv-header .name {{
        font-size: 1.3rem;
    }}

    .cv-header .job-title {{
        font-size: 0.9rem;
    }}

    .cv-header .bio-short {{
        font-size: 0.75rem;
    }}

    .meta-field,
    .contact-small {{
        font-size: 0.75rem;
    }}

    .header-skill-pill {{
        font-size: 0.65rem;
        padding: 2px 6px;
    }}

    .section-title span {{
        font-size: 0.8rem;
    }}

    .entry-title {{
        font-size: 0.85rem;
    }}

    .table-row {{
        flex-direction: column;
        gap: 2px;
    }}

    .table-row .t-label {{
        min-width: auto;
        font-size: 0.75rem;
    }}

    .table-row .t-value {{
        font-size: 0.75rem;
    }}
}}

/* Very small screens (old phones) */
@media (max-width: 360px) {{
    .cv-header .name {{
        font-size: 1.1rem;
    }}

    .cv-body {{
        padding: 14px;
    }}

    .header-left,
    .header-right {{
        padding: 14px;
    }}

    .entry-card {{
        padding: 10px;
    }}
}}
    </style>
</head>
<body>
    <div class='cv-wrap'>
        <div class='top-bar'></div>
        <div class='cv-header'>
            <div class='header-left'>
                <div class='name'>{cv.NameAr}</div>
                {(cv.Title != null ? $"<div class='job-title'>{cv.Title.ValueAr}</div>" : "")}
                <div class='bio-short'>{cv.BioSummary}</div>
            </div>
            <div class='header-right'>
                {(cv.Department != null ? $"<div class='meta-field'><span class='label'>القسم: </span>{cv.Department}</div>" : "")}
                {(cv.Authority != null ? $"<div class='meta-field'><span class='label'>الكلية: </span>{cv.Authority.ValueAr}</div>" : "")}
                {(cv.University != null ? $"<div class='meta-field'><span class='label'>الجامعة: </span>{cv.University.ValueAr}</div>" : "")}
                {(cv.OfficialEmail != null ? $"<div class='contact-small'>{cv.OfficialEmail}</div>" : "")}
                {(cv.MainPhoneNumber != null ? $"<div class='contact-small'> هاتف: {cv.MainPhoneNumber}</div>" : "")}
                {(cv.WorkPhoneNumber != null ? $"<div class='contact-small'> هاتف عمل: {cv.WorkPhoneNumber}</div>" : "")}
                {(cv.FaxNumber != null ? $"<div class='contact-small'>فاكس: {cv.FaxNumber}</div>" : "")}
                {(cv.BirthDate.HasValue ? $"<span class='birthdate-small'></span><span>تاريخ الميلاد: {cv.BirthDate.Value:yyyy/MM/dd}</span>" : "")}
                <div class='header-skills'>
                    {string.Join("", cv.Skills?.Select(s => $"<span class='header-skill-pill'>{s}</span>") ?? new List<string>())}
                </div>
            </div>
        </div>
        <div class='cv-body'>");

            // --- RENDERING SECTIONS ---
            
            // --- GENERAL EXPERIENCES ---
            if (cv.GeneralExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>الخبرات العامة</span><div class='line'></div></div>");
                foreach (var ge in cv.GeneralExperiences)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{ge.ExperienceTitle}</div>
            <div class='table-row'><span class='t-label'>الجهة</span><span class='t-value'>{ge.Authority}</span></div>
            <div class='table-row'><span class='t-label'>البلد / المدينة</span><span class='t-value'>{ge.CountryOrCity}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- TEACHING EXPERIENCES ---
            if (cv.TeachingExperiences?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>الخبرات التدريسية</span><div class='line'></div></div>");
                foreach (var te in cv.TeachingExperiences)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{te.CourseName}</div>
            <div class='table-row'><span class='t-label'>المستوى الأكاديمي</span><span class='t-value'>{te.AcademicLevel}</span></div>
            <div class='table-row'><span class='t-label'>الجامعة / الكلية</span><span class='t-value'>{te.UniversityOrFaculty}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- ACADEMIC QUALIFICATIONS ---
            if (cv.AcademicQualifications?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المؤهلات العلمية</span><div class='line'></div></div>");
                foreach (var aq in cv.AcademicQualifications)
                {
                    sb.Append($@"<div class='entry-card'>
                <div class='entry-title'>{aq.Qualification?.ValueAr} — {aq.Specialization}</div>
                <div class='table-row'><span class='t-label'>التقدير</span><span class='t-value'>{aq.Grade?.ValueAr}</span></div>
                <div class='table-row'><span class='t-label'>الجامعة</span><span class='t-value'>{aq.UniversityOrFaculty}</span></div>
                <div class='table-row'><span class='t-label'>التاريخ</span><span class='t-value'>{aq.DateOfObtainingTheQualification:yyyy/MM/dd}</span></div>
            </div>");
                }
            }

            // --- JOB RANKS ---
            if (cv.JobRanks?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>الدرجات الوظيفية</span><div class='line'></div></div>");
                foreach (var jr in cv.JobRanks)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{jr.JobRank?.ValueAr}</div>
            <div class='table-row'><span class='t-label'>تاريخ الدرجة</span><span class='t-value'>{jr.DateOfJobRank:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- ADMINISTRATIVE POSITIONS ---
            if (cv.AdministrativePositions?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المناصب الإدارية</span><div class='line'></div></div>");
                foreach (var ap in cv.AdministrativePositions)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{ap.Position}</div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- CONFERENCES AND SEMINARS ---
            if (cv.ConferencesAndSeminars?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المؤتمرات والندوات</span><div class='line'></div></div>");
                foreach (var cs in cv.ConferencesAndSeminars)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{cs.Name}</div>
            <div class='table-row'><span class='t-label'>دور المشاركة</span><span class='t-value'>{cs.RoleOfParticipation?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>الجهة المنظمة</span><span class='t-value'>{cs.OrganizingAuthority}</span></div>
            <div class='table-row'><span class='t-label'>مكان الانعقاد</span><span class='t-value'>{cs.Venue}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- SCIENTIFIC MISSIONS ---
            if (cv.ScientificMissions?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المهمات العلمية</span><div class='line'></div></div>");
                foreach (var sm in cv.ScientificMissions)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{sm.MissionName}</div>
            <div class='table-row'><span class='t-label'>الجامعة / الكلية</span><span class='t-value'>{sm.UniversityOrFaculty}</span></div>
            <div class='table-row'><span class='t-label'>البلد / المدينة</span><span class='t-value'>{sm.CountryOrCity}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- TRAINING AND PROGRAMS ---
            if (cv.TrainingPrograms?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>البرامج التدريبية</span><div class='line'></div></div>");
                foreach (var tp in cv.TrainingPrograms)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{tp.TrainingProgramName}</div>
            <div class='table-row'><span class='t-label'>مكان الانعقاد</span><span class='t-value'>{tp.Venue}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- COMMITTEES AND ASSOCIATIONS ---
            if (cv.CommitteesAndAssociations?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>اللجان والجمعيات</span><div class='line'></div></div>");
                foreach (var cas in cv.CommitteesAndAssociations)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{cas.NameOfCommitteeOrAssociation}</div>
            <div class='table-row'><span class='t-label'>النوع</span><span class='t-value'>{cas.TypeOfCommitteeOrAssociation?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>درجة الاشتراك</span><span class='t-value'>{cas.DegreeOfSubscription?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{cas.StartDate:yyyy/MM/dd} – {(cas.EndDate.HasValue ? cas.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- PARTICIPATION IN MAGAZINES ---
            if (cv.ParticipationInMagazines?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المشاركة في المجلات</span><div class='line'></div></div>");
                foreach (var pim in cv.ParticipationInMagazines)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{pim.NameOfMagazine}</div>
            <div class='table-row'><span class='t-label'>نوع المشاركة</span><span class='t-value'>{pim.TypeOfParticipation?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>رابط المجلة</span><span class='t-value'>{pim.WebsiteOfMagazine}</span></div>
        </div>");
                }
            }

            // --- REVIEWING ARTICLES ---
            if (cv.ReviewingArticles?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>تحكيم المقالات</span><div class='line'></div></div>");
                foreach (var ra in cv.ReviewingArticles)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{ra.TitleOfArticle}</div>
            <div class='table-row'><span class='t-label'>الجهة</span><span class='t-value'>{ra.Authority}</span></div>
            <div class='table-row'><span class='t-label'>تاريخ التحكيم</span><span class='t-value'>{ra.ReviewingDate:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- PROJECTS ---
            if (cv.Projects?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المشاريع</span><div class='line'></div></div>");
                foreach (var p in cv.Projects)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{p.NameOfProject}</div>
            <div class='table-row'><span class='t-label'>نوع المشروع</span><span class='t-value'>{p.TypeOfProject?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>دور المشاركة</span><span class='t-value'>{p.ParticipationRole?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- CONTRIBUTIONS TO COMMUNITY SERVICE ---
            if (cv.ContributionsToCommunityService?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المساهمات لخدمة المجتمع</span><div class='line'></div></div>");
                foreach (var ctcs in cv.ContributionsToCommunityService)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{ctcs.ContributionTitle}</div>
            <div class='table-row'><span class='t-label'>التاريخ</span><span class='t-value'>{ctcs.DateOfContribution:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- CONTRIBUTIONS TO UNIVERSITY ---
            if (cv.ContributionsToUniversity?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المساهمات للجامعة</span><div class='line'></div></div>");
                foreach (var ctu in cv.ContributionsToUniversity)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{ctu.ContributionTitle}</div>
            <div class='table-row'><span class='t-label'>نوع المساهمة</span><span class='t-value'>{ctu.TypeOfContribution?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>التاريخ</span><span class='t-value'>{ctu.DateOfContribution:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- PARTICIPATION IN QUALITY WORKS ---
            if (cv.ParticipationInQualityWork?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المشاركة في أعمال الجودة</span><div class='line'></div></div>");
                foreach (var piqw in cv.ParticipationInQualityWork)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{piqw.ParticipationTitle}</div>
            <div class='table-row'><span class='t-label'>الفترة</span><span class='t-value'>{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</span></div>
        </div>");
                }
            }

            // --- SCIENTIFIC WRITINGS ---
            if (cv.ScientificWritings?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>المؤلفات العلمية</span><div class='line'></div></div>");
                foreach (var sw in cv.ScientificWritings)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{sw.Title}</div>
            <div class='table-row'><span class='t-label'>دور المؤلف</span><span class='t-value'>{sw.AuthorRole?.ValueAr}</span></div>
            <div class='table-row'><span class='t-label'>دار النشر</span><span class='t-value'>{sw.PublishingHouse}</span></div>
            <div class='table-row'><span class='t-label'>ISBN</span><span class='t-value'>{sw.ISBN}</span></div>
            <div class='table-row'><span class='t-label'>تاريخ النشر</span><span class='t-value'>{sw.PublishingDate:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- PATENTS ---
            if (cv.Patents?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>براءات الاختراع</span><div class='line'></div></div>");
                foreach (var p in cv.Patents)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{p.NameOfPatent}</div>
            <div class='table-row'><span class='t-label'>جهة الاعتماد</span><span class='t-value'>{p.AccreditingAuthorityOrCountry}</span></div>
            <div class='table-row'><span class='t-label'>تاريخ الاعتماد</span><span class='t-value'>{p.AccreditationDate:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- PRIZES AND REWARDS ---
            if (cv.PrizesAndRewards?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>الجوائز والمكافآت</span><div class='line'></div></div>");
                foreach (var pr in cv.PrizesAndRewards)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{pr.Prize?.ValueAr}</div>
            <div class='table-row'><span class='t-label'>الجهة المانحة</span><span class='t-value'>{pr.AwardingAuthority}</span></div>
            <div class='table-row'><span class='t-label'>تاريخ الاستلام</span><span class='t-value'>{pr.DateReceived:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            // --- MANIFESTATIONS OF SCIENTIFIC APPRECIATION ---
            if (cv.ManifestationsOfScientificAppreciation?.Any() == true)
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>مظاهر التقدير العلمي</span><div class='line'></div></div>");
                foreach (var msa in cv.ManifestationsOfScientificAppreciation)
                {
                    sb.Append($@"<div class='entry-card'>
            <div class='entry-title'>{msa.TitleOfAppreciation}</div>
            <div class='table-row'><span class='t-label'>جهة الإصدار</span><span class='t-value'>{msa.IssuingAuthority}</span></div>
            <div class='table-row'><span class='t-label'>التاريخ</span><span class='t-value'>{msa.DateOfAppreciation:yyyy/MM/dd}</span></div>
        </div>");
                }
            }

            if (!string.IsNullOrEmpty(cv.PersonalWebsite) || !string.IsNullOrEmpty(cv.LinkedIn) ||
                !string.IsNullOrEmpty(cv.GoogleScholar) || !string.IsNullOrEmpty(cv.Scopus) ||
                !string.IsNullOrEmpty(cv.YouTube) || !string.IsNullOrEmpty(cv.Facebook) ||
                !string.IsNullOrEmpty(cv.Instagram) || !string.IsNullOrEmpty(cv.X))
            {
                sb.Append("<div class='section-title'><div class='bar'></div><span>التواصل الاجتماعي</span><div class='line'></div></div>");
                sb.Append("<div class='entry-card'>");

                if (!string.IsNullOrEmpty(cv.PersonalWebsite))
                    sb.Append($"<div class='table-row'><span class='t-label'>الموقع الشخصي</span><span class='t-value'>{cv.PersonalWebsite}</span></div>");

                if (!string.IsNullOrEmpty(cv.LinkedIn))
                    sb.Append($"<div class='table-row'><span class='t-label'>LinkedIn</span><span class='t-value'>{cv.LinkedIn}</span></div>");

                if (!string.IsNullOrEmpty(cv.GoogleScholar))
                    sb.Append($"<div class='table-row'><span class='t-label'>Google Scholar</span><span class='t-value'>{cv.GoogleScholar}</span></div>");

                if (!string.IsNullOrEmpty(cv.Scopus))
                    sb.Append($"<div class='table-row'><span class='t-label'>Scopus</span><span class='t-value'>{cv.Scopus}</span></div>");

                if (!string.IsNullOrEmpty(cv.YouTube))
                    sb.Append($"<div class='table-row'><span class='t-label'>YouTube</span><span class='t-value'>{cv.YouTube}</span></div>");

                if (!string.IsNullOrEmpty(cv.Facebook))
                    sb.Append($"<div class='table-row'><span class='t-label'>Facebook</span><span class='t-value'>{cv.Facebook}</span></div>");

                if (!string.IsNullOrEmpty(cv.Instagram))
                    sb.Append($"<div class='table-row'><span class='t-label'>Instagram</span><span class='t-value'>{cv.Instagram}</span></div>");

                if (!string.IsNullOrEmpty(cv.X))
                    sb.Append($"<div class='table-row'><span class='t-label'>X (Twitter)</span><span class='t-value'>{cv.X}</span></div>");

                sb.Append("</div>");
            }

            sb.Append("</div></div></body></html>");
            return sb.ToString();
        }

    }
}
