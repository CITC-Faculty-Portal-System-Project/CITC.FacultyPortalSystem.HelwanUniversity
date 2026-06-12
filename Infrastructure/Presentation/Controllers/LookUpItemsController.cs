namespace Presentation.Controllers
{
    public class LookUpItemsController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(IEnumerable<FacultyResponseDTO>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("UniversityFaculties")]
        public async Task<ActionResult<IEnumerable<FacultyResponseDTO>>>GetAllFaculties()
            => Ok(await _serviceManager.LookUpItemService.GetAllFacultiesAsync());


        [ProducesResponseType(typeof(IEnumerable<FacultyWithDepartmentResposneDTO>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("UniversityFacultiesWithDepartments")]
        public async Task<ActionResult<IEnumerable<FacultyWithDepartmentResposneDTO>>> GetAllFacultiesWithDepartmentsAsync()
            => Ok(await _serviceManager.LookUpItemService.GetAllFacultiesWithDepartmentsAsync());



        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicQualifications")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAcademicQualifications()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AcademicQualification"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Universities")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetUniversities()
          => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("University"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Faculties")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetFaculties()
          => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Faculty"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("MagazineParticipationRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetMagazineParticipationRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("MagazineParticipationRole"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AuthorRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAuthorRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AuthorRole"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicGrades")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAcademicGrades()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AcademicGrade"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Rewards")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetRewards()
              => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Rewards"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("DispatchTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetDispatches()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Dispatch"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Titles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetTitle()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Title"));

        
        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("SocialStates")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetSocialStates()
      => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("SocialStatus"));

        
        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Genders")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetGenders()
      => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Gender"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("StudyFields")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetStudyFields()
      => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("StudyField"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Departments")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetDepartments()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Department"));



        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ContributionTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetContributionTypes()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ContributionTypes"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("EmploymentDegrees")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetEmploymentDegrees()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("EmploymentDegrees"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("SeminarParticipationTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetSmemiarParticipationTypes()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("SmemiarParticipationType"));


        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("CommitteeParticipationDegrees")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetComiteeParticipationDegrees()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ComiteeParticipationDegree"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("TypesofCommittee")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetTypesofComitee()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("TypeofComitee"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ProjectTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetProjectTypes()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ProjectType"));

        [ProducesResponseType(typeof(LookupItemDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ProjectRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetProjectRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ProjectRole"));



    }
}
