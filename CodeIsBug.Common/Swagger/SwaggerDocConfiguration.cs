using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeIsBug.Common.Swagger
{
    public static class SwaggerDocConfiguration
    {  
        public static void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("vdefault", new OpenApiInfo { Title = "Default API", Version = "v1" });

            var basePath = AppContext.BaseDirectory;

            // 获取根目录下，所有 xml 完整路径（注：并不会获取二级目录下的文件）
            var directoryInfo = new DirectoryInfo(basePath);
            var xmls = directoryInfo
                .GetFiles()
                .Where(f => f.Name.ToLower().EndsWith(".xml"))
                .Select(f => f.FullName)
                .ToList();

            // 添加注释文档
            foreach (var xml in xmls) options.IncludeXmlComments(xml, true);
        }
    }
}
