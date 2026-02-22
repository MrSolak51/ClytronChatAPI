using Microsoft.AspNetCore.Mvc;

namespace ChatUygulamasıBackend.Controllers
{
    [ApiController] // API davranışlarını (otomatik model doğrulama vb.) etkinleştirir
    [Route("api/[controller]")] // URL: api/user
    public class UserController : ControllerBase // View desteği olmayan, daha hafif olan base class
    {
        // Örnek: Sisteme bağlı kullanıcıları getir
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new List<string> { "Ahmet", "Mehmet", "Ayşe" };
            return Ok(users); // JSON olarak 200 OK döner
        }

        // Örnek: Kullanıcı detayını getir (api/user/ahmet)
        [HttpGet("{username}")]
        public IActionResult GetUserDetail(string username)
        {
            // Veritabanı sorgusu simülasyonu
            return Ok(new { Username = username, Status = "Online" });
        }
    }
}