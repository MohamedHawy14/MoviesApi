
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.Helpers;
using MoviesApi.Services;
using System.Text;

namespace MoviesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => { options.UseSqlServer(builder.Configuration.GetConnectionString("Cs")); }
                );
            builder.Services.AddTransient<IGenreServices, GenreServices>();
            builder.Services.AddTransient<IMovieServices, MovieServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
               options =>
               {
                   options.SaveToken = false;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       ValidateIssuer = true,
                       ValidIssuer = builder.Configuration["JWT:issuerIP"],
                       ValidateAudience = true,
                       ValidAudience = builder.Configuration["JWT:audienceIP"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
                        ClockSkew = TimeSpan.Zero
                   };

               }
               );
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            #region Swagger
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movies",
                    Description = "Api",
                    TermsOfService=new Uri("https://www.google.com"),
                    Contact = new OpenApiContact() { Name="Hawy",Email="mohamedsaadfathey20192019@gmail.com",Url=new Uri("https://www.google.com") },
                    License=new OpenApiLicense() { Name="Mohamed Saad", Url= new Uri("https://www.google.com") }
                    

                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(e => e.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
