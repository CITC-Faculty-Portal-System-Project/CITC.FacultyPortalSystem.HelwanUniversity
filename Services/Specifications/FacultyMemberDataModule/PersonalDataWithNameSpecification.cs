using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.FacultyMemberDataModule
{
    internal class PersonalDataWithNameSpecification : BaseSpecifications<PersonalData, int>
    {
        public PersonalDataWithNameSpecification(string name) 
            : base(p => !p.IsDeleted &&  (EF.Functions.Like(p.Name, $"%{name}%") || EF.Functions.Like(p.NameInComposition, $"%{name}%")))
        {
            AddIncludes(p => p.FacultyMember!);
            
        }
    }
}
