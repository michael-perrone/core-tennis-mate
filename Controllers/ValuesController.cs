using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tennis_Mate.Data;
using Tennis_Mate.Models;

namespace Tennis_Mate.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {  
        private readonly DataContext dataContext;
        public ValuesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await this.dataContext.Values.ToListAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetValue(int id)
        {
            var value = await this.dataContext.Values.FirstOrDefaultAsync(x => id == x.Id);

            return Ok(value);
        }
    } 
}
