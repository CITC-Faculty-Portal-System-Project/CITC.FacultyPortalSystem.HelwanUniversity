using Domain.Entities.MissionsModule;
using Services.Specifications.ConferncesAndSeminarsModule;
using Services.Specifications.MissionsModule;
using Shared;
using Shared.Dtos.ConfrencesAndSeminarsModule;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class MissionService(IUnitOfWork _unitOfWork, IMapper _mapper 
        , IAuthenticationService _authentication) : IMissionService
    {
        public async Task<MissionResponseDto> AddAsync(MissionAddDto mission)
        {
            var repo = _unitOfWork.GetRepository<ScientificMissions, int>();
            
            var existsSpecification = new MissionCheckingAlreadyExistSpecification(mission);
            var currentOne = repo.GetAsync(existsSpecification);
            if(currentOne is not null)
                throw new MissionAlreadyExist();

            var currentUser = await _authentication.GetCurrentUserAsync(_authentication.GetLoggedUserEmail());
            mission.CreatedBy = currentUser.UserName;
            mission.FacultyMemberId = currentUser.UserId;

            var entity = _mapper.Map<ScientificMissions>(mission);
            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MissionResponseDto>(mission);

        }

        public async Task<bool> DeleteMissionAsync(int id , string reason = "لا يوجد")
        {
            var email = _authentication.GetLoggedUserEmail();
            var user = await _authentication.GetCurrentUserAsync(email);

            var repo =  _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new FacultyMemberGetsMissionSpecification(id);
            var mission = await repo.GetAsync(specification) ?? throw new NotFoundException("Cannot find this mission");
            if(mission?.FacultyMember?.Email != email)
                throw new UnauthorizedAccessException("You Don't Have Acess to Delete this Mission");

            mission.DeletedBy = user.UserName;
            mission.DeletedAt = DateTime.Now;
            mission.DeletionReason = reason;
            mission.IsDeleted = true;

            repo.Update(mission);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<MissionEditResponseDto?> EditAsync(int id , MissionEditDto mission)
        {
            var repo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new FacultyMemberGetsMissionSpecification(id);
            var foundMission = await repo.GetAsync(specification) 
                ?? throw new NotFoundException("Mission isnot Found!");
            
            var currentUser = await _authentication.GetCurrentUserAsync(_authentication.GetLoggedUserEmail());
            if (foundMission.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Edit this Mission");
            
            
            mission.UpdatedAt = DateTime.Now;
            mission.UpdatedBy = currentUser.UserName;
            foundMission = _mapper.Map<ScientificMissions>(mission);
            repo.Update(foundMission);

            var response = _mapper.Map<MissionEditResponseDto>(mission);
            return response;

         }

        public async Task<PaginatedResult<MissionResponseDto?>> GetAllMissionsAsync(MissionSpecificationParamaters paramaters)
        {
            var repo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new FacultyMemberGetsMissionSpecification(paramaters);
            var missionsEntity = await repo.GetAsync(specification) ?? throw new NotFoundException("No Missions Found");

            var mappedData = _mapper.Map<IEnumerable<MissionResponseDto>>(missionsEntity);
            var currentPageCount = mappedData.Count();

            var countSpecifications = new MissionsCountSpecification(paramaters);
            var totalCount = await repo.CountAsync(countSpecifications);
            return new PaginatedResult<MissionResponseDto?>(paramaters.pageIndex, currentPageCount, totalCount, mappedData);

        }

        public async Task<MissionResponseDto?> GetMissionByIdAsync(int id)
        {
            var currentUser = await _authentication
                 .GetCurrentUserAsync(_authentication.GetLoggedUserEmail()) ??
                 throw new UnauthorizedAccessException("You Cannot Get Mission");


            var repo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specfifcation = new FacultyMemberGetsMissionSpecification(id);
            var entity = await repo.GetAsync(specfifcation) ?? throw new NotFoundException("No Missions with this Id");

            if (entity.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Get this Mission");

            var mapped = _mapper.Map<MissionResponseDto>(entity);
            return mapped;

        }
    }
}
