using Microsoft.AspNetCore.Builder;
using PostAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPostApp(builder.Configuration);

var app = builder.Build();

app.UsePostApp();

app.Run();
