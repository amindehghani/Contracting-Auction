using AutoMapper;
using ContractingAuction.Core.Interfaces.IMapper;

namespace ContractingAuction.Core.Mapper;

public class BaseMapper<TSource, TDestination> : IBaseMapper<TSource, TDestination>
{
    private readonly IMapper _mapper;

    public BaseMapper(
        IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination MapModel(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public IEnumerable<TDestination> MapList(IEnumerable<TSource> sources)
    {
        return _mapper.Map<IEnumerable<TDestination>>(sources);
    }
}