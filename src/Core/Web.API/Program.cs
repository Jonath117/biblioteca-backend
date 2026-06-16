using Catalog.Infrastructure;
using IAM.Application;
using IAM.Infrastructure;
using IAM.Presentation;
using Workflow.Infrastructure;
using Workspace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIamInfrastructure(builder.Configuration);
builder.Services.AddIamApplication();
builder.Services.AddIamPresentation();

builder.Services.AddWorkspaceInfrastructure(builder.Configuration);
builder.Services.AddWorkflowInfrastructure(builder.Configuration);
builder.Services.AddCatalogInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options => 
    {
        // Define la ruta del endpoint OpenAPI
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API v1");
    });
}

app.UseCors("FrontendPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
