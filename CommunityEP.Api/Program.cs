using NLog.Extensions.Logging;
using Service.UtilityService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    //options.Filters.Add<ExceptionLogFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//调用配置SqlSugar
builder.Services.AddSqlSuGarConfig(builder.Configuration);
//注册EmailService
builder.Services.AddEmailConfig();
//注册Nlog日志
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddConsole();//输出到控制台
    logBuilder.AddNLog();//记录到文件夹
    //logBuilder.AddEventLog();//记录到系统日志
});
//注册HashService
builder.Services.AddHashService();
//注册JwtService
builder.Services.AddJwtService();
//注册鉴权
builder.Services.AddJwtAuthentication(builder.Configuration);
//注册授权
builder.Services.AddAuthorize();
//注册Mapper
builder.Services.AddAutoMapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//app.MapControllers();

app.Run();
