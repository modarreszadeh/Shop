namespace Api.ViewModels;

public record ProductOutVm(
    int Id,
    string Title,
    int Price,
    int InventoryCount,
    int Discount
);