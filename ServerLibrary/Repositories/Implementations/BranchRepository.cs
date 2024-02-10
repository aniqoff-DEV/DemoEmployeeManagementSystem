using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepository : IGenericRepository<Branch>
    {
        private readonly AppDbContext appDbContext;

        public BranchRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await appDbContext.Branches.FindAsync(id);
            if (department is null) return NotFound();

            appDbContext.Branches.Remove(department);
            await Commit();
            return Success();
        }

        public async Task<Branch> GetById(int id) =>
            await appDbContext.Branches.FindAsync(id);

        public async Task<List<Branch>> GetAll() =>
            await appDbContext.Branches.ToListAsync();

        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await Checkname(item.Name!)) return new GeneralResponse(false, "Branch is already added");
            appDbContext.Branches.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var department = await appDbContext.Branches.FindAsync(item.Id);
            if (department is null) return NotFound();

            department.Name = item.Name;
            await Commit();
            return Success();
        }
        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.Branches.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
        private static GeneralResponse NotFound() => new(false, "Sorry, branch not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
