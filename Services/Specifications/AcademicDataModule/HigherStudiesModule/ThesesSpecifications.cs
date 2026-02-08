using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.DataFetchingFromExternalService;

namespace Services.Specifications.AcademicDataModule.HigherStudiesModule
{
    internal class ThesesSpecifications : BaseSpecifications<Thesis , int>
    {
        public ThesesSpecifications(ThesesFetchingDTO thesesFetchingDTO)
            : base(th => !th.IsDeleted && th.Link == thesesFetchingDTO.Link && th.Title == thesesFetchingDTO.Title
            && th.Grade.ValueAr == thesesFetchingDTO.Grade && th.EnrollmentDate == thesesFetchingDTO.EnrollmentDate
            && th.RegistrationDate == thesesFetchingDTO.RegistrationDate && th.InternalGradeDate == thesesFetchingDTO.InternalGradeDate
            && th.SupervisionConfirmationDate == thesesFetchingDTO.SupervisionConfirmationDate && th.FacultyMember.NationalNumber == thesesFetchingDTO.NationalNumber)
        {
            AddIncludes(th => th.FacultyMember);
            AddIncludes(th => th.Grade);
        }


        public ThesesSpecifications(Guid FacultyMemberId , string title)
            : base(th => !th.IsDeleted && th.Title == title && th.FacultyMemberId == FacultyMemberId)
         {
         }
    }
}
