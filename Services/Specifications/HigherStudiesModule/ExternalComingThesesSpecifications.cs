using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.DataFetchingFromExternalService;

namespace Services.Specifications.HigherStudiesModule
{
    internal class ExternalComingThesesSpecifications : BaseSpecifications<Thesis , int>
    {
        public ExternalComingThesesSpecifications(ThesesFetchingDTO thesesFetchingDTO)
            : base(th => !th.IsDeleted && th.Link == thesesFetchingDTO.Link && th.Title == thesesFetchingDTO.Title
            && th.Grade.ValueAr == thesesFetchingDTO.Grade && th.EnrollmentDate == thesesFetchingDTO.EnrollmentDate
            && th.RegistrationDate == thesesFetchingDTO.RegistrationDate && th.InternalGradeDate == thesesFetchingDTO.InternalGradeDate
            && th.SupervisionConfirmationDate == thesesFetchingDTO.SupervisionConfirmationDate && th.FacultyMember.NationalNumber == thesesFetchingDTO.NationalNumber)
        {
            AddIncludes(th => th.FacultyMember);
            AddIncludes(th => th.Grade);
        }


        public ExternalComingThesesSpecifications(Guid FacultyMemberId , string title)
            : base(th => !th.IsDeleted && th.Title == title && th.FacultyMemberId == FacultyMemberId)
         {
         }
    }
}
