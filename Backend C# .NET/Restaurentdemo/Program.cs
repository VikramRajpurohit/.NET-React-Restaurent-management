using Restaurentdemo.Models;
using Restaurentdemo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RestaurantDBContext>(
    builder.Configuration.GetSection("RestaurantDatabase"));
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<FoodItemService>();
builder.Services.AddSingleton<OrderDetailService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin=>true).AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
