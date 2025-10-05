﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ct.backend.Common.Message;
using ct.backend.Common.Pagination;
using ct.backend.Common.Validate;
using ct.backend.Domain.Entities;
using ct.backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ct.backend.Features.Machines
{
    [Route("api/[controller]")]
    public class MachineController : AbstractController<int, CreateMachineRequest, UpdateMachineRequest, QueryMachineRequest, MachineDto>
    {
        private readonly BookingDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MachineController> _logger;

        public MachineController(BookingDbContext context, IMapper mapper, ILogger<MachineController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create a machine
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<MachineDto>>> Create([FromBody] CreateMachineRequest request, CancellationToken ct)
        {
            var response = new MachineResponse<MachineDto>();

            // 1. Validate request
            if (!ModelState.IsValid)
            {
                response.AddError(MessageCodes.E001);
                return BadRequest(response);
            }

            // 2. Check exist
            var exists = await _context.Machines.AnyAsync(b =>
                b.Code == request.Code, ct);

            if (exists)
            {
                response.AddError(MessageCodes.E007, "Code already exists", nameof(request.Code));
                return Conflict(response);
            }

            // 3. Map request -> entity
            var machine = _mapper.Map<Machine>(request);

            try
            {
                // 4. Save to DB
                await _context.Machines.AddAsync(machine, ct);
                var rows = await _context.SaveChangesAsync(ct);

                // 5. Map entity -> dto
                var dto = _mapper.Map<MachineDto>(machine);

                // 6. Wrap response
                response.Data = dto;
                response.Message = MessageCodes.E000;
                return Ok(response);
            }
            catch (DbUpdateException)
            {
                response.AddError(MessageCodes.E999, "Create machine failed");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Update a machine
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<MachineDto>>> Update([FromRoute] int id, [FromBody] UpdateMachineRequest request, CancellationToken ct)
        {
            var response = new MachineResponse<MachineDto>();

            // 1. Validate request
            if (!ModelState.IsValid)
            {
                response.AddError(MessageCodes.E001);
                return BadRequest(response);
            }

            // 2. Check exist
            var machine = await _context.Machines.FindAsync(id, ct);
            if (machine == null)
            {
                response.AddError(MessageCodes.E005, nameof(id));
                return NotFound(response);
            }

            // Check duplicate code if code is provided in the request
            if (request.Code != null)
            {
                var exists = await _context.Machines.AnyAsync(b =>
                    b.Code == request.Code &&
                    b.MachineId != id, ct);

                if (exists)
                {
                    response.AddError(MessageCodes.E007, nameof(request.Code)); // duplicate
                    return Conflict(response);
                }
            }

            // 3. Map request -> entity
            _mapper.Map(request, machine);
            machine.UpdatedAt = DateTime.UtcNow;

            try
            {
                // 4. Save to DB
                var rows = await _context.SaveChangesAsync(ct);

                // 5. Map entity -> dto
                var dto = _mapper.Map<MachineDto>(machine);

                // 6. Wrap response
                response.Data = dto;
                response.Message = MessageCodes.E000;
                return Ok(response);
            }
            catch (DbUpdateException)
            {
                response.AddError(MessageCodes.E999, "Update machine failed");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        ///  Delete a machine
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<object?>>> Delete([FromRoute] int id, CancellationToken ct)
        {
            var response = new MachineResponse<MachineDto>();

            var machine = await _context.Machines.FindAsync(id, ct);
            if (machine is null)
            {
                response.AddError(MessageCodes.E005, nameof(id));
                return NotFound(response);
            }

            _context.Machines.Remove(machine);
            var rows = await _context.SaveChangesAsync(ct);

            if (rows <= 0)
            {
                response.AddError(MessageCodes.E999, "Delete machine failed");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            response.Message = MessageCodes.E000;
            return Ok(response);
        }

        /// <summary>
        /// Get machine by id  
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<MachineDto>>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var response = new MachineResponse<MachineDto>();

            var machine = await _context.Machines.FindAsync(id, ct);
            if (machine is null)
            {
                response.AddError(MessageCodes.E005, nameof(id));
                return NotFound(response);
            }

            var dto = _mapper.Map<MachineDto>(machine);

            response.Data = dto;
            response.Message = MessageCodes.E000;
            return Ok(response);
        }

        /// <summary>
        /// Get all machines (no paging) - use with caution
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<IEnumerable<MachineDto>>>> GetAll(CancellationToken ct)
        {
            var response = new MachineResponse<IEnumerable<MachineDto>>();
            var machineDtos = await _context.Machines
                .AsNoTracking()
                .ProjectTo<MachineDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            response.Data = machineDtos;
            response.Message = MessageCodes.E000;
            return Ok(response);
        }


        /// <summary>
        /// Get machines with paging, filtering, sorting 
        /// </summary>
        public override async Task<ActionResult<AbstractResponse<PaginatedList<MachineDto>>>> GetPaged([FromQuery] QueryMachineRequest request, CancellationToken ct)
        {
            var response = new MachineResponse<PaginatedList<MachineDto>>();
            return Ok(response);
        }
    }
}

