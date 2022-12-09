using IRepository;
using IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using SqlSugar;
using SqlSugar.IOC;

namespace Service.UtilityService
{
    public static class ConfigServices
    {
        public static void AddSqlSuGarConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlSugarConfig = configuration.GetSection("ConnectionStrings");
            services.AddSqlSugar(new List<IocConfig>()
            {
                new IocConfig()
                {
                    ConfigId = "0",
                    ConnectionString = sqlSugarConfig["SqlServer"],
                    DbType = IocDbType.SqlServer,
                    IsAutoCloseConnection = false,
                    SlaveConnectionConfigs = null,
                }
            });
            SqlSugarScope sqlSugarScope = new SqlSugarScope(new List<ConnectionConfig>()
            {
                new ConnectionConfig()
                {
                    ConfigId = "0",
                    ConnectionString = sqlSugarConfig["SqlServer"],
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = false,
                    SlaveConnectionConfigs = null,
                }
            });//使用DbScoped.Sugar就能直接注入了
            services.AddSingleton<ISugarUnitOfWork<RepositoryDbContext>>(
                new SugarUnitOfWork<RepositoryDbContext>(sqlSugarScope));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEntryRecordRepository, EntryRecordRepository>();
            services.AddScoped<IEntryRecordService, EntryRecordService>();
            services.AddScoped<IOutRecordRepository, OutRecordRepository>();
            services.AddScoped<IOutRecordService, OutRecordService>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IEscalationInfoRepository, EscalationInfoRepository>();
            services.AddScoped<IEscalationInfoService, EscalationInfoService>();
            services.AddScoped<IEpidemicInfoRepository, EpidemicInfoRepository>();
            services.AddScoped<IEpidemicInfoService, EpidemicInfoService>();
        }

        public static void AddEmailConfig(this IServiceCollection services)
        {
            services.AddScoped<IEmailService,EmailService>();
        }

        public static void AddHashService(this IServiceCollection services)
        {
            services.AddScoped<HashService>();
        }

        public static void AddVisitApiService(this IServiceCollection services)
        {
            services.AddScoped<VisitApiService>();
        }

        public static void AddJwtService(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtConfig");
            services.AddAuthentication("Jwt")
                .AddJwtBearer("Jwt", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtSection["Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(jwtSection["Secret"]))
                    };
                });
        }

        public static void AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var cookieSection = configuration.GetSection("CookieConfig");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = cookieSection["Name"];
                    options.Cookie.HttpOnly = bool.Parse(cookieSection["HttpOnly"]);
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                    //options.SlidingExpiration = true;
                    //options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = cookieSection["LoginPath"];
                    options.AccessDeniedPath = cookieSection["AccessDeniedPath"];
                    options.LogoutPath = cookieSection["LogoutPath"];
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                });
        }

        public static void AddAuthorize(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAssertion(ctx =>
                    {
                        if (ctx.User.Claims.Count() > 0 && ctx.User.Claims.First(c => c.Type.Contains("role")).Value == "管理员")
                            return true;
                        else
                            return false;
                    });
                });
                options.AddPolicy("Ordinary", policy =>
                {
                    policy.RequireAssertion(ctx =>
                    {
                        if (ctx.User.Claims.Count() > 0 && (ctx.User.Claims.First(c => c.Type.Contains("role")).Value == "普通用户" ||
                                                            ctx.User.Claims.First(c => c.Type.Contains("role")).Value == "管理员"))
                            return true;
                        else
                            return false;
                    });
                });
            });
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperService));
        }

        public static void AddRedisCache(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });
        }
    }
}
