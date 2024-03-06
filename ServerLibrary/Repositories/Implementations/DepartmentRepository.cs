using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var department = await appDbContext.Departments.FindAsync(id);
            if (department is null) return NotFound();

            appDbContext.Departments.Remove(department);
            await Commit();
            return Success();
        }

        public async Task<List<Department>> GetAll() => await appDbContext.Departments
            .AsNoTracking()
            .Include(gd => gd.GeneralDepartment)
            .ToListAsync();

        public async Task<Department> GetById(int id) => await appDbContext.Departments.FindAsync(id);

        public async Task<GeneralResponse> Insert(Department item)
        {
            if (!await Checkname(item.Name!)) return new GeneralResponse(false, "Department is already added");
            appDbContext.Departments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var department = await appDbContext.Departments.FindAsync(item.Id);
            if (department is null) return NotFound();

            department.Name = item.Name;
            department.GeneralDepartmentId = item.GeneralDepartmentId;
            await Commit();
            return Success();
        }

        private async Task<bool> Checkname(string name)
        {
            var item = await appDbContext.Departments.FirstOrDefaultAsync(x =>
                x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, department not found");
        private static GeneralResponse Success() => new(true, "Proccess completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
