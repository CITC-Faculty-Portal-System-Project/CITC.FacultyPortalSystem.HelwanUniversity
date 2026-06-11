using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using System.Linq.Expressions;

namespace Services.Specifications.FacultyMembersProfilesModule
{
    public class OtherUsersPageSpecifications : BaseSpecifications<FacultyMember, Guid>

    {
        public OtherUsersPageSpecifications
            (BaseFacultyMemberProfileSpecificationParamters parameters , Guid currentUserId, int take = 9 , bool isPaginated = false) 
            : base(fd => !fd.IsDeleted && fd.Id != currentUserId &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || fd.PersonalData!.NameAr.Contains(parameters.Search)
                    || fd.PersonalData!.NameEn.Contains(parameters.Search)
                    || fd.PersonalData!.Faculty.NameAR.Contains(parameters.Search)
                    || fd.PersonalData!.Department.NameAR.Contains(parameters.Search)
                    || fd.PersonalData!.Department.NameEN.Contains(parameters.Search)
                    || fd.PersonalData!.Faculty.NameEN.Contains(parameters.Search)))
        {

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title!);
            AddIncludes(fd => fd.PersonalData!.ProfilePicture!);
            AddIncludes(fd => fd.PersonalData!.Department!);
            
            AddOrderByDescending(fd => fd.ResearchContributions!.Count);


            if(isPaginated)
                ApplyCursorTake(take);
                AddOrderBy(m => m.Id);
        }
    }
}
