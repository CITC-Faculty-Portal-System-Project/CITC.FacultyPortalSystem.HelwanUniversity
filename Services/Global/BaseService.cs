using Shared.Dtos.IdentityModule;
namespace Services.Global
{
    public abstract class BaseService<TEntity, TId>(IUnitOfWork _unitOfWork, IAuthenticationService _authenticationService, IMapper _mapper, IValidationService _validationService)
        where TEntity : BaseEntity<TId>
        where TId : notnull
    {
        protected readonly IMapper Mapper = _mapper;
        protected readonly IUnitOfWork UnitOfWork = _unitOfWork;
        protected readonly IAuthenticationService AuthService = _authenticationService;
        protected readonly IValidationService ValidationService = _validationService;

        #region Identity
        protected async Task<UserResultDto> GetCurrentUserAsync()
        {
            var userEmail = AuthService.GetLoggedUserEmail();
            return await AuthService.GetCurrentUserAsync(userEmail)
                ?? throw new UnauthorizedAccessException("Unauthorized.");
        }

        protected async Task<FacultyMember> GetFacultyMemberByEmailAsync(string email)
        {
            var repo = UnitOfWork.GetRepository<FacultyMember, Guid>();
            return await repo.GetAsync(new FacultyMemberWithEmailSpecifications(email))
                ?? throw new NotFoundException("errors.FacultyMember.notFound" , email);
        }
        #endregion

        #region Repository
        protected IGenericRepository<TEntity, TId> Repo 
            => UnitOfWork.GetRepository<TEntity, TId>();

        protected IGenericRepository<T, TKey> GetRepository<T, TKey>()
            where T : BaseEntity<TKey>
            where TKey : notnull
            => UnitOfWork.GetRepository<T, TKey>();
        #endregion

        #region Ownership
        protected static void EnsureOwnership(
            Guid entityFacultyMemberId,
            Guid currentUserId,
            string? entityNameOverride = null)
        {
            if(entityFacultyMemberId != currentUserId)
                throw new UnauthorizedAccessException(
                    $"You do not have permission to access this {(entityNameOverride ?? "resource")}."
                );
        }
        #endregion

        #region Persistence
        protected abstract string EntityName { get; }

        protected async Task SaveChangesAsync()
            => await UnitOfWork.SaveChangesAsync();

        protected NotFoundException NotFound()
            => new("errors.Entity.notFound" , EntityName);
        #endregion

        #region Validations
        protected async Task ValidateAsync<T>(T dto)
        {
            await ValidationService.ValidateAsync(dto);
        }
        #endregion

    }
}
