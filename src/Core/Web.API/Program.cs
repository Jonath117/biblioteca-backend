using Catalog.Infrastructure;
using IAM.Application;
using IAM.Infrastructure;
using IAM.Presentation;
using Workflow.Application;
using Workflow.Infrastructure;
using Workspace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddIamInfrastructure(builder.Configuration);
builder.Services.AddIamApplication();
builder.Services.AddIamPresentation();

builder.Services.AddWorkflowApplication();
builder.Services.AddWorkflowInfrastructure(builder.Configuration);

builder.Services.AddWorkspaceInfrastructure(builder.Configuration);
builder.Services.AddCatalogInfrastructure(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API v1");
    });
}

app.UseCors("FrontendPolicy");

app.MapControllers();

app.Run();