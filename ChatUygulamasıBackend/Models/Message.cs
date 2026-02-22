namespace ChatUygulamasıBackend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }

        // Soru işareti (?) ekleyerek bu alanın boş (null) olabileceğini belirttik
        public string? ReceiverId { get; set; }

        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
