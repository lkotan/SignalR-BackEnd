using AutoMapper;
using Chat.API.Repositories;
using Chat.Business.Abstract;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController:ControllerRepository<IRoomService,RoomModel>
    {
        private readonly IRoomService _service;
        public RoomsController(IRoomService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
