using FastAQ.Models.Config.FAQMySQLConfig;
using FastAQ.Models.Config.ElasticSearchConfig;
using FastAQ.Services.AQDBServices;
using FastAQ.Services.ElasticSearchServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.Configure<ElasticSearchConfig>(builder.Configuration.GetSection("ElasticSearchConfig"));

builder.Services.AddDbContext<AQDBServices>();
builder.Services.AddSingleton<ElasticSearchServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin()    // 允许任何来源
              .AllowAnyMethod()    // 允许任何HTTP方法
              .AllowAnyHeader()    // 允许任何请求头
              .WithExposedHeaders("X-Custom-Header"); // 暴露自定义响应头
    });
});
var app = builder.Build();
app.UseCors("DevelopmentPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();
app.Run();


