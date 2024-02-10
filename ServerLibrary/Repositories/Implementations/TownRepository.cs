using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class TownRepository : IGenericRepository<Town>
    {
        private readonly AppDbContext appDbContext;

        public TownRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var town = await appDbContext.Towns.FindAsync(id);
            if (town is null) return NotFound();

            appDbContext.Towns.Remove(town);
            await Commit();
            return Success();
        }

        public async Task<List<Town>> GetAll() =>
            await appDbContext.Towns.ToListAsync();

        public async Task<Town> GetById(int id) => await appDbContext.Towns.FindAsync(id);

        public async Task<GeneralResponse> Insert(Town item)
        {
            if (!await Checkname(item.Name!)) return new GeneralResponse(false, "Town is already added");
            appDbContext.Towns.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Town item)
        {
            var town = await appDbContext.Towns.FindAsync(item.Id);
            if (town is null) return NotFound();

            town.Name = item.Name;
            await Commit();
            return Success();
        }

        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.Towns.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, deaprtment not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
