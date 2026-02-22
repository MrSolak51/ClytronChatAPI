using ChatUygulamasıBackend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace ChatUygulamasıBackend.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> OnlineUsers = new Dictionary<string, string>();
        private readonly ChatContext _context;

        public ChatHub(ChatContext context) { _context = context; }

        // KRİTİK EKSİK BURASIYDI: Mesaj Gönderme Metodu
        public async Task SendMessage(string user, string message)
        {
            // 1. Mesajı Veritabanına Kaydet (Persistence)
            var chatMessage = new Message
            {
                SenderId = user,
                Content = message,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // 2. Mesajı tüm bağlı istemcilere yayınla (Broadcast)
            // Angular'daki this.hubConnection.on('ReceiveMessage', ...) kısmını tetikler
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var username = httpContext.Request.Query["username"].ToString();

            if (!string.IsNullOrEmpty(username))
            {
                OnlineUsers[Context.ConnectionId] = username;
                await Clients.All.SendAsync("UserListUpdated", OnlineUsers.Values.Distinct().ToList());
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (OnlineUsers.Remove(Context.ConnectionId))
            {
                await Clients.All.SendAsync("UserListUpdated", OnlineUsers.Values.Distinct().ToList());
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}