using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Training.EFCore;
using Training.Services.Service;

namespace Training.API;

public class AutofacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var IAppServices = Assembly.Load(typeof(JWTService).Assembly.ToString());
        var AppServices = Assembly.Load(typeof(JWTService).Assembly.ToString());
        var IRespotry = typeof(IRespotry<>);
        var Respotry =  typeof(Respotry<>);
        builder.RegisterAssemblyTypes(IAppServices, AppServices).Where(x=>x.Name.EndsWith("Service")).AsImplementedInterfaces();
        builder.RegisterGeneric(Respotry).As(typeof(IRespotry<>)).AsImplementedInterfaces();
    }
}
