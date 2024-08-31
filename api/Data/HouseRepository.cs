using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<List<HouseDto>> GetAll();
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
}