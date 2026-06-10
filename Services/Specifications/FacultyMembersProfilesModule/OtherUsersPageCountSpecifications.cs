using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using System.Linq.Expressions;

namespace Services.Specifications.FacultyMembersProfilesModule
{
    public class OtherUsersPageCountSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public OtherUsersPageCountSpecifications
            (FacultyMembersProfileSpecificationParamters parameters) 
            : base(fd => !fd.IsDeleted &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || fd.PersonalData!.NameAr.Contains(parameters.Search)
                    || fd.PersonalData!.NameEn.Contains(parameters.Search)
                    || fd.PersonalData!.Faculty.NameAR.Contains(parameters.Search)
                    || fd.PersonalData!.Department.NameAR.Contains(parameters.Search)
                    || fd.PersonalData!.Department.NameEN.Contains(parameters.Search)
                    || fd.PersonalData!.Faculty.NameEN.Contains(parameters.Search)))
        {
        }
    }
}
