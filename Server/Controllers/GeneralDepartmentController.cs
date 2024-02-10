using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController : GenericController<GeneralDepartment>
    {
        private readonly IGenericRepository<GeneralDepartment> genericRepository;

        public GeneralDepartmentController(IGenericRepository<GeneralDepartment> genericRepository) : base(genericRepository) 
        {
            this.genericRepository = genericRepository;
        }
    }
}
