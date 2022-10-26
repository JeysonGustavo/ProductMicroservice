using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Core.Manager;
using Product.API.Core.Models.Domain;
using Product.API.Core.Models.Request;
using Product.API.Core.Models.Response;

namespace Product.API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productManager.GetAllProducts();

            return Ok(_mapper.Map<IEnumerable<ProductResponseModel>>(products));
        }

        [HttpGet("{id}", Name="GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productManager.GetProductById(id);

            if (product is null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponseModel>(product));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductRequestModel requestModel)
        {
            var product = _mapper.Map<ProductModel>(requestModel);

            await _productManager.CreateProduct(product);

            var response = await _productManager.GetProductById(product.Id);
            return Ok(_mapper.Map<ProductResponseModel>(response));
        }
    }
}