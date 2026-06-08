using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudyBuddy.Application;
using StudyBuddy.Domain;
using StudyBuddy.Infrastructure;
using StudyBuddy.Infrastructure.Seeds;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

#region AddServices
builder.Services.AddInfratructureServices(builder.Configuration);
builder.Services.AddAplicationServices();
builder.Services.AddDomainServices(builder.Configuration);
#endregion
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyBuddy API", Version = "v1" });
    options.AddSignalRSwaggerGen();
    options.SupportNonNullableReferenceTypes();

    options.UseAllOfToExtendReferenceSchemas();
    options.SchemaFilter<SwaggerNullableFilter>();
 

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

#region Seed
using (var scope = app.Services.CreateScope())
{
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    var seeder = scope.ServiceProvider.GetRequiredService<ISeed>();
    var root = env.WebRootPath;
    await seeder.Seed(root); // تشغيل عملية التعبئة
}
#endregion

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.EnableFilter();


    });
}

//app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapHub<PrivateChatHub>("hubs/PrivateChatHub");
app.MapHub<PrivateChatHub>("hubs/GroupChatHub");
app.MapHub<PrivateChatHub>("hubs/NotificationHub");
app.MapControllers();

app.Run();
