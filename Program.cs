using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MVCmodel.Models;
using Microsoft.AspNetCore.Identity;
using MVCmodel.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ cho MVC (controllers với views)
builder.Services.AddControllersWithViews();

// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KhiemConnecion"));
});

// Cấu hình Identity (cho người dùng và phân quyền)
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Cấu hình IdentityOptions cho mật khẩu, khóa tài khoản, và cài đặt người dùng
builder.Services.Configure<IdentityOptions>(options =>
{

    // Cài đặt người dùng
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Đăng ký AutoMapper (nếu bạn sử dụng AutoMapper)
builder.Services.AddAutoMapper(typeof(Program));

// Xây dựng ứng dụng
var app = builder.Build();

// Seed Roles (thêm roles mặc định vào cơ sở dữ liệu nếu cần)
SeedRolesAndUsers.SeedRolesAndUsersMethod(app);

// Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Cấu hình Swagger cho môi trường phát triển
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Cấu hình trang lỗi trong môi trường sản xuất
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cấu hình các middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Cấu hình các middleware bảo mật
app.UseAuthentication();
app.UseAuthorization();

// Định nghĩa các route cho các controller
app.MapControllerRoute(
    name: "contact",
    pattern: "Contact",
    defaults: new { controller = "Contact", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
