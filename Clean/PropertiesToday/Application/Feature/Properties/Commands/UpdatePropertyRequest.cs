using Application.Models;
using Application.Repository;
using Domain;
using MediatR;

namespace Application.Feature.Properties.Commands
{
    public class UpdatePropertyRequest : IRequest<bool>
    {
        public UpdatePropertyRequest(UpdateProperty updateProperty)
        {
            UpdateProperty = updateProperty;
        }

        public UpdateProperty UpdateProperty { get; set; }

    }

    public class UpdatePropertyRequestHandler : IRequestHandler<UpdatePropertyRequest, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        public UpdatePropertyRequestHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property propertyInDb = await _propertyRepository.GetByIdAsync(request.UpdateProperty.Id);
            if (propertyInDb != null)
            {
                propertyInDb.Name = request.UpdateProperty.Name;
                propertyInDb.Description = request.UpdateProperty.Description;
                propertyInDb.Type = request.UpdateProperty.Type;
                propertyInDb.ErfSize = request.UpdateProperty.ErfSize;
                propertyInDb.FloorSize = request.UpdateProperty.FloorSize;
                propertyInDb.Price = request.UpdateProperty.Price;
                propertyInDb.Levies = request.UpdateProperty.Levies;
                propertyInDb.PetsAllowed = request.UpdateProperty.PetsAllowed;
                propertyInDb.BedRooms = request.UpdateProperty.BedRooms;
                propertyInDb.Bathrooms = request.UpdateProperty.Bathrooms;
                propertyInDb.Kitchens = request.UpdateProperty.Kitchens;
                propertyInDb.Louge = request.UpdateProperty.Louge;
                propertyInDb.Dining = request.UpdateProperty.Dining;
                await _propertyRepository.UpdateAsync(propertyInDb);
                return true;
            }
            return false;
        }
    }
}
