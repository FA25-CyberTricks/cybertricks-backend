using AutoMapper;
using AutoMapper.QueryableExtensions;
using ct.backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ct.backend.Features.Stores
{
    // /odata/Stores
    [Route("odata/Stores")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class StoresODataController : ODataController
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<StoresODataController> _logger;

        public StoresODataController(BookingDbContext db, IMapper mapper, ILogger<StoresODataController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        // GET /odata/Stores?$filter=contains(Name,'x')&$orderby=Name&$top=10&$skip=0&$count=true&$expand=Brand
        [EnableQuery(PageSize = 50, MaxExpansionDepth = 2)]
        public IQueryable<StoreDto> Get()
            => _db.Stores
                  .AsNoTracking()
                  .ProjectTo<StoreDto>(_mapper.ConfigurationProvider);

        // GET /odata/Stores(123)
        [EnableQuery]
        [HttpGet("({key})")]
        public async Task<IActionResult> Get([FromODataUri] int key, CancellationToken ct)
        {
            var q = _db.Stores
                       .AsNoTracking()
                       .Where(s => s.StoreId == key)
                       .ProjectTo<StoreDto>(_mapper.ConfigurationProvider);

            if (!await q.AnyAsync(ct)) return NotFound();
            return Ok(SingleResult.Create(q));
        }
    }
}
