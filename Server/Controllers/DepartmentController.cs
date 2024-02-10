using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department>
    {
        private readonly IGenericRepository<Department> genericRepository;

        public DepartmentController(IGenericRepository<Department> genericRepository) : base(genericRepository)
        {
            this.genericRepository = genericRepository;
        }
    }
}
