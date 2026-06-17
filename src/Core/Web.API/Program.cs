using Catalog.Infrastructure;
using IAM.Application;
using IAM.Infrastructure;
using IAM.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workflow.Application;
using Workflow.Infrastructure;
using Workflow.Presentation;
using Workspace.Application;
using Workspace.Infrastructure;
using Workspace.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddIamInfrastructure(builder.Configuration);
builder.Services.AddIamApplication();
builder.Services.AddIamPresentation();

builder.Services.AddWorkflowApplication();
builder.Services.AddWorkflowInfrastructure(builder.Configuration);
builder.Services.AddWorkflowPresentation();

builder.Services.AddWorkspaceInfrastructure(builder.Configuration);
builder.Services.AddWorkspaceApplication();
builder.Services.AddWorkspacePresentation();

builder.Services.AddCatalogInfrastructure(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
