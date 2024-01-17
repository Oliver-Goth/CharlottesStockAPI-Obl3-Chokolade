using ChocolateLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CharlottesStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasterEggsController : ControllerBase
    {
        private readonly EasterEggsRepository _repository;

        public EasterEggsController()
        {
            _repository = new EasterEggsRepository();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            var eggs = _repository.Get();
            return Ok(eggs);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{productNo}")]
        public IActionResult GetByProductNo(int productNo)
        {
            try
            {
                var egg = _repository.GetByProductNo(productNo);
                return Ok(egg);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("lowstock/{stockLevel}")]
        public IActionResult GetLowStock(int stockLevel)
        {
            var lowStockEggs = _repository.GetLowStock(stockLevel);
            return Ok(lowStockEggs);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public IActionResult Update([FromBody] EasterEgg egg)
        {
            try
            {
                _repository.Update(egg);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
