using ChatUygulamasıBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatUygulamasıBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ChatContext _context;

        public MessagesController(ChatContext context)
        {
            _context = context;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            // Son 50 mesajı tarihe göre sıralayıp getiriyoruz
            return await _context.Messages
                .OrderBy(m => m.Timestamp)
                .Take(50)
                .ToListAsync();
        }
    }
}