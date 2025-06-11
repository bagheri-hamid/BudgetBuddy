using BudgetBuddy.Extensions;
using BudgetBuddy.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args)
    .AddSerilog();

// Add services to the container.
builder.Services.AddControllers().AddApplicationPart(typeof(BudgetBuddy.Api.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApisVersioning();
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApiBehavior();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.UseUserIdLogContext();
app.UseRequestBodyLogging();
app.UseSerilogRequestLogging(option => { option.IncludeQueryInRequestPath = true; });

app.MapControllers();

app.Run();