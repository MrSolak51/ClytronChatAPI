using ChatUygulamasıBackend.Hubs;
using ChatUygulamasıBackend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS Politikasını Tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("https://clytronchat.onrender.com") // Angular'ın adresi
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR için kritik
    });
});

// 1. Önce servisleri ekle
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChatContext>();
    db.Database.Migrate();
}
// 3. Middleware Pipeline (SIRALAMA ÇOK ÖNEMLİ)

// Geliştirme aşamasında HTTPS yönlendirmesi CORS el sıkışmasını bozabilir.
// Bu yüzden sadece üretim (Production) ortamında aktif ediyoruz.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

// UseCors mutlaka UseRouting'den SONRA, MapHub'dan ÖNCE gelmeli
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

// SignalR Hub Endpoint'i
app.MapHub<ChatHub>("/chatHub");

app.Run();