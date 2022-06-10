using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using TodoApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace TodoApi.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase{
        private readonly TodoApiContext _context;
        public TodosController(TodoApiContext context){
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllTodos(){
            return Ok(_context.TblTodos.Include("Status").ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetTodo([FromRoute]long? id){
            if (id == null) return BadRequest(); 
            var todo = _context.TblTodos.SingleOrDefault(x=>x.Id == id);
            if(todo == null) return NotFound();
            _context.Entry(todo).Reference(x => x.Status).Load();   
            return Ok(todo);
        }
        [HttpPost]
        public IActionResult CreateTodos([FromBody]TodoDTO todoDTO){
            if (todoDTO == null) return BadRequest();
            try
            {
                _context.Add(new TblTodo{
                    Todo = todoDTO.Todo,
                    StatusId = todoDTO.StatusId
                });
                _context.SaveChanges();
                return Ok();
            }
            catch (System.Exception)
            {
                return Problem();
            }
        }
        [HttpPut("{id}")]
        public IActionResult EditTodo([FromRoute] long? id,[FromBody]TodoDTO todoDTO){
            if (todoDTO == null || id == null) return BadRequest();
            var todo = _context.TblTodos.SingleOrDefault(e => e.Id == id);
            if (todo == null) return NotFound();
            try
            {
                todo.Todo = todoDTO.Todo;
                todo.StatusId = todoDTO.StatusId;
                _context.SaveChanges();
                 return Ok();
            }
            catch (System.Exception)
            {
                return Problem();
            }
        }
    }
}