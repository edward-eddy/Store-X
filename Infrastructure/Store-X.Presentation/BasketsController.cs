using Microsoft.AspNetCore.Mvc;
using Store_X.Services_Abstractions;
using Store_X.Shared.Dtos.Baskets;

namespace Store_X.Presentation
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class BasketsController(IServiceManager serviceManager) : ControllerBase
    public class BasketsController(IServiceManager serviceManager) : _ControllerParent
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
