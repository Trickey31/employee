using Microsoft.AspNetCore.Mvc;
using aspnetcore;
using aspnetcore.Application;
using aspnetcore.Domain;
using aspnetcore.Domain.Resource;
using aspnetcore.Domain.Service;
using aspnetcore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new LocalTimeZoneConverter());
    })
    .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context =>
{
    var error = context.ModelState.Values.SelectMany(x => x.Errors);

    return new BadRequestObjectResult(new BaseException()
    {
        StatusCode = (int)StatusCode.BadRequest,
        DevMessage = ResourceVN.Error_UserError,
        UserMessage = ResourceVN.Error_UserError,
        TraceId = "",
        MoreInfo = "",
        Errors = error
    }.ToString() ?? "");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];

// Khai báo Dependency Injection
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISettingService, SettingService>();




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder
        .WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
}
);

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowVueApp");

app.Run();
