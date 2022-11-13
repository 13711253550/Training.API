using Autofac;
using AutoMapper;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Training.EFCore;
using Training.EFCore.Context;
using Training.Services.Service;
using Training.Domain.Entity;
using Training.Domain.Shard;
using Training.API;
using Newtonsoft.Json.Linq;
using Training.Services;
using Training.Services.IService;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAutofac(new AutofacServiceProviderFactory());


//开启JWT认证
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        //配置JWT认证参数
        //解释TokenValidationParameters 这个类是用来配置Token的验证参数的
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //是否验证发行人
            ValidateIssuer = true,
            //发行人
            ValidIssuer = "http://localhost:5041",
            //是否验证接收人
            ValidateAudience = true,
            //接收人
            ValidAudience = "http://localhost:5041",
            //是否验证安全密钥
            ValidateIssuerSigningKey = true,
            //安全密钥
            //配置文件中的密钥

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSetting:Token"])),
            //是否验证过期时间
            ValidateLifetime = true,
            //过期时间
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

//Event事件: 如果Bearer认证失败调用
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            //如果Token过期
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Authorization", "-1");
                //暴露前端自定义头部字段
                context.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
            }
            return Task.CompletedTask;
        }
    };
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "电商", Description = "API描述" });
    options.OrderActionsBy(o => o.RelativePath);
    options.IncludeXmlComments("NoName.Core.xml", true);
});
builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddCors((x) => { x.AddPolicy("kuayu", d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); });
if (builder.Configuration["SQL"] == "MySQL")
{
    builder.Services.AddDbContext<SqlContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
}
else
{
    //使用MySql数据库
    //解释MySqlServerVersion.LatestSupportedServerVersion
    //这个类是用来配置MySql的版本的
    builder.Services.AddDbContext<SqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 22))));
}

//AutoFac自动注册
//解释:builder.Host.UseServiceProviderFactory()
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    //解释:builder.RegisterAssemblyTypes() 这个方法是用来注册程序集中的类的
    builder.RegisterModule(new AutofacModuleRegister());
});

//autoMapper 自动映射
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "项目名称 V1");

    s.RoutePrefix = "";//请求swagger路径\
});
app.UseCors("kuayu");
//}
//注意：app.UseAuthentication() 必须在 app.UseAuthorization() 之前调用
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
