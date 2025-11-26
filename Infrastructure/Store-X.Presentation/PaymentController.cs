using Microsoft.AspNetCore.Mvc;
using Store_X.Services_Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Presentation
{
    public class PaymentsController(IServiceManager _serviceManager) : _ControllerParent
    {

        // create paymentIntent
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }
    }
}