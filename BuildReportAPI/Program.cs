using BuildReportAPI.Services;
using BuildReportAPI.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IReportBuilder, ReportBuilder>();
builder.Services.AddTransient<IReporter, Reporter>();
builder.Services.AddSingleton<IReportCounter, ReportCounter>();
builder.Services.AddSingleton<IControlBuildCache, ControlBuildCache>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
