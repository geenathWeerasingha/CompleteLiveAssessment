using LiveAssessment.Data;
using LiveAssessment.Interfaces;
using LiveAssessment.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("CorsPolicy:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
                      policyBuilder =>
                      {
                          policyBuilder
                            .WithOrigins(allowedOrigins)   /*specify which origin allow the access resources*/
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowCredentials()     /*allow coookies to send with request*/
                            .WithExposedHeaders("Content-Disposition"); // Example of exposing specific headers

                          // If you need to allow credentials (cookies, etc.), uncomment the line below.
                          // .AllowCredentials();
                      });
});



 
builder.Services.AddControllers();

//Handled incomming request through controller classes.


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


////object-to-object mapping in .NET applications
//IServiceCollection is a part of the ASP.NET Core dependency injection framework. 

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IVariationRepository, VariationRepository>();
 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//used to genertae  info about endpoints in application.

builder.Services.AddSwaggerGen();

//Add swagger generator your services. Swagger (OpenAPI) is args tool for 
//documenting API. 

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var env = builder.Environment;


var app = builder.Build();

 




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("MyCorsPolicy");



app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

