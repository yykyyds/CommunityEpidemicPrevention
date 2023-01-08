using NLog.Extensions.Logging;
using Service.UtilityService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    //options.Filters.Add<ExceptionLogFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//��������SqlSugar
builder.Services.AddSqlSuGarConfig(builder.Configuration);
//ע��EmailService
builder.Services.AddEmailConfig();
//ע��Nlog��־
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddConsole();//���������̨
    logBuilder.AddNLog();//��¼���ļ���
    //logBuilder.AddEventLog();//��¼��ϵͳ��־
});
//ע��HashService
builder.Services.AddHashService();
//ע��JwtService
builder.Services.AddJwtService();
//ע���Ȩ
builder.Services.AddJwtAuthentication(builder.Configuration);
//ע����Ȩ
builder.Services.AddAuthorize();
//ע��Mapper
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
