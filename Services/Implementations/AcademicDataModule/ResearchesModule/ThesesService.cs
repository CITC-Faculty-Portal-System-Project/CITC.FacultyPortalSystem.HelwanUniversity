using Domain.Contracts;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Global;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Collections.Generic;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ThesesService
        (IUnitOfWork unitOfWork, IMapper mapper
        , IAuthenticationService authenticationService) : BaseService<Thesis, int>
        (unitOfWork, authenticationService, mapper), IThesesService
    {
        
        
        
        protected override string EntityName => "Theses";


        public async Task<ThesesResponseDTO> AddTheses(ThesesDTO theses)
        {
            var researchesRepo = UnitOfWork.GetRepository<Research, int>();
            
            var currentUser = await GetCurrentUserAsync();

            theses.FacultyMemberId = currentUser.UserId;

            var entity = Mapper.Map<Thesis>(theses);
            
            if(theses.Researches is not null)
                foreach(var research in theses.Researches!)
                {
                    var researchEntity = await researchesRepo.
                        GetAsync(new ResearchSpecifications(research.Id , currentUser.UserId));

                    entity.Researches!.Add(researchEntity!);
                }

            
            await Repo.AddAsync(entity);

            await UnitOfWork.SaveChangesAsync();
            
            return Mapper.Map<ThesesResponseDTO>(entity);
        }

        public async Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses
            (ThesesSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var thesesEntites = await Repo.GetAllAsync(new ThesesSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new ThesesCountSpecifications(parameters, user.UserId));

            var currentPage = thesesEntites.Count();

            var thesesResponses = Mapper.Map<IEnumerable<ThesesResponseDTO>>(thesesEntites);

            return new PaginatedResult<ThesesResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, thesesResponses);
        }

        public async Task<ThesesResponseDTO> GetThesesById(int Id)
        {
            var user = await GetCurrentUserAsync();

            var entity = await Repo.GetAsync(new ThesesSpecifications(Id, user.UserId))
                     ??throw NotFound();

            EnsureOwnership(entity.FacultyMemberId, user.UserId , EntityName);

            return Mapper.Map<ThesesResponseDTO>(entity);
        }
    }
}
