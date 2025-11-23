using Shared;
using Shared.Dtos.ConfrencesAndSeminarsModule;
using Shared.SpecificationParameters.SemiarsAndConferncesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface ISeminarsAndConfrencesService
    {
        public Task<ConferncesAndSeminarsResponseDto> AddAsync(ConfrencesAndSeminarsAddDto confrences);
        public Task<PaginatedResult<ConferncesAndSeminarsResponseDto?>> GetAsync(SeminarsAndConferncesSpecificationParameters parameters);
        public Task<ConferncesAndSeminarsResponseDto?> GetByIdAsync(int id);
        public Task<ConferncesAndSeminarsResponseDto?> UpdateAsync(int id , ConfrencesAndSeminarsEditDto editDto);
        public Task<bool> DeleteAsync(int id , string reason = "لا يوجد");
    }
}
