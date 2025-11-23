using Domain.Entities.MissionsModule;
using Services.Specifications.ConferncesAndSeminarsModule;
using Services.Specifications.MissionsModule;
using Shared;
using Shared.Dtos.ConfrencesAndSeminarsModule;
using Shared.Dtos.MissionsModule;
using Shared.SpecificationParameters.SemiarsAndConferncesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SeminarsAndConferncesService(IAuthenticationService _authenticationService
                                 , IUnitOfWork _unitOfWork , IMapper _mapper) : ISeminarsAndConfrencesService
    {
        public async Task<ConferncesAndSeminarsResponseDto> AddAsync(ConfrencesAndSeminarsAddDto confrences)
        {
            var currentUser = await _authenticationService
                    .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ?? 
                    throw new UnauthorizedAccessException("You Cannot Add Confernce Or Seminar");
            
            var repo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            confrences.CreatedAt = DateTime.Now;
            confrences.CreatedBy = currentUser.UserName;

            var entity = _mapper.Map<ConferencesAndSeminars>(confrences);
            entity.FacultyMemberId = currentUser.UserId;

            await repo.AddAsync(entity);
            return _mapper.Map<ConferncesAndSeminarsResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id , string reason = "لا يوجد")
        {
            var currentUser = await _authenticationService
                    .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                    throw new UnauthorizedAccessException("You Cannot Delete Confernce Or Seminar");

            var repo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specification = new ConferncesAndSeminarsSpecification(id);
            var entity = await repo.GetAsync(specification) ?? throw new NotFoundException("This Seminar Doesn't Exist");

            if(entity.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Delete Confernce Or Seminar");

            
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            entity.DeletedBy = currentUser.UserName;
            entity.DeletionReason = reason;
            repo.Update(entity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResult<ConferncesAndSeminarsResponseDto?>> GetAsync(SeminarsAndConferncesSpecificationParameters parameters)
        {
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Get Confernce Or Seminar");
        
            parameters.FacultyMemberEmail = currentUser.Email;

            var repo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specification = new ConferncesAndSeminarsSpecification(parameters);
            var entity = await repo.GetAsync(specification) ?? throw new NotFoundException("There are No Seminars");

            var mappedData = _mapper.Map<IEnumerable<ConferncesAndSeminarsResponseDto>>(entity);
            var currentPageCount = mappedData.Count();

            var countSpecifications = new ConferncesAndSeminarsCountSpecification(parameters);
            var totalCount = await repo.CountAsync(countSpecifications);
            return new PaginatedResult<ConferncesAndSeminarsResponseDto?>(parameters.pageIndex, currentPageCount, totalCount, mappedData);


        }

        public async Task<ConferncesAndSeminarsResponseDto?> GetByIdAsync(int id)
        { 

            var currentUser = await _authenticationService
                        .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                        throw new UnauthorizedAccessException("You Cannot Get Confernce Or Seminar");


            var repo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specfifcation = new ConferncesAndSeminarsSpecification(id);
            var entity = await repo.GetAsync(specfifcation) ?? throw new NotFoundException("No Seminars with this Id");

            if (entity.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Read this Seminars");

            var mapped = _mapper.Map<ConferncesAndSeminarsResponseDto>(entity);
            return mapped;

        }

        public async Task<ConferncesAndSeminarsResponseDto?> UpdateAsync(int id, ConfrencesAndSeminarsEditDto editDto)
        {
            var currentUser = await _authenticationService
                                 .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                                 throw new UnauthorizedAccessException("You Cannot Update Confernce Or Seminar");


            var repo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specfifcation = new ConferncesAndSeminarsSpecification(id);
            var entity = await repo.GetAsync(specfifcation) ?? throw new NotFoundException("No Seminars with this Id");

            if (entity.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Update this Seminars");

            editDto.UpdatedBy = currentUser.UserName;
            _mapper.Map<ConferencesAndSeminars>(editDto);
            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<ConferncesAndSeminarsResponseDto>(entity);
            return mapped;
        }
    }
}
