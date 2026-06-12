using Domain.Entities.UniversityFacultiesAndDepartments;
using Services.Specifications.LookUpItems;
using System.Text.Json;

namespace Services.Implementations.ReportsAndDashboards.Helpers
{
    public class FacultyDepartmentResolver
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        private readonly JsonSerializerOptions _jsonOptions;

        public FacultyDepartmentResolver(
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            JsonSerializerOptions jsonOptions)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _jsonOptions = jsonOptions;
        }

        public async Task<(List<Faculty> Faculties, List<Department> Departments)>
            ResolveFacultiesAndDepartmentsAsync(
                List<int>? facultyIds,
                List<int>? departmentIds)
        {
            var faculties = new List<Faculty>();
            var departments = new List<Department>();

            if (facultyIds != null && facultyIds.Any())
            {
                var facultyRepo = _unitOfWork.GetRepository<Faculty, int>();

                foreach (var facultyId in facultyIds)
                {
                    var cacheKey = $"faculty:{facultyId}";

                    Faculty? faculty = null;

                    var cachedFaculty =
                        await _cacheService.GetCachedValueAsync(cacheKey);

                    if (!string.IsNullOrEmpty(cachedFaculty))
                    {
                        faculty = JsonSerializer.Deserialize<Faculty>(cachedFaculty , _jsonOptions);
                    }
                    else
                    {
                        faculty = await facultyRepo.GetByIdAsync(facultyId);

                        if (faculty != null)
                        {
                            await _cacheService.SetCachedValueAsync(
                                cacheKey,
                                faculty,
                                TimeSpan.FromHours(1));
                        }
                    }

                    if (faculty != null)
                    {
                        faculties.Add(faculty);
                    }
                }
            }

            if (departmentIds != null && departmentIds.Any())
            {
                var departmentRepo =
                    _unitOfWork.GetRepository<Department, int>();

                foreach (var departmentId in departmentIds)
                {
                    var cacheKey = $"department:{departmentId}";

                    Department? department = null;

                    var cachedDepartment =
                        await _cacheService.GetCachedValueAsync(cacheKey);

                    if (!string.IsNullOrEmpty(cachedDepartment))
                    {
                        department = JsonSerializer.Deserialize<Department>(
                            cachedDepartment , _jsonOptions);
                    }

                    else
                    {
                        department = await departmentRepo.GetAsync(
                            new DepartmentSpecifications(departmentId));

                        if (department != null)
                        {
                            await _cacheService.SetCachedValueAsync(
                                cacheKey,
                                department,
                                TimeSpan.FromHours(1));
                        }
                    }

                    if (department != null)
                    {
                        departments.Add(department);
                    }
                }
            }

            return (faculties, departments);
        }
    }
}
