# 1. Chỉ định image nền có sẵn để build app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 2. Đặt thư mục làm việc trong image
WORKDIR /src

# 3. Copy toàn bộ code của bạn vào image
COPY . .

# 4. Build/publish project (xuất bản ra folder publish)
RUN dotnet publish "ShopChain.Api/ShopChain.Api.csproj" -c Release -o /app/publish

# 5. Dùng image nhẹ hơn chỉ để chạy (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# 6. Đặt thư mục làm việc trong image runtime
WORKDIR /app

# 7. Copy sản phẩm đã publish từ bước build qua
COPY --from=build /app/publish .

# 8. Mở port 8082 trong container để app lắng nghe request
EXPOSE 8082

# 9. Lệnh khởi động ứng dụng khi container chạy
ENTRYPOINT ["dotnet", "ShopChain.Api.dll"]
