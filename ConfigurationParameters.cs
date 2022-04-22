using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForIronWaterStudio.Models;

namespace TestTaskForIronWaterStudio
{
    public class ConfigurationParameters: PageModel
    {
        private readonly IConfiguration Configuration;
        public ConfigurationParameters(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AuthorizeOptions OnGet()
        {
            var authorizeOptions = new AuthorizeOptions();
            Configuration.GetSection(AuthorizeOptions.AuthorizeInfo).Bind(authorizeOptions);
            return authorizeOptions;
        }
    }
}
