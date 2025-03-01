using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbacksController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> GetFeedbacks()
        {
            return _feedbackService.GetAllFeedbacks();
        }

        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedback(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return feedback;
        }

        [HttpPost]
        public ActionResult<Feedback> PostFeedback(Feedback feedback)
        {
            _feedbackService.AddFeedback(feedback);
            return CreatedAtAction(nameof(GetFeedback), new { id = feedback.FeedbackId }, feedback);
        }

        [HttpPut("{id}")]
        public IActionResult PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }
            _feedbackService.UpdateFeedback(feedback);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            _feedbackService.DeleteFeedback(id);
            return NoContent();
        }
    }
}