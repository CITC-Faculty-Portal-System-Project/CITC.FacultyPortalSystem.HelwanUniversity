using Domain.Entities.UniversityFacultiesAndDepartments;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.LookUpItems
{
    internal class FacultySpecifications : BaseSpecifications<Faculty, int>
    {
        public FacultySpecifications
            (string facultyName) : 
                base(d => !d.IsDeleted && EF.Functions.Like(d.NameAR, facultyName) || EF.Functions.Like(d.NameEN, facultyName))
        {
        }
    }
}
