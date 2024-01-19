using Api.Services.Order;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutOrderInVm inputVm, CancellationToken cancellationToken)
    {
        var isSuccessCheckout = await _orderService.CheckoutAsync(inputVm.UserId, inputVm.ProductId, cancellationToken);

        if (isSuccessCheckout == false)
            return BadRequest();

        return Ok();
    }
}