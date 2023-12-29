using Microsoft.EntityFrameworkCore;
using YogaTime.Api.Infrastructures;
using YogaTime.Context;
using YogaTime.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();

// � ���� ������ ���� - ��� ���������� ���
//builder.Services.AddLoggerRegistr();

builder.Services.AddDependencies();


var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<TimeTableContext>(options => options.UseSqlServer(conString),
    ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
