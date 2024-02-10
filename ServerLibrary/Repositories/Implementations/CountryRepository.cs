using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class CountryRepository : IGenericRepository<Country>
    {
        private readonly AppDbContext appDbContext;

        public CountryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var country = await appDbContext.Countries.FindAsync(id);
            if (country is null) return NotFound();

            appDbContext.Countries.Remove(country);
            await Commit();
            return Success();
        }

        public async Task<List<Country>> GetAll() =>
            await appDbContext.Countries.ToListAsync();

        public async Task<Country> GetById(int id) => await appDbContext.Countries.FindAsync(id);

        public async Task<GeneralResponse> Insert(Country item)
        {
            if (!await Checkname(item.Name!)) return new GeneralResponse(false, "Country is already added");
            appDbContext.Countries.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Country item)
        {
            var country = await appDbContext.Countries.FindAsync(item.Id);
            if (country is null) return NotFound();

            country.Name = item.Name;
            await Commit();
            return Success();
        }

        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.Countries.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, department not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
