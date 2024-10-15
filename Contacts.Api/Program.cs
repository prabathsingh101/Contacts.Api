
using Contacts.Api.Exceptions;
using Contacts.Api.Services;
using Contacts.Api.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ContactService>();
//builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
//builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").
                                              AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowAnyOrigin();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseExceptionHandler(e => { });
app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.MapControllers();
app.UseCors("MyAllowSpecificOrigins");
app.Run();
