using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationBidExtensions
{
    public static void MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("/house/{houseId:int}/bids", async (int houseId, IBidRepository bidRepository, IHouseRepository houseRepository) =>
        {
            if (await houseRepository.Get(houseId) is null) return Results.Problem($"House wit ID {houseId} not found", statusCode: 404);
            var bids = await bidRepository.Get(houseId);
            return Results.Ok(bids);
        }).ProducesProblem(404)
        .Produces<BidDto[]>(StatusCodes.Status200OK);

        app.MapPost("/house/{houseId:int}/bids", async (int houseId, [FromBody] BidDto bid, IBidRepository bidRepository, IHouseRepository houseRepository) =>
        {
            if (bid.HouseId != houseId) return Results.Problem($"House with ID {houseId} not found", statusCode: StatusCodes.Status400BadRequest);

            if (bid.Amount <= await bidRepository.GetHighestBidAmountForHouse(houseId)) return Results.ValidationProblem(
                new Dictionary<string, string[]>(){
                    {nameof(bid.Amount), ["Bid amount should greater than previous bids."]}
                });

            if (!MiniValidator.TryValidate(bid, out var errors))
                return Results.ValidationProblem(errors);

            if (await houseRepository.Get(houseId) is null) return Results.Problem($"House wit ID {houseId} not found", statusCode: 404);

            var bidCreated = await bidRepository.Add(bid);
            return Results.Created($"/house/{bidCreated.HouseId}/bids", bidCreated);
        }).ProducesProblem(404)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces<BidDto>(StatusCodes.Status200OK);
    }
}