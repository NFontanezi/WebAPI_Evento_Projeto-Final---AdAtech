using APIEvent.Core.Interface;
using APIEvent.Core.Service;
using APIEvent.Data.Infra.Repository;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using Projeto_WebAPI_Evento.Filters;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Adiciono o esquema de JWT Bearer
    .AddJwtBearer(options =>
    {
        //Adiciona as opções de validação
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true, // para inativar a validação do issuer, informar false e remover ValidIssuer
            ValidateAudience = true, // para inativar a validação da audience, informar false e remover ValidAudience
            ValidIssuer = "APIPessoa.com",
            ValidAudience = "APIEvent.com"
        };
    });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put *ONLY* your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddMvc(options =>
{
    options.Filters.Add<GeneralExceptionFilter>();
});


builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<ValidatePostiveInputActionFilter>();
builder.Services.AddScoped<ValidadePositiveAmountActionFilter>();


var app = builder.Build();

//padronização de data/hora
var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS },
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
