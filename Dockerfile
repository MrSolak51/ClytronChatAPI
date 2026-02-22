# 1. Aşama: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Önce tüm klasör yapısını içeri alalım
COPY . .

# Proje dosyasına giden tam yolu belirtiyoruz
# ChatUygulamasıBackend klasörünün içindeki .csproj'u hedef al
RUN dotnet restore "ChatUygulamasıBackend/ChatUygulamasıBackend.csproj"

# Publish yaparken de tam yolu kullanıyoruz
RUN dotnet publish "ChatUygulamasıBackend/ChatUygulamasıBackend.csproj" -c Release -o /app/publish

# 2. Aşama: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Derlenen dosyaları kopyala
COPY --from=build /app/publish .

# Render Port Ayarı
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Çıktı DLL'inin adını kontrol et (Genelde proje adıyla aynıdır)
ENTRYPOINT ["dotnet", "ChatUygulamasıBackend.dll"]
