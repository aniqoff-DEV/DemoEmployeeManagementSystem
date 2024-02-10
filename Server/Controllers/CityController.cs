using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : GenericController<City>
    {
        private readonly IGenericRepository<City> genericRepository;

        public CityController(IGenericRepository<City> genericRepository) : base(genericRepository)
        {
            this.genericRepository = genericRepository;
        }
    }
}
