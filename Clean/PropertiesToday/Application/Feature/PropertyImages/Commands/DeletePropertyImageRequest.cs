using Application.Repository;
using Domain;
using MediatR;

namespace Application.Feature.PropertyImages.Commands
{
    public class DeletePropertyImageRequest : IRequest<bool>
    {
        public DeletePropertyImageRequest(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; }


    }

    public class DeletePropertyImageRequestHandler :IRequestHandler<DeletePropertyImageRequest,bool>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public DeletePropertyImageRequestHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<bool> Handle(DeletePropertyImageRequest request, CancellationToken cancellationToken)
        {
            Image image = await _propertyImageRepository.GetImageByIdAsync(request.ImageId);
            if (image == null)
            {
                return false;
            }
            await _propertyImageRepository.DeleteAsync(image);
            return true;
        }
    }
}


