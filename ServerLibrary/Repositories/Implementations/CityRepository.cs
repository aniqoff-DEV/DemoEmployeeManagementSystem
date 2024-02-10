using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class CityRepository : IGenericRepository<City>
    {
        private readonly AppDbContext appDbContext;

        public CityRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var city = await appDbContext.Cities.FindAsync(id);
            if (city is null) return NotFound();

            appDbContext.Cities.Remove(city);
            await Commit();
            return Success();
        }

        public async Task<List<City>> GetAll() =>
            await appDbContext.Cities.ToListAsync();

        public async Task<City> GetById(int id) => await appDbContext.Cities.FindAsync(id);

        public async Task<GeneralResponse> Insert(City item)
        {
            if (!await Checkname(item.Name!)) return new GeneralResponse(false, "City is already added");
            appDbContext.Cities.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(City item)
        {
            var city = await appDbContext.Cities.FindAsync(item.Id);
            if (city is null) return NotFound();

            city.Name = item.Name;
            await Commit();
            return Success();
        }

        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.Cities.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, deaprtment not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
