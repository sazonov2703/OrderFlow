using MediatR;

namespace Application.UseCases.Orders.Commands.Requests;

public record CreateOrderCommand(
    Guid WorkspaceId,
    List<Guid> OrderItemsId,
    Guid CustomerId,
    string Description,
    DateTime Deadline,
    decimal ShippingCost,
    string ShippingRecipentName,
    string ShippingCountry,
    string ShippingCity,
    string ShippingStreet,
    string ShippingHouseNumber,
    string ShippingFlatNumber,
    string ShippingZipCode
    ) : IRequest<Guid>;
