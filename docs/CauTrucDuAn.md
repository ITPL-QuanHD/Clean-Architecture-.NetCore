# Cấu trúc project ShopChain

Tài liệu này mô tả chi tiết các thư mục và chức năng của từng tầng trong giải pháp.

## 1. ShopChain.Api
Tầng **API** phục vụ trình bầy và nhận/yêu cầu HTTP.
- Chứa các controller xử lý request.
- Khởi tạo và đăng ký các dịch vụ qua `DependencyInjection`.
- Tập tin `Program.cs` khai báo pipeline ASP.NET Core.

## 2. ShopChain.Application
Tầng **Application** chứa các use-case, logic nghiệp vụ để xử lý dữ liệu.
- Các Command/Query phục vụ pattern CQRS.
- DTOs để trao đổi dữ liệu qua lớp API.
- Các mapping giữa Entity và DTO.

## 3. ShopChain.Core
Tầng **Core** hay Domain.
- Định nghĩa các Entity (Store, Employee, Province, ...).
- Khai báo các Interface repository, model chung.
- Khối Dependency để các tầng khác implement.

## 4. ShopChain.Infrastructure
Tầng **Infrastructure** chứa các thực thi cụ thể.
- DbContext và migration Entity Framework Core.
- Triển khai các repository từ Interface trong Core.
- Triển khai các dịch vụ ngoại như gọi HTTP, lưu trữ file.

## 5. Sơ đồ tương tác
```
API → Application → Core → Infrastructure
```
Tầng API chỉ chủ yếu thu thập request và trả về kết quả.
Tầng Application thực hiện nghiệp vụ, gọi Repository/Service từ Infrastructure thông qua Interface của Core.
Tầng Infrastructure chịu trách nhiệm lưu trữ dữ liệu và giao tiếp ngoại vi.

## 6. Áp dụng Clean Architecture

Clean Architecture chia dự án thành nhiều vòng tròn đồng tâm, nơi mỗi vòng được phép phụ thuộc vào vòng bên trong nhưng không ngược lại. ShopChain áp dụng như sau:

- **Entities (Core)**: chứa các entity và interface thuần C#, không phụ thuộc framework.
- **Use Cases (Application)**: hiện thực các kịch bản nghiệp vụ (Command/Query, Service) và chỉ tham chiếu `Core`.
- **Interface Adapters (Api + Infrastructure)**: chuyển đổi dữ liệu, cung cấp controller, triển khai repository, mapping.
- **Frameworks & Drivers**: các chi tiết ngoài cùng như ASP.NET Core, EF Core, SQL Server.

Tất cả phụ thuộc đều hướng vào `Core`. Việc trích xuất interface ở `Core` giúp `Infrastructure` và `Api` có thể thay thế hay kiểm thử độc lập.

Sơ đồ tổng quát của clean architecture:
```
Entities -> Use Cases -> Interface Adapters -> Frameworks & Drivers
```
