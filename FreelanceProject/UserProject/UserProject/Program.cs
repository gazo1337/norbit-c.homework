using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserDomain;
using UserInfrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

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

builder.Services.AddDbContext<UserContext>(b => b.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(myAllowSpecificOrigins);

var usersGroup = app.MapGroup("user/");

//Вход в систему
usersGroup.MapPost("/login", (UserContext db, string login, string password) =>
{
    var users = db.Users.ToList();
    var logUser = users.FirstOrDefault(p => p.Login == login && p.Password == password);
    if (logUser is null)
        return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, logUser.Login) };
    // создаем JWT-токен
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJwt,
        username = logUser.Login,
    };

    return Results.Json(response);
});
//Вывод всех пользователей
usersGroup.MapGet("/all", [Authorize] (UserContext db) =>
{
    var users = db.Users.ToList();
    return Results.Ok(users);
});
//Добавление пользователя
usersGroup.MapPost("create/", [Authorize] (User user, UserContext db) =>
{
        var users = db.Users.ToList();
        if (user == null)
        {
            return Results.BadRequest();
        }

        if (users.Count != 0)
        {
            user.Id = db.Users.Max(x => x.Id) + 1;
            db.Users.Add(user);
            db.SaveChanges();
            return Results.Ok(user);
        }

        db.Users.Add(user);
        db.SaveChanges();
        return Results.Ok(user);
});
//Удаление пользователя
usersGroup.MapDelete("delete/", [Authorize] (int userId, UserContext db) =>
{
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        db.Users.Remove(user);
        db.SaveChanges();
        Results.Ok("Deleted!");
});
//Поиск по id
usersGroup.MapGet("getid/", [Authorize] (int userId, UserContext db) =>
{
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        return Results.Ok(user);
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "jxlfgkfngiscjsj54tsgh!3rty";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}