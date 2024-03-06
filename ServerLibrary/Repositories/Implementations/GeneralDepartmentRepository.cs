using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class GeneralDepartmentRepository : IGenericRepository<GeneralDepartment>
    {
        private readonly AppDbContext appDbContext;

        public GeneralDepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await appDbContext.GeneralDepartments.FindAsync(id);
            if (department is null) return NotFound();

            appDbContext.GeneralDepartments.Remove(department);
            await Commit();
            return Success();
        }

        public async Task<GeneralDepartment> GetById(int id) =>
            await appDbContext.GeneralDepartments.FindAsync(id);

        public async Task<List<GeneralDepartment>> GetAll() =>
            await appDbContext.GeneralDepartments.ToListAsync();

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            if (!await Checkname(item.Name)) return new GeneralResponse(false, "General Department is already added");
            appDbContext.GeneralDepartments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var department = await appDbContext.GeneralDepartments.FindAsync(item.Id);
            if (department is null) return NotFound();

            department.Name = item.Name;
            await Commit();
            return Success();
        }

        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, general department not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
