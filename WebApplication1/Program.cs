using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebApp.DataBase;
using WebApp.Infrastructure;
using WebApp.Interface;
using WebApp.Model;
using WebApp.Repository;
using WebApp.Service.Implenment;
using WebApp.Service.Interface;
using WebApp.ViewModel.Mapper;
using WebApplication1.Extenxions;
using WebApplication1.Middlewares;
using WWebApplication1.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

builder.Services.AddAuthentication(opt =>
{
    //Đặt phương thức xác thực mặc định và phương thức xử lý jwt cho toàn bộ hệ thống
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    //configuration for Jwt Bearer option
    var key = builder.Configuration["Jwt:Secret"];
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,//skip checking who issued the token
        ValidateAudience = false,//skip checking who the token is for.
        ValidateLifetime = true,//Ensure the token is not expired.
        ValidateIssuerSigningKey = true,//Ensure the token's signature is vaild
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))//The key used to validate the token's signature
    };

});
var host = Dns.GetHostEntry(Dns.GetHostName());
foreach (var ip in host.AddressList)
{
    if (ip.AddressFamily == AddressFamily.InterNetwork)
    {
        Console.WriteLine("Backend IP: " + ip.ToString());
    }
}
builder.Services.AddAuthorization();
//Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.
#region Dependency Injection Container

#region Base Service
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService,CommentService>();
#endregion
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token dạng: Bearer <JWT>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});
//builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",          // ✅ khi chạy trên máy dev
            "http://10.110.17.175:5173"       // ✅ khi người khác dùng IP này truy cập FE
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // nếu bạn dùng cookie/login
    });
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7256);
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60); // thời gian chờ client (default là 30s)
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
});


var app = builder.Build();

app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<StatusCodeMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseWebSockets();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CommentHub>("/commentHub").RequireCors("AllowAll"); ;
});
//app.UseHttpsRedirection();



app.Run();
