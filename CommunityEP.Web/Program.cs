using Service.UtilityService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//ע��VisitApiService
builder.Services.AddVisitApiService();
//ע��Redis����
builder.Services.AddRedisCache(builder.Configuration);
//ע���Ȩ
builder.Services.AddCookieAuthentication(builder.Configuration);
//ע����Ȩ
builder.Services.AddAuthorize();
//ע��Hash
builder.Services.AddHashService();
//ע��SqlSugar
builder.Services.AddSqlSuGarConfig(builder.Configuration);
//ע��JwtService
builder.Services.AddJwtService();
//ע��Mapper
builder.Services.AddAutoMapper();
VisitApiService.Url = builder.Configuration["ApiAddress:Address"];

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
