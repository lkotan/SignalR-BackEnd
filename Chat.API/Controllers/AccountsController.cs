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
    public class AccountsController:ControllerRepository<IAccountService,AccountModel>
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("RoomJoin")]
        public async Task<IActionResult> RoomJoin([FromQuery] int roomId)
        {
            return Ok(await _service.RoomJoin(roomId));
        }

        [HttpPost("RoomLeft")]
        public async Task<IActionResult> RoomLeft()
        {
            return Ok(await _service.RoomLeft());
        }

    }
}
