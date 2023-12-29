using Microsoft.OpenApi.Models;

namespace YogaTime.Api.Infrastructures
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("YogaClass", new OpenApiInfo { Title = "Сущность занятия", Version = "v1" });
                c.SwaggerDoc("Room", new OpenApiInfo { Title = "Сущность залы", Version = "v1" });
                c.SwaggerDoc("Instructor", new OpenApiInfo { Title = "Сущность инструкторы", Version = "v1" });
                c.SwaggerDoc("Group", new OpenApiInfo { Title = "Сущность группы", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Сущность участники", Version = "v1" });
                c.SwaggerDoc("TimeTableItem", new OpenApiInfo { Title = "Сущность элемент расписания", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "YogaTime.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("YogaClass/swagger.json", "Занятия йогой");
                x.SwaggerEndpoint("Instructor/swagger.json", "Инструкторы");
                x.SwaggerEndpoint("Room/swagger.json", "Зал");
                x.SwaggerEndpoint("Group/swagger.json", "Группы");
                x.SwaggerEndpoint("Person/swagger.json", "Люди");
                x.SwaggerEndpoint("TimeTableItem/swagger.json", "Элемент расписания");
            });
        }
    }
}
