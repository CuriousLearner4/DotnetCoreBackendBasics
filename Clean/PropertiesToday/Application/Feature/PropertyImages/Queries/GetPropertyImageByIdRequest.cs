using Application.Models;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.PropertyImages.Queries
{
    public class GetPropertyImageByIdRequest : IRequest<ViewImage>
    {
        public GetPropertyImageByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }

    public class GetPropertyImageByIdRequestHandler : IRequestHandler<GetPropertyImageByIdRequest, ViewImage>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IMapper mapper;

        public GetPropertyImageByIdRequestHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.mapper = mapper;
        }

        public async Task<ViewImage> Handle(GetPropertyImageByIdRequest request, CancellationToken cancellationToken)
        {
            Image image = await propertyImageRepository.GetImageByIdAsync(request.Id);
            if (image != null)
            {
                return mapper.Map<ViewImage>(image);
            }
            return null;
        }
    }
}
