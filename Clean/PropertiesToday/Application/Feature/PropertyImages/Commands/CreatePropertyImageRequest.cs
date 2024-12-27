using Application.Models;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.PropertyImages.Commands
{
    public class CreatePropertyImageRequest :IRequest<bool>
    {
        public NewImage NewImage { get; set; }

        public CreatePropertyImageRequest(NewImage newImage)
        {
            NewImage = newImage;
        }

    }

    public class CreatePropertyImageRequestHandler : IRequestHandler<CreatePropertyImageRequest, bool>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public CreatePropertyImageRequestHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePropertyImageRequest request, CancellationToken cancellationToken)
        {
            Image image = _mapper.Map<Image>(request.NewImage);
            await _propertyImageRepository.AddAsync(image);
            return true;

        }
    }
}
