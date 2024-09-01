using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HouseDbContext>();
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.MapGet("/houses", async (IHouseRepository houseRepository) =>
    await houseRepository.GetAll()).Produces<HouseDto[]>(StatusCodes.Status200OK);

app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository repository) =>
{
    var house = await repository.Get(houseId);
    if (house is null) return Results.Problem($"Could not find house with ID {houseId}", statusCode: 404);
    return Results.Ok(house);
}).ProducesProblem(404).Produces<HouseDetailsDto>(StatusCodes.Status200OK);

app.MapPost("/houses", async ([FromBody] HouseDetailsDto dto, IHouseRepository repository) =>
{
    var newHouse = repository.Add(dto);
    return Results.Created($"/house/{newHouse.Id}", newHouse);
}.Produces<HouseDetailsDto>(StatusCodes.Status201Created);

app.MapPut("/houses/", async ([FromBody] HouseDetailsDto dto, IHouseRepository repository) =>
{
    if (await repository.Get(dto.Id) is null)
    {
        return Results.Problem($"House wit ID {dto.Id} not found", statusCode: 404);
    }
    var updatedHouse = await repository.Update(dto);
    return Results.Ok(dto);
}.ProducesProblem(404).Produces<HouseDetailsDto>(StatusCodes.Status200OK);

app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository repository) =>
{
    if (await repository.Get(dto.Id) is null)
    {
        return Results.Problem($"House wit ID {dto.Id} not found", statusCode: 404);
    }
    await repository.Delete(houseId);
    return Results.Ok();
}.ProducesProblem(404).Produces<HouseDetailsDto>(StatusCodes.Status200OK);



app.Run();
