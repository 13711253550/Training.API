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
using Autofac.Core;
using Aop.Api;
using Hangfire;
using Hangfire.MemoryStorage;
using CSRedis;
using Microsoft.Extensions.Configuration;
using Training.Domain.Entity.Seckill;
using Aop.Api.Domain;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAutofac(new AutofacServiceProviderFactory());

#region JWT
//����JWT��֤
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        //����JWT��֤����
        //����TokenValidationParameters ���������������Token����֤������
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //�Ƿ���֤������
            ValidateIssuer = true,
            //������
            ValidIssuer = "http://localhost:5041",
            //�Ƿ���֤������
            ValidateAudience = true,
            //������
            ValidAudience = "http://localhost:5041",
            //�Ƿ���֤��ȫ��Կ
            ValidateIssuerSigningKey = true,
            //��ȫ��Կ
            //�����ļ��е���Կ

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSetting:Token"])),
            //�Ƿ���֤����ʱ��
            ValidateLifetime = true,
            //����ʱ��
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

//Event�¼�: ���Bearer��֤ʧ�ܵ���JWTService�ķ���
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            //���Token����
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //����JWTService���� ˢ��Token
                // var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJWTService>();
                
                //ˢ��Token
                context.Response.Headers.Add("Authorization", "-1");
                //��¶ǰ���Զ���ͷ���ֶ�
                context.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
            }
            return Task.CompletedTask;
        }
    };
});
// Add services to the container.
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "����", Description = "API����" });
    options.OrderActionsBy(o => o.RelativePath);
    options.IncludeXmlComments("NoName.Core.xml", true);
});
builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.PropertyNamingPolicy = null);
//builder.Services.AddCors((x) => { x.AddPolicy("kuayu", d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());});

<<<<<<< HEAD
#region ��ʱ����
=======
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3
builder.Services.AddHangfire(configura =>
{
    configura.UseStorage(new MemoryStorage());
    //ÿ��30����ִ��һ��
    //����SeckillService����
    RecurringJob.AddOrUpdate<SeckillService>(x => x.SetRedis(), "0 0/1 * * * ? ");
<<<<<<< HEAD
    RecurringJob.AddOrUpdate<SeckillService>(x => x.RedisToDB(), "0 0/30 * * * ? ");
});
#endregion
=======
});
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3

#region Redis
////ע��CSRedis����
//string strRedis = builder.Configuration["Redis:Host"];
//var cs = new CSRedisClient(strRedis);//ʵ����CSRedis�ͻ���
//RedisHelper.Initialization(cs);//��ʼ��RedisHelper��ʵ��
#endregion

#region ���ݿ�
//����
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
    });
});

if (builder.Configuration["SQL"] == "MySQL")
{
    builder.Services.AddDbContext<SqlContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
}
else
{
    //ʹ��MySql���ݿ�
    //����MySqlServerVersion.LatestSupportedServerVersion
    //���������������MySql�İ汾��
    builder.Services.AddDbContext<SqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 22))));
}
#endregion

#region SignalR
//ʹ��SignalR
builder.Services.AddSignalR();
#endregion

#region ֧����֧��
//֧����֧��
builder.Services.Configure<AlipayConfig>(builder.Configuration.GetSection("Alipay"));
#endregion

#region Autofac/AutoMapper�Զ�ע��
//AutoFac�Զ�ע��
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    //����:builder.RegisterAssemblyTypes() �������������ע������е����
    builder.RegisterModule(new AutofacModuleRegister());
});

//autoMapper �Զ�ӳ��
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "��Ŀ���� V1");

    s.RoutePrefix = "";//����swagger·��\
});
app.UseCors();

app.UseHangfireDashboard();//���ú�̨�Ǳ���

app.UseHangfireServer();//��ʼʹ��Hangfire����

//SignlR
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<SignaIRChat>("/chatHub");
//});

app.MapHub<SignaIRChatService>("/chatHub");

//ע�⣺app.UseAuthentication() ������ app.UseAuthorization() ֮ǰ����
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
