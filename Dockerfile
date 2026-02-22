# 1. Aşama: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# TÜM DOSYALARI KOPYALA (Klasör yapısını korumak için en garantisi)
COPY . .

# Proje dosyasını bul ve bağımlılıkları yükle
# Eğer dosya bir alt klasördeyse yolunu ona göre yazmalısın. 
# Örneğin: "ChatUygulamasıBackend/ChatUygulamasıBackend.csproj"
RUN dotnet restore "ChatUygulamasıBackend.csproj"

# Yayınla
RUN dotnet publish "ChatUygulamasıBackend.csproj" -c Release -o /app/publish

# 2. Aşama: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# DLL adının doğruluğundan emin ol (genelde proje adıyla aynıdır)
ENTRYPOINT ["dotnet", "ChatUygulamasıBackend.dll"]
