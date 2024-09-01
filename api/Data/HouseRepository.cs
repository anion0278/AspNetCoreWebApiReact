using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailsDto?> Get(int id);
}


public class HouseRepository: IHouseRepository
{
    private readonly HouseDbContext _dbContext;

    public HouseRepository(HouseDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<List<HouseDto>> GetAll()
    {
        return await _dbContext.Houses
                .Select(he => new HouseDto(he.Id, he.Address, he.Country, he.Price))
                .ToListAsync();
    }

    public async Task<HouseDetailsDto?> Get(int id)
    {
        var h = await _dbContext.Houses.SingleOrDefaultAsync(h => h.Id == id);
        if (h is null) return null;
        return new HouseDetailsDto(h.Id, h.Address, h.Country, h.Price, h.Description, h.Photo);
    }

}