using CRUD_Operation.Data;
using CRUD_Operation.DatabaseOperations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//
//
//
//
// PLEASE READ THE README SECTION OF THE GITHUB REPO FIRST .........
//
//
//
//
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Injecting_dbcontext

// here we are adding orm as service and configuring it to use sqlite
// The configuration can also do in the appsettings.json file, but for now I am doing it here

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=applicationDatabase.db "));

// because sqlite is a local database that runs within the filesystem, so properties like server and connection string are not required

builder.Services.AddScoped<DatabaseOperations>();
// adding DatabaseOperations as a service ....scoped so that there is only one instance created for the session

#endregion


SQLitePCL.Batteries.Init();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();