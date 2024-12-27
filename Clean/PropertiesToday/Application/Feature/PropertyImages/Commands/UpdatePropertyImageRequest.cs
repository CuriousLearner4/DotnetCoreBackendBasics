using Application.Models;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.PropertyImages.Commands
{
    public class UpdatePropertyImageRequest : IRequest<bool>
    {
        public UpdateImage updateImage;

        public UpdatePropertyImageRequest(UpdateImage updateImage)
        {
            this.updateImage = updateImage;
        }
    }

    public class UpdatePropertyImageRequestHandler : IRequestHandler<UpdatePropertyImageRequest, bool>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyImageRequestHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePropertyImageRequest request, CancellationToken cancellationToken)
        {
            Image image = await _propertyImageRepository.GetImageByIdAsync(request.updateImage.Id);
            if (image == null)
            {
                return false;
            }
            _mapper.Map(request.updateImage, image);
            await _propertyImageRepository.UpdateAsync(image);
            return true;

        }
    }
}
