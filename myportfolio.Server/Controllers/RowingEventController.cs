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
        public IActionResult GetRowingEvents([FromQuery] TableOptions tableOptions)
        {
            try
            {                
                var rowingEvents = _rowingEventRepository.GetRowingEvents(tableOptions);                
                var total = _rowingEventRepository.GetRowingEventsCount();
                return Ok(new { data = rowingEvents, total });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting rowing events");
                return StatusCode(500, "There was an error processing this request");
            }
        }

        [HttpPost]
        public IActionResult AddRowingEvent(RowingEventViewModel rowingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newRowingEvent = new RowingEvent();
                _mapper.Map(rowingEvent, newRowingEvent);
                return Ok(_rowingEventRepository.AddRowingEvent(newRowingEvent));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new rowing event");
                return StatusCode(500, "There was an error processing this request");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult EditRowingEvent(int id, [FromBody] RowingEventViewModel rowingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingRecord = _rowingEventRepository.GetRowingEvent(id);
                if (existingRecord == null) return NotFound();

                _mapper.Map(rowingEvent, existingRecord);
                _rowingEventRepository.SaveChanges();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a rowing event");
                return StatusCode(500, "There was an error processing this request");
            }           
           
        }
    }
}
