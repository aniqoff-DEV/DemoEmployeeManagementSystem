using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : GenericController<Branch>
    {
        private readonly IGenericRepository<Branch> genericRepository;

        public BranchController(IGenericRepository<Branch> genericRepository) : base(genericRepository)
        {
            this.genericRepository = genericRepository;
        }
    }
}
