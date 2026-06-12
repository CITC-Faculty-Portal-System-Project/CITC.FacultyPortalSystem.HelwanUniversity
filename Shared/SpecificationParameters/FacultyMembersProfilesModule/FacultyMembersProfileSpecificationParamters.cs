using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.FacultyMembersProfilesModule
{
    public class FacultyMembersProfileSpecificationParamters : BaseFacultyMemberProfileSpecificationParamters
    {

        public Guid? BeforeFacultyMemberId;

        private const int MaxTake = 50;
        private int take = 20;

        public int Take
        {
            get => take;
            set => take = value > MaxTake ? MaxTake : value;
        }



    }
}
