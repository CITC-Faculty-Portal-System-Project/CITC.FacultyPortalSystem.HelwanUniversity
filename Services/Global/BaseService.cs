using Shared.Dtos.IdentityModule;

using Shared.Enums.Logging;
namespace Services.Global
{
    public abstract class BaseService<TEntity, TId>(
        IUnitOfWork _unitOfWork,
        IAuthenticationService _authenticationService,
        IMapper _mapper)
        where TEntity : class
        where TId : notnull
    {
        protected readonly IMapper Mapper = _mapper;
        protected readonly IUnitOfWork UnitOfWork = _unitOfWork;
        protected readonly IAuthenticationService AuthService = _authenticationService;

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
                ?? throw new NotFoundException($"Faculty Member with email {email} not found.");
        }

        protected async Task<UserResultDto> GetUserByEmailAsync(string email)
            => await AuthService.GetCurrentUserAsync(email)
				?? throw new NotFoundException($"User with email {email} not found.");

        protected async Task<UserResultDto> GetUserByIdAsync(Guid id)
            => await AuthService.GetUserByIdAsync(id)
				?? throw new NotFoundException($"User with ID {id} not found.");
		#endregion

		#region Repository
		protected string? GetUserIP()
            => AuthService.GetUserIP();
        #endregion

        #region Repository
        protected IGenericRepository<TEntity, TId> Repo
            => UnitOfWork.GetRepository<TEntity, TId>();

        protected IGenericRepository<T, TKey> GetRepository<T, TKey>()
            where T : class
            where TKey : notnull
            => UnitOfWork.GetRepository<T, TKey>();
        #endregion

        #region Ownership
        protected static void EnsureOwnership(
            Guid entityFacultyMemberId,
            Guid currentUserId,
            string? entityNameOverride = null)
        {
            if (entityFacultyMemberId != currentUserId)
                throw new UnauthorizedAccessException(
                    $"You do not have permission to access this {(entityNameOverride ?? "resource")}."
                );
        }


        protected async Task EnsureOwnershipIfClientAsync(
         Guid entityFacultyMemberId,
         string? facultyMemberEmail)
        {
            if (facultyMemberEmail is not null)
                return;

            var currentUser = await GetCurrentUserAsync();

            EnsureOwnership(
                entityFacultyMemberId,
                currentUser.UserId,
                EntityName);
        }



        #endregion

        #region Persistence
        protected abstract string EntityName { get; }

        protected async Task SaveChangesAsync()
            => await UnitOfWork.SaveChangesAsync();

        protected NotFoundException NotFound()
            => new($"The requested {EntityName} resource was not found.");
        #endregion
    }
}
