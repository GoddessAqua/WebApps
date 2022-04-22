using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForIronWaterStudio.Models;

namespace TestTaskForIronWaterStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public JsonResult  GetFilteredProducts([FromBody] Filter filter)
        {
            if(filter.LowerBound > 0 && filter.UpperBound > 0 && filter.Name == "")
            {
                var products = _context.Products
                                       .Where(p => p.Price >= filter.LowerBound && p.Price <= filter.UpperBound && filter.LowerBound <= filter.UpperBound)
                                       .Select(p => new { p.Id, p.Name, p.Price })
                                       .ToList();
                return new JsonResult(products);
            }
            else if (filter.LowerBound > 0 && filter.UpperBound > 0 && filter.Name != "")
            {
                var products = _context.Products
                                      .Where(p =>p.Name == filter.Name && p.Price >= filter.LowerBound && p.Price <= filter.UpperBound && filter.LowerBound <= filter.UpperBound)
                                      .Select(p => new { p.Id, p.Name, p.Price }).ToList();
                return new JsonResult(products);
            }
            else if (filter.LowerBound <= 0 && filter.UpperBound <= 0 && filter.Name != "")
            {
                var products = _context.Products
                                      .Where(p => p.Name == filter.Name)
                                      .Select(p => new { p.Id, p.Name, p.Price }).ToList();
                return new JsonResult(products);
            }
            else
            {
                var products = _context.Products;
                return new JsonResult(products);
            }
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetFilteredProducts(int id)
        {
            var product = await _context.Products
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return new JsonResult(NotFound());
            return new JsonResult(product);
        }
    }
}