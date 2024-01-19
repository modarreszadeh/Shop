using Api.Services.Product;
using Api.ViewModels;
using Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var productList = await _productService.GetListAsync(cancellationToken);

        var productListOutVm = productList.Select(p => p.Adapt<ProductOutVm>());

        return Ok(productListOutVm);
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> Details(int productId, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(productId, cancellationToken);

        var productOutVm = product.Adapt<ProductOutVm>();

        return Ok(productOutVm);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateProductInVm inputVm, CancellationToken cancellationToken)
    {
        var product = inputVm.Adapt<Product>();

        await _productService.CreateAsync(product, cancellationToken);

        return Created();
    }

    [HttpPatch("{productId:int}")]
    public async Task<IActionResult> IncreaseInventory(int productId, int count, CancellationToken cancellationToken)
    {
        await _productService.IncreaseInventoryCountAsync(productId, count, cancellationToken);

        return Ok();
    }
}