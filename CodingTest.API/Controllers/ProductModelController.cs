using CodingTest.Logic;
using CodingTest.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.API.Controllers
{
    [ApiController]
    [Route("api/ProductModel")]
    public class ProductModelController : ControllerBase
    {
        private ILogger<ProductModelController> _logger;
        private AppDBContext _context;

        // May as well inject a logger
        public ProductModelController(ILogger<ProductModelController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _context.ProductModels.OrderBy(e => e.Name).ToList();
        }

        [HttpGet]
        [Route("{productId:int}")]
        public ActionResult<ProductModel> Get(int productId)
        {
            var record = _context.ProductModels.Where(e => e.Id == productId).FirstOrDefault();

            return (record != null) ? Ok(record) : NotFound(productId);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Insert([FromBody] ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            _context.ProductModels.Add(productModel);

            await _context.SaveChangesAsync();

            if (productModel.Id > 0)
                return new CreatedResult($"{Request.Path}/{productModel.Id}", productModel);
            else
                return BadRequest(productModel);
        }

        // These were not requested but they work if needed :)
        /*
        [HttpPut]
        [Route("{productId:int}")]
        public async Task<ActionResult<ProductModel>> Update(int productId, [FromBody] ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var model = _context.ProductModels.Where(e => e.Id == productId).FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            model.Description = productModel.Description;
            model.Name = productModel.Name;
            model.Price = productModel.Price;

            await _context.SaveChangesAsync();

            return Ok(productModel);
        }

        [HttpDelete]
        [Route("{productId:int}")]
        public async Task<ActionResult<ProductModel>> Delete(int productId)
        {
            var model = _context.ProductModels.Where(e => e.Id == productId).FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            _context.ProductModels.Remove(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
        */
    }
}