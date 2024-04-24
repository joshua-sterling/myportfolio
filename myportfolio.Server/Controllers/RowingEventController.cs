using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myportfolio.Server.Controllers.ViewModels;
using myportfolio.Server.DataAccess;
using myportfolio.Server.Models;

namespace myportfolio.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RowingEventController : ControllerBase
    {
        private readonly RowingEventRepository _rowingEventRepository;
        private readonly ILogger<RowingEventController> _logger;
        private readonly IMapper _mapper;
        public RowingEventController(RowingEventRepository rowingEventRepository, 
            ILogger<RowingEventController> logger,
            IMapper mapper)
        {
            _rowingEventRepository = rowingEventRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRowingEventsAsync([FromQuery] TableOptions tableOptions)
        {
            try
            {                
                var rowingEvents = await _rowingEventRepository.GetRowingEventsAsync(tableOptions);                
                var total = await _rowingEventRepository.GetRowingEventsCountAsync();

                return Ok(new { data = rowingEvents, total });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting rowing events");
                return StatusCode(500, "There was an error processing this request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRowingEventAsync(RowingEventViewModel rowingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newRowingEvent = new RowingEvent();
                _mapper.Map(rowingEvent, newRowingEvent);
                return Ok(await _rowingEventRepository.AddRowingEventAsync(newRowingEvent));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding new rowing event: {rowingEvent}");
                return StatusCode(500, "There was an error processing this request");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditRowingEventAsync(int id, [FromBody] RowingEventViewModel rowingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingRecord = await _rowingEventRepository.GetRowingEventAsync(id);
                if (existingRecord == null) return NotFound();

                _mapper.Map(rowingEvent, existingRecord);
                await _rowingEventRepository.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a rowing event");
                return StatusCode(500, "There was an error processing this request");
            }           
           
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<ChartData>>> GetRowingEventSummaryAsync()
        {
            var query =  _rowingEventRepository.GetRowingEvents();
            var summary = await query.GroupBy(x => x.Distance).Select(y => new ChartData
            {
                Name = y.Key.ToString()+"m", //adding m to indicate meters
                Value = y.Count()
            }).ToListAsync();
            return Ok(summary);
        }
    }
}
