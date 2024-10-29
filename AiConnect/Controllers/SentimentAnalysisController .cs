using AiConnect.Services;
using Microsoft.AspNetCore.Mvc;

namespace AiConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentAnalysisController : ControllerBase
    {
        private readonly SentimentAnalysisService _sentimentAnalysisService;

        public SentimentAnalysisController()
        {
            _sentimentAnalysisService = new SentimentAnalysisService();
        }

        [HttpPost]
        public IActionResult AnalyzeSentiment([FromBody] string text)
        {
            var prediction = _sentimentAnalysisService.Predict(text);
            return Ok(prediction);
        }
    }
}
