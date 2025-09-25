using AutoMapper;
using ct.backend.Common.Pagination;
using ct.backend.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ct.backend.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController<TId, TCreateRequest, TUpdateRequest, TQueryRequest, TDto> : ControllerBase
    where TId : notnull
    where TCreateRequest : AbstractRequest
    where TUpdateRequest : AbstractRequest
    where TQueryRequest : AbstractRequest
    {
        /// <summary>
        /// POST /
        /// </summary>
        [HttpPost]
        public abstract Task<ActionResult<AbstractResponse<TDto>>> Create([FromBody] TCreateRequest request, CancellationToken ct);


        /// <summary>
        /// PUT /{id}
        /// </summary>
        [HttpPut("{id}")]
        public abstract Task<ActionResult<AbstractResponse<TDto>>> Update([FromRoute] TId id, [FromBody] TUpdateRequest request, CancellationToken ct);

        /// <summary>
        /// DELETE /{id}
        /// </summary>
        [HttpDelete("{id}")]
        public abstract Task<ActionResult<AbstractResponse<object?>>> Delete([FromRoute] TId id, CancellationToken ct);

        /// <summary>
        /// GET /{id}
        /// </summary>
        [HttpGet("{id}")]
        public abstract Task<ActionResult<AbstractResponse<TDto>>> GetById([FromRoute] TId id, CancellationToken ct);


        /// <summary>
        /// GET /all – lấy toàn bộ (không phân trang). Dùng cẩn trọng.
        /// </summary>
        [HttpGet("all")]
        public abstract Task<ActionResult<AbstractResponse<IEnumerable<TDto>>>> GetAll(CancellationToken ct);


        /// <summary>
        /// GET / – có phân trang/lọc/sắp xếp… qua TQueryRequest.
        /// </summary>
        [HttpGet]
        public abstract Task<ActionResult<AbstractResponse<PaginatedList<TDto>>>> GetPaged([FromQuery] TQueryRequest request, CancellationToken ct);
    }
}
