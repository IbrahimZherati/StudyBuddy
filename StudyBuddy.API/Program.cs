using Microsoft.OpenApi.Models;
using StudyBuddy.Application;
using StudyBuddy.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

#region AddServices
builder.Services.AddInfratructureServices(builder.Configuration);
builder.Services.AddAplicationServices();
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

// app.UseCors(policy =>
//     policy.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader());

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapHub<PrivateChatHub>("hubs/PrivateChatHub");
app.MapHub<PrivateChatHub>("hubs/GroupChatHub");
app.MapControllers();

app.Run();
