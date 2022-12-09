using Service.UtilityService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//×¢²áVisitApiService
builder.Services.AddVisitApiService();
//×¢²áRedis»º´æ
builder.Services.AddRedisCache(builder.Configuration);
//×¢²á¼øÈ¨
builder.Services.AddCookieAuthentication(builder.Configuration);
//×¢²áÊÚÈ¨
builder.Services.AddAuthorize();
//×¢²áHash
builder.Services.AddHashService();
//×¢²áSqlSugar
builder.Services.AddSqlSuGarConfig(builder.Configuration);
//×¢²áJwtService
builder.Services.AddJwtService();
//×¢²áMapper
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
