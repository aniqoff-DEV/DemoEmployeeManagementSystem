using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownController : GenericController<Town>
    {
        private readonly IGenericRepository<Town> genericRepository;

        public TownController(IGenericRepository<Town> genericRepository) : base(genericRepository)
        {
            this.genericRepository = genericRepository;
        }
    }
}
