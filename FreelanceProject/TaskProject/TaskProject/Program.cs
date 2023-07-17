using Microsoft.EntityFrameworkCore;
using TaskDomain;
using TaskInfrastructure;
using static TaskInfrastructure.TaskContext;
using TaskProject.Routes;

var builder = WebApplication.CreateBuilder(args);

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<TaskContext>(b => b.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskManager, TaskManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors(myAllowSpecificOrigins);
app.AddTaskRouter();

app.UseHttpsRedirection();

app.Run();
