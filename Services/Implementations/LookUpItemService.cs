using Services.Abstraction.Contracts.Common;
using Services.Specifications.LookUpItems;

namespace Services.Implementations
{
    public class LookUpItemService(IUnitOfWork _unitOfWork
        , IMapper _mapper
        , ILangContext _langContext) : ILookUpItemService
    {
        public async Task<IEnumerable<LookupItemDto>> GetLookUpItemByType(string type)
        {
            var repo = _unitOfWork.GetRepository<Lookup, Guid>();
            var specification = new LookUpItemTypeSpecification(type);
            var entity = await repo.GetAllAsync(specification);


            return _mapper.Map<IEnumerable<LookupItemDto>>(entity, opt =>
            {
                opt.Items["isAr"] = _langContext.IsAr;
            }); 
            
        }
    }
}
