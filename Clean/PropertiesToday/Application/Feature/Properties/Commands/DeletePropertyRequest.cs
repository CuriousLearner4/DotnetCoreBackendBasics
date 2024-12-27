using System.ComponentModel;
using Application.Repository;
using AutoMapper.Configuration.Conventions;
using Domain;
using MediatR;

namespace Application.Feature.Properties.Commands
{
    public class DeletePropertyRequest :IRequest<bool>
    {
        public int PropertyId { get; set; }

        public DeletePropertyRequest(int id)
        {
            PropertyId = id;
        }
    }

    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyRequest,bool>
    {
        public readonly IPropertyRepository propertyRepository;
        public DeletePropertyRequestHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            Property property = await propertyRepository.GetByIdAsync(request.PropertyId);
            if (property == null) return false;
            await propertyRepository.DeleteAsync(property);
            return true;
        }
    }
}
