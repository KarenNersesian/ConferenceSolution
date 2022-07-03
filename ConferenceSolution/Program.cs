using ConferenceShareModel.Db;
using ConferenceShareModel.Repos;
using ConferenceSolution.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IParticipantRepo, ParticipantRepo>();
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (Convert.ToBoolean(app.Configuration.GetSection("MockData").Value) == true)
{
    DataSeeder.Preperate(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
