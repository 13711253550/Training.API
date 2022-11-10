using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Training.EFCore;
using Training.EFCore.Context;
using Training.Services.Service;

var builder = WebApplication.CreateBuilder(args);


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

//Event�¼�:���Bearer��֤ʧ�ܵ���
//builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    options.Events = new JwtBearerEvents
//    {
//        OnAuthenticationFailed = context =>
//        {
//            //���Token����
//            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
//            {
//                //����JWTService���� ˢ��Token
//                var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJWTService>();
//                var token = jwtService.GetToken(context.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
//                //���µ�Tokenд�뵽��Ӧͷ��
//                context.Response.Headers.Add("Authorization", "Bearer " + token);
//            }
//            return Task.CompletedTask;
//        }
//    };
//});
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
    builder.Services.AddDbContext<SqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("MySql"), MySqlServerVersion.LatestSupportedServerVersion));
}


//AutoFac�Զ�ע��
var buliders = new ContainerBuilder();
buliders.RegisterAssemblyTypes(typeof(OrderService).Assembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerDependency();
//���� ע��
buliders.RegisterGeneric(typeof(Respotry<>)).As(typeof(IRespotry<>)).InstancePerDependency();


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
