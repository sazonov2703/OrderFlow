namespace Application.DTOs;

public record ShippingInOrder(decimal ShippingCost,
    string ShippingRecipentName,
    string ShippingCountry,
    string ShippingCity,
    string ShippingStreet,
    string ShippingHouseNumber,
    string ShippingFlatNumber,
    string ShippingZipCode
    );