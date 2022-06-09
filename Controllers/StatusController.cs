using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using Microsoft.AspNetCore.Http;
namespace TodoApi.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase {
        private readonly TodoApiContext _context;
        public StatusController(TodoApiContext context){
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllStatus(){
            return Ok(_context.TblStatuses.ToList());
        }
    }
}