using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.PropertyImages.Queries
{
    public class GetAllPropertyImagesRequest : IRequest<IEnumerable<ViewImage>>
    {
    
    }

    public class GetAllPropertyImagesRequestHandler : IRequestHandler<GetAllPropertyImagesRequest, IEnumerable<ViewImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IMapper mapper;

        public GetAllPropertyImagesRequestHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ViewImage>> Handle(GetAllPropertyImagesRequest request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<ViewImage>>(await propertyImageRepository.GetAllAsync());
        }
    }
}
