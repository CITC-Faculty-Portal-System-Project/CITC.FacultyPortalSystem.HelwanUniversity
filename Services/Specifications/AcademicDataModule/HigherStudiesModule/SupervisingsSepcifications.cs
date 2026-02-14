
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.DataFetchingFromExternalService;

namespace Services.Specifications.AcademicDataModule.HigherStudiesModule
{
    internal class SupervisingsSepcifications : BaseSpecifications<Supervising , int>
    {
        public SupervisingsSepcifications(SupervisingsFetchingDTO supervisingsFetchingDTO) 
            : base(sp => sp.Title == supervisingsFetchingDTO.ThesisTitle && sp.StudentName == supervisingsFetchingDTO.StudentName
            && sp.Specialization== supervisingsFetchingDTO.Specialization && sp.RegistrationDate == supervisingsFetchingDTO.RegistrationDate
            && sp.SupervisionFormationDate == supervisingsFetchingDTO.SupervisionFormationDate && sp.GrantingDate == supervisingsFetchingDTO.GrantingDate
            && sp.DiscussionDate == supervisingsFetchingDTO.DiscussionDate && sp.UniversityOrFaculty == supervisingsFetchingDTO.UniversityFaculty
            && sp.FacultyMember.NationalNumber == supervisingsFetchingDTO.NationalNumber)
        {
            AddIncludes(sp => sp.FacultyMember);
        }
    }
}
