using Catalog.Infrastructure;
using IAM.Infrastructure;
using Workflow.Infrastructure;
using Workspace.Application;
using Workspace.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIamInfrastructure(builder.Configuration);
builder.Services.AddWorkspaceInfrastructure(builder.Configuration);
builder.Services.AddWorkspaceApplication();
builder.Services.AddWorkflowInfrastructure(builder.Configuration);
builder.Services.AddCatalogInfrastructure(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
