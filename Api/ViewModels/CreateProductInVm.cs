namespace Api.ViewModels;

public record CreateProductInVm(
    string Title,
    int Price,
    int Discount,
    int InventoryCount
);