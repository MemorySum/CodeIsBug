using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CodeIsBug.Common.Components.DependencyInjection;

/// <summary>
/// autofac注入
/// </summary>
public class AutofacModuleRegister : Module
{
    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        var assemblesRepositoryNoInterfaces = Assembly.Load("CodeIsBug.Repository");
        var assemblesServicesNoInterfaces = Assembly.Load("CodeIsBug.Service");
        var assemblesMoelNoInterfaces = Assembly.Load("CodeIsBug.Models");
        builder.RegisterAssemblyTypes(assemblesMoelNoInterfaces)
            .Where(c => c.IsClass);
        builder.RegisterAssemblyTypes(assemblesServicesNoInterfaces)
            .Where(c => c.IsClass);
        builder.RegisterAssemblyTypes(assemblesRepositoryNoInterfaces)
            .Where(c => c.IsClass);

    }
}