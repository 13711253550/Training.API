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

//Event�¼�: ���Bearer��֤ʧ�ܵ���
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            //���Token����
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Authorization", "-1");
                //��¶ǰ���Զ���ͷ���ֶ�
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
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "����", Description = "API����" });
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
    //ʹ��MySql���ݿ�
    //����MySqlServerVersion.LatestSupportedServerVersion
    //���������������MySql�İ汾��
    builder.Services.AddDbContext<SqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 22))));
}

//AutoFac�Զ�ע��
//����:builder.Host.UseServiceProviderFactory()
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    //����:builder.RegisterAssemblyTypes() �������������ע������е����
    builder.RegisterModule(new AutofacModuleRegister());
});

//autoMapper �Զ�ӳ��
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "��Ŀ���� V1");

    s.RoutePrefix = "";//����swagger·��\
});
app.UseCors("kuayu");
//}
//ע�⣺app.UseAuthentication() ������ app.UseAuthorization() ֮ǰ����
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
