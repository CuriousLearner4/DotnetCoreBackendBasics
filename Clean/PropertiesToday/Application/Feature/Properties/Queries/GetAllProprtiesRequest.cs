
using System.Collections.Generic;
using Application.Models;
using Application.PipelineBehaviour.Contracts;
using Application.Repository;
using AutoMapper;
using MediatR;

namespace Application.Feature.Properties.Queries
{
    public class GetAllProprtiesRequest : IRequest<IEnumerable<ViewProperty>>, ICacheable
    {
        public GetAllProprtiesRequest()
        {
            CacheKey = "GetAllProprties";
        }

        public string CacheKey { get; set; }
        public bool BypassCache { get ; set; }
        public TimeSpan SlidingExpiration { get ; set; }
        
    }

    public class GetAllPropertiesRequestHandler : IRequestHandler<GetAllProprtiesRequest, IEnumerable<ViewProperty>>
    {
        private readonly IPropertyRepository _propertiesRepo;
        private readonly IMapper _mapper;

        public GetAllPropertiesRequestHandler(IPropertyRepository propertiesRepo, IMapper mapper)
        {
            _propertiesRepo = propertiesRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewProperty>> Handle(GetAllProprtiesRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ViewProperty> properties = _mapper.Map<IEnumerable<ViewProperty>>(await _propertiesRepo.GetAllAsync());
            return properties;
        }
    }
}
