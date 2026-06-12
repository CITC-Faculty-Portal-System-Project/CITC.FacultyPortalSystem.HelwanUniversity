using Domain.Entities.UniversityFacultiesAndDepartments;
using Microsoft.EntityFrameworkCore;

namespace Services.Specifications.LookUpItems
{
    internal class DepartmentSpecifications : BaseSpecifications<Department, int>
    {
        public DepartmentSpecifications
            (string deptName) : 
            base(d => !d.IsDeleted && EF.Functions.Like(d.NameAR , deptName) || EF.Functions.Like(d.NameEN, deptName))
        {
        }


        public DepartmentSpecifications
    (int id) :
    base(d => !d.IsDeleted && d.Id == id)
        {
            AddIncludes(d => d.Faculty!);
        }
    }
}
