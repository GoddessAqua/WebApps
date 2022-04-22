using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForIronWaterStudio.Models;

namespace TestTaskForIronWaterStudio.Controllers
{
    public class HomeController : Controller
    {
        readonly ProductContext _context;
        private readonly IConfiguration Configuration;

        public HomeController(ProductContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public IActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorize(LoginInfo user)
        {
            ConfigurationParameters config = new(Configuration);
           return (user.Login.Equals(config.OnGet().Login) && user.Password.Equals(config.OnGet().Password)) ?
                RedirectToAction("Manage") : RedirectToAction("Authorize");
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeProduct(int? id)
        {
            if (id != null)
            {
                var product = await _context.Products
                                            .FirstOrDefaultAsync(p => p.Id == id);
                if (product != null) return View(product);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage");
        }
    }
}