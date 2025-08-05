# Hướng dẫn sử dụng ShopChain cho người mới

## Giới thiệu
ShopChain là Web API xây dựng bằng ASP.NET Core 8.0 dùng để quản lý thông tin cửa hàng. Dữ liệu được lưu trữ qua Entity Framework Core với SQL Server.

## Yêu cầu cài đặt
1. **.NET 8 SDK**
2. **SQL Server** (có thể dùng phiên bản Express cho môi trường phát triển)

## Cài đặt
1. Clone mã nguồn về máy:
   ```bash
   git clone <url repository>
   ```
2. Mở file `ShopChain.Api/appsettings.json` và chỉnh chuỗi kết nối `DefaultConnection` cho phù hợp với SQL Server của bạn.
3. Từ thư mục gốc, chạy lệnh build:
   ```bash
   dotnet build
   ```
4. Tạo cơ sở dữ liệu và các bảng bằng Entity Framework Core qua Visual Studio 2022.
   Mở **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console)
   rồi chạy lệnh:
   ```powershell
   Update-Database
   ```

## Chạy API
1. Thực thi:
   ```bash
   dotnet run --project ShopChain.Api
   ```
2. Mặc định API sẽ chạy ở `https://localhost:7172` (hoặc một port tùy cấu hình). Swagger sẵn có tại `/swagger`.

## Thử nghiệm với Swagger
Sau khi chạy API, truy cập đường dẫn `https://localhost:7172/swagger` để xem tài liệu và thử gọi API trực tiếp. Swagger liệt kê ba nhóm chính:
- **Stores** (cửa hàng)
- **Employees** (nhân viên)
- **Provinces** (tỉnh/thành)

### Ví dụ các endpoint quan trọng
- `GET /api/stores` – lấy danh sách cửa hàng.
- `GET /api/stores/{id}` – chi tiết cửa hàng theo mã.
- `POST /api/stores` – thêm cửa hàng mới.
- `PUT /api/stores` – cập nhật thông tin cửa hàng.
- `DELETE /api/stores/{id}` – xoá cửa hàng.
- `GET /api/stores/provinces` – danh sách tỉnh/thành.
- `POST /api/stores/provinces` – tạo dữ liệu tỉnh mới từ API ngoài.
- `GET /api/employees` và các endpoint tương tự để quản lý nhân viên.
- `GET /api/provinces/all` – lấy toàn bộ tỉnh đã lưu trong hệ thống.

## Thao tác mẫu
Dưới đây là ví dụ quy trình thêm mới và xem danh sách cửa hàng:
1. Gửi `POST /api/stores` với nội dung JSON mô tả cửa hàng.
2. Sau khi thành công, gọi `GET /api/stores` để kiểm tra cửa hàng vừa tạo.

## Kết luận
Tài liệu này giới thiệu nhanh cách cài đặt và sử dụng ShopChain Web API cho người mới bắt đầu. Bạn có thể mở rộng thêm các bảng hoặc chức năng khác tuỳ nhu cầu.
