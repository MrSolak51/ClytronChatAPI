# 1. Aşama: Build (Derleme)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve bağımlılıkları yükle (Restore)
# Not: .csproj dosyanın adı farklıysa onu yazmalısın.
COPY ["ChatUygulamasıBackend.csproj", "./"]
RUN dotnet restore "./ChatUygulamasıBackend.csproj"

# Tüm dosyaları kopyala ve yayınla (Publish)
COPY . .
RUN dotnet publish "./ChatUygulamasıBackend.csproj" -c Release -o /app/publish

# 2. Aşama: Run (Çalıştırma)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render'ın dinamik PORT atamasını yakalamak için
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ChatUygulamasıBackend.dll"]