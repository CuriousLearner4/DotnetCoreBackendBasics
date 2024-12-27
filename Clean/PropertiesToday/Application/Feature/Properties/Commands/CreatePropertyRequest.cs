using Application.Models;
using Application.PipelineBehaviour.Contracts;
using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.Properties.Commands
{
    public class CreatePropertyRequest : IRequest<bool>,IValidatable
    {
        public NewProperty PropertyRequest { get; set; }
        public CreatePropertyRequest(NewProperty propertyRequest)
        {
            PropertyRequest = propertyRequest;
        }
    }

    public class CreatePropertyRequestHandler : IRequestHandler<CreatePropertyRequest, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public CreatePropertyRequestHandler(IMapper mapper, IPropertyRepository propertyRepository)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request.PropertyRequest);
            property.ListDate = DateTime.Now;
            await _propertyRepository.AddAsync(property);
            return true;
        }
    }
}
