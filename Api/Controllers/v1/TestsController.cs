using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TestsController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public TestsController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var message = new EmailMessage(new string[] {"p.tomasi@hotmail.ch"}, "Test Mail", "Test");
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }
    }

}
