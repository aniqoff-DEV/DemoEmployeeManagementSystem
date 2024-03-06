using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : GenericController<Employee>
    {
        private readonly IGenericRepository<Employee> _genericRepository;

        public EmployeeController(IGenericRepository<Employee> genericRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
