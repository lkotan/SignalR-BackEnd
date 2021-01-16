using AutoMapper;
using Chat.API.Repositories;
using Chat.Business.Abstract;
using Chat.Entities;
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
    public class MessagesController:ControllerRepository<IMessageService,MessageModel>
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet("RoomMessages")]
        public async Task<IActionResult> GetRoomMessages([FromQuery] int roomId)
        {
            return Ok(await _service.GetRoomMessagesAsync(roomId));
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertMessage([FromBody] ListMessageModel model)
        {
            return Ok(await _service.InsertMessageAsync(model));
        }
    }
}
