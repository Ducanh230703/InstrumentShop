using InstrumentShop.API.Services; // Thêm using cho các services
using InstrumentShop.Shared.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    // Configure DbContext
    builder.Services.AddDbContext<InstrumentShopDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("InstrumentShop.API")));

    // Register Services (Dependency Injection)
    builder.Services.AddScoped<AdminService>();
    builder.Services.AddScoped<CustomerService>();
    builder.Services.AddScoped<InstrumentService>();
    builder.Services.AddScoped<InstrumentCategoryService>();
    builder.Services.AddScoped<OrderService>();
    builder.Services.AddScoped<OrderDetailService>();
    builder.Services.AddScoped<PaymentService>();

    // Add CORS (Cross-Origin Resource Sharing) configuration (optional)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

    app.MapControllers();

    app.Run();           