using QuestPDF.Fluent;
using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using System.Text;

namespace Services.Implementations.CVGenerationModule.Templates
{
    public class ProfessionalTemplateCV : ICVTemplate
    {
        public string TemplateName => "professional";

        public byte[] GeneratePdf(CVResponseDTO cv)
        {
            return new Pdf.ProfessionalPdfDocumentCV(cv).GeneratePdf();
        }

        public string GenerateHtml(CVResponseDTO cv)
        {
            var initials = string.IsNullOrEmpty(cv.Name) ? "" : cv.Name.Substring(0, 1);
            var sb = new StringBuilder();

            // بداية الـ HTML مع الـ Styles
            sb.Append($@"
<!doctype html>
<html dir='rtl' lang='ar'>
<head>
    <meta charset='UTF-8' /><style>
    * {{ box-sizing: border-box; margin: 0; padding: 0; }}
    body {{ font-family: 'Cairo', sans-serif; background: #f8fafc; padding: 20px; direction: rtl; }}
    .cv-wrap {{ background: #fff; max-width: 980px; margin: 0 auto; display: flex; box-shadow: 0 4px 32px rgba(25, 53, 90, 0.12); border-radius: 8px; overflow: hidden; min-height: 800px; }}
    .cv-sidebar {{ background: #19355a; flex: 0 0 260px; padding: 36px 22px; display: flex; flex-direction: column; }}
    .avatar {{ width: 80px; height: 80px; border-radius: 50%; background: rgba(179, 142, 25, 0.3); border: 3px solid #b38e19; margin: 0 auto 18px auto; display: flex; align-items: center; justify-content: center; }}
    .avatar-initial {{ color: #b38e19; font-weight: 900; font-size: 1.8rem; }}
    .sidebar-name {{ color: #fff; font-weight: 800; font-size: 1.1rem; text-align: center; margin-bottom: 4px; }}
    .sidebar-title {{ color: #b38e19; font-weight: 600; font-size: 0.82rem; text-align: center; margin-bottom: 22px; }}
    .sb-section {{ margin-bottom: 22px; }}
    .sb-section-title {{ color: #b38e19; font-weight: 700; font-size: 0.75rem; text-transform: uppercase; border-bottom: 1px solid rgba(255, 255, 255, 0.15); padding-bottom: 5px; margin-bottom: 10px; }}
    .sb-text {{ color: #cbd5e1; font-size: 0.78rem; margin-bottom: 4px; word-break: break-word; line-height: 1.5; }}
    .sb-skill {{ display: inline-block; background: rgba(179, 142, 25, 0.18); color: #b38e19; border-radius: 99px; padding: 2px 10px; font-size: 0.68rem; font-weight: 600; border: 1px solid rgba(179, 142, 25, 0.5); margin: 2px; }}
    .cv-main {{ flex: 1; padding: 36px; }}
    .affiliation-box {{ background: #f0f4f8; border-radius: 6px; padding: 12px 16px; margin-bottom: 18px; font-size: 0.85rem; color: #334155; }}
    .section-title {{ color: #19355a; font-weight: 800; font-size: 0.88rem; text-transform: uppercase; margin-top: 24px; margin-bottom: 10px; padding-bottom: 6px; border-bottom: 2px solid #19355a; }}
    .entry-item {{ margin-bottom: 12px; padding-bottom: 12px; border-bottom: 1px solid #f1f5f9; }}
    .e-title {{ font-weight: 700; font-size: 0.9rem; color: #1e293b; }}
    .e-meta {{ color: #64748b; font-size: 0.78rem; }}
    </style>
</head>
<body>
    <div class='cv-wrap'>
        <div class='cv-sidebar'>
            <div class='avatar'><span class='avatar-initial'>{initials}</span></div>
            <div class='sidebar-name'>{cv.Name}</div>
            {(cv.Title != null ? $"<div class='sidebar-title'>{cv.Title.ValueAr}</div>" : "")}
            
            <div class='sb-section'>
                <div class='sb-section-title'>بيانات الاتصال</div>
                {(!string.IsNullOrEmpty(cv.OfficialEmail) ? $"<div class='sb-text'>{cv.OfficialEmail}</div>" : "")}
                {(!string.IsNullOrEmpty(cv.MainPhoneNumber) ? $"<div class='sb-text'> هاتف: {cv.MainPhoneNumber}</div>" : "")}
                {(!string.IsNullOrEmpty(cv.WorkPhoneNumber) ? $"<div class='sb-text'> هاتف عمل: {cv.WorkPhoneNumber}</div>" : "")}
                {(!string.IsNullOrEmpty(cv.FaxNumber) ? $"<div class='sb-text'>فاكس: {cv.FaxNumber}</div>" : "")}
                {(cv.BirthDate.HasValue ? $"<div class='sb-text'>تاريخ الميلاد: {cv.BirthDate.Value:yyyy/MM/dd}</div>" : "")}
            </div>

            {(cv.Skills?.Any() == true ? $@"<div class='sb-section'><div class='sb-section-title'>المهارات</div>{string.Join("", cv.Skills.Select(s => $"<span class='sb-skill'>{s}</span>"))}</div>" : "")}");

            // إضافة السوشيال ميديا داخل السايد بار
            if (HasSocialMedia(cv))
            {
                sb.Append("<div class='sb-section'><div class='sb-section-title'>التواصل الاجتماعي</div>");
                if (!string.IsNullOrEmpty(cv.PersonalWebsite)) sb.Append($"<div class='sb-text'>الموقع الشخصي: {cv.PersonalWebsite}</div>");
                if (!string.IsNullOrEmpty(cv.LinkedIn)) sb.Append($"<div class='sb-text'>LinkedIn: {cv.LinkedIn}</div>");
                if (!string.IsNullOrEmpty(cv.GoogleScholar)) sb.Append($"<div class='sb-text'>Scholar: {cv.GoogleScholar}</div>");
                if (!string.IsNullOrEmpty(cv.Scopus)) sb.Append($"<div class='sb-text'>Scopus: {cv.Scopus}</div>");
                if (!string.IsNullOrEmpty(cv.YouTube)) sb.Append($"<div class='sb-text'>YouTube: {cv.YouTube}</div>");
                if (!string.IsNullOrEmpty(cv.Facebook)) sb.Append($"<div class='sb-text'>Facebook: {cv.Facebook}</div>");
                if (!string.IsNullOrEmpty(cv.Instagram)) sb.Append($"<div class='sb-text'>Instagram: {cv.Instagram}</div>");
                if (!string.IsNullOrEmpty(cv.X)) sb.Append($"<div class='sb-text'>X: {cv.X}</div>");
                sb.Append("</div>");
            }

            sb.Append("</div> ");

            sb.Append($@"
        <div class='cv-main'>
            <div class='affiliation-box'>
                {(cv.Department != null ? cv.Department.ValueAr : "")} · {(cv.Authority != null ? cv.Authority.ValueAr : "")} · {(cv.University != null ? cv.University.ValueAr : "")}
            </div>
            {(string.IsNullOrEmpty(cv.BioSummary) ? "" : $"<div class='section-title'>نبذة تعريفية</div><p style='font-size:0.88rem; color:#374151; line-height:1.75;'>{cv.BioSummary}</p>")}
            
            {(cv.GeneralExperiences?.Any() == true ? $"<div class='section-title'>الخبرات العامة</div>" + string.Join("", cv.GeneralExperiences.Select(ge => $"<div class='entry-item'><div class='e-title'>{ge.ExperienceTitle}</div><div class='e-meta'>{ge.Authority} · {ge.CountryOrCity} · {ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.TeachingExperiences?.Any() == true ? $"<div class='section-title'>الخبرات التدريسية</div>" + string.Join("", cv.TeachingExperiences.Select(te => $"<div class='entry-item'><div class='e-title'>{te.CourseName}</div><div class='e-meta'>{te.AcademicLevel} · {te.UniversityOrFaculty} · {te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.AcademicQualifications?.Any() == true ? $"<div class='section-title'>المؤهلات العلمية</div>" + string.Join("", cv.AcademicQualifications.Select(aq => $"<div class='entry-item'><div class='e-title'>{aq.Qualification?.ValueAr} — {aq.Specialization}</div><div class='e-meta'>{aq.Grade?.ValueAr} · {aq.UniversityOrFaculty} · {aq.DateOfObtainingTheQualification:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.JobRanks?.Any() == true ? $"<div class='section-title'>الدرجات الوظيفية</div>" + string.Join("", cv.JobRanks.Select(jr => $"<div class='entry-item'><div class='e-title'>{jr.JobRank?.ValueAr}</div><div class='e-meta'>{jr.DateOfJobRank:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.AdministrativePositions?.Any() == true ? $"<div class='section-title'>المناصب الإدارية</div>" + string.Join("", cv.AdministrativePositions.Select(ap => $"<div class='entry-item'><div class='e-title'>{ap.Position}</div><div class='e-meta'>{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.ConferencesAndSeminars?.Any() == true ? $"<div class='section-title'>المؤتمرات والندوات</div>" + string.Join("", cv.ConferencesAndSeminars.Select(cs => $"<div class='entry-item'><div class='e-title'>{cs.Name}</div><div class='e-meta'>{cs.RoleOfParticipation?.ValueAr} · {cs.OrganizingAuthority} · {cs.Venue} · {cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.ScientificMissions?.Any() == true ? $"<div class='section-title'>المهمات العلمية</div>" + string.Join("", cv.ScientificMissions.Select(sm => $"<div class='entry-item'><div class='e-title'>{sm.MissionName}</div><div class='e-meta'>{sm.UniversityOrFaculty} · {sm.CountryOrCity} · {sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.TrainingPrograms?.Any() == true ? $"<div class='section-title'>البرامج التدريبية</div>" + string.Join("", cv.TrainingPrograms.Select(tp => $"<div class='entry-item'><div class='e-title'>{tp.TrainingProgramName}</div><div class='e-meta'>{tp.Venue} · {tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.CommitteesAndAssociations?.Any() == true ? $"<div class='section-title'>اللجان والجمعيات</div>" + string.Join("", cv.CommitteesAndAssociations.Select(ca => $"<div class='entry-item'><div class='e-title'>{ca.NameOfCommitteeOrAssociation}</div><div class='e-meta'>{ca.TypeOfCommitteeOrAssociation?.ValueAr} · {ca.DegreeOfSubscription?.ValueAr} · {ca.StartDate:yyyy/MM/dd} – {(ca.EndDate.HasValue ? ca.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.ParticipationInMagazines?.Any() == true ? $"<div class='section-title'>المشاركة في المجلات</div>" + string.Join("", cv.ParticipationInMagazines.Select(pim => $"<div class='entry-item'><div class='e-title'>{pim.NameOfMagazine}</div><div class='e-meta'>{pim.TypeOfParticipation?.ValueAr} · {pim.WebsiteOfMagazine}</div></div>")) : "")}

            {(cv.ReviewingArticles?.Any() == true ? $"<div class='section-title'>تحكيم المقالات</div>" + string.Join("", cv.ReviewingArticles.Select(ra => $"<div class='entry-item'><div class='e-title'>{ra.TitleOfArticle}</div><div class='e-meta'>{ra.Authority} · {ra.ReviewingDate:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.Projects?.Any() == true ? $"<div class='section-title'>المشاريع</div>" + string.Join("", cv.Projects.Select(p => $"<div class='entry-item'><div class='e-title'>{p.NameOfProject}</div><div class='e-meta'>{p.TypeOfProject?.ValueAr} · {p.ParticipationRole?.ValueAr} · {p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.ContributionsToCommunityService?.Any() == true ? $"<div class='section-title'>المساهمات لخدمة المجتمع</div>" + string.Join("", cv.ContributionsToCommunityService.Select(ctcs => $"<div class='entry-item'><div class='e-title'>{ctcs.ContributionTitle}</div><div class='e-meta'>{ctcs.DateOfContribution:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.ContributionsToUniversity?.Any() == true ? $"<div class='section-title'>المساهمات للجامعة</div>" + string.Join("", cv.ContributionsToUniversity.Select(ctu => $"<div class='entry-item'><div class='e-title'>{ctu.ContributionTitle}</div><div class='e-meta'>{ctu.TypeOfContribution?.ValueAr} · {ctu.DateOfContribution:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.ParticipationInQualityWork?.Any() == true ? $"<div class='section-title'>المشاركة في أعمال الجودة</div>" + string.Join("", cv.ParticipationInQualityWork.Select(piqw => $"<div class='entry-item'><div class='e-title'>{piqw.ParticipationTitle}</div><div class='e-meta'>{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}</div></div>")) : "")}

            {(cv.ScientificWritings?.Any() == true ? $"<div class='section-title'>المؤلفات العلمية</div>" + string.Join("", cv.ScientificWritings.Select(sw => $"<div class='entry-item'><div class='e-title'>{sw.Title}</div><div class='e-meta'>{sw.AuthorRole?.ValueAr} · {sw.PublishingHouse} · ISBN: {sw.ISBN} · {sw.PublishingDate:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.Patents?.Any() == true ? $"<div class='section-title'>براءات الاختراع</div>" + string.Join("", cv.Patents.Select(p => $"<div class='entry-item'><div class='e-title'>{p.NameOfPatent}</div><div class='e-meta'>{p.AccreditingAuthorityOrCountry} · {p.AccreditationDate:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.PrizesAndRewards?.Any() == true ? $"<div class='section-title'>الجوائز والمكافآت</div>" + string.Join("", cv.PrizesAndRewards.Select(pr => $"<div class='entry-item'><div class='e-title'>{pr.Prize?.ValueAr}</div><div class='e-meta'>{pr.AwardingAuthority} · {pr.DateReceived:yyyy/MM/dd}</div></div>")) : "")}

            {(cv.ManifestationsOfScientificAppreciation?.Any() == true ? $"<div class='section-title'>مظاهر التقدير العلمي</div>" + string.Join("", cv.ManifestationsOfScientificAppreciation.Select(msa => $"<div class='entry-item'><div class='e-title'>{msa.TitleOfAppreciation}</div><div class='e-meta'>{msa.IssuingAuthority} · {msa.DateOfAppreciation:yyyy/MM/dd}</div></div>")) : "")}
        </div>
    </div>
</body>
</html>");
            return sb.ToString();
        }

        private bool HasSocialMedia(CVResponseDTO cv)
        {
            return !string.IsNullOrEmpty(cv.PersonalWebsite) || !string.IsNullOrEmpty(cv.LinkedIn) ||
                   !string.IsNullOrEmpty(cv.GoogleScholar) || !string.IsNullOrEmpty(cv.Scopus) ||
                   !string.IsNullOrEmpty(cv.YouTube) || !string.IsNullOrEmpty(cv.Facebook) ||
                   !string.IsNullOrEmpty(cv.Instagram) || !string.IsNullOrEmpty(cv.X);
        }
    }
}