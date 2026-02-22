namespace ChatUygulamasıBackend.Models
{
    public class User
    {
        // Birincil Anahtar (Primary Key)
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Kullanıcının görünen adı
        public string Username { get; set; } = string.Empty;

        // SignalR için kritik: Kullanıcının o anki aktif bağlantı ID'si
        // Bir kullanıcı birden fazla cihazdan bağlanırsa bu bir liste (List<string>) olabilir.
        public string? ConnectionId { get; set; }

        // Durum yönetimi (State Management)
        public bool IsOnline { get; set; }

        public DateTime LastSeen { get; set; } = DateTime.Now;

        // İlişkisel veri (Navigation Property)
        // Kullanıcının gönderdiği mesajlara hızlı erişim sağlar
        public virtual ICollection<Message>? SentMessages { get; set; }
    }
}