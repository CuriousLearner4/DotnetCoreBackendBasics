using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.PipelineBehaviour.Contracts;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.Properties.Queries
{
    public class GetPropertyByIdRequest : IRequest<ViewProperty>, ICacheable
    {
        public int PropertyId { get; set; }
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpiration { get; set; }

        public GetPropertyByIdRequest(int PropertyId)
        {
            this.PropertyId = PropertyId;
            CacheKey = $"GetPropertieById:{PropertyId}";
        }
    }

    public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdRequest, ViewProperty>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetPropertyByIdRequestHandler(IPropertyRepository propertyRepository,IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<ViewProperty> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
        {
            Property property = await propertyRepository.GetByIdAsync(request.PropertyId);
            if (property != null)
            {
                return mapper.Map<ViewProperty>(property);
            }
            return null;
        }
    }

}
