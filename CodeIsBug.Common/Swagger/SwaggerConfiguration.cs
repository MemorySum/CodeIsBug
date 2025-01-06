using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeIsBug.Common.Swagger
{
    public static class SwaggerConfiguration
    {
        private static OpenApiSecurityScheme Scheme => new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "在下面输入框输入Token,不用输入Bearer[空格] ",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme,
            },
        };

        public static void Configure(SwaggerGenOptions option)
        {
            option.ResolveConflictingActions(apiDesc => apiDesc.First());
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            option.AddSecurityDefinition(Scheme.Reference.Id, Scheme);

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { Scheme, Array.Empty<string>() },
            });
        }
    }
}
