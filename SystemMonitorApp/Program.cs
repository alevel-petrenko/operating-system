using SystemMonitor.Api.Background;
using SystemMonitor.Api.Services;
using SystemMonitor.Api.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddSingleton<IEnvironmentService, ProcessEnvironmentReader>();
builder.Services.AddSingleton<ILoggingService, FileLoggingService>();
builder.Services.AddSingleton<IProcessService, ProcessService>();
builder.Services.AddHostedService<ProcessBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AllowAngularApp");
app.MapHub<ProcessHub>("/hubs/processes");
app.MapControllers();

app.Run();
