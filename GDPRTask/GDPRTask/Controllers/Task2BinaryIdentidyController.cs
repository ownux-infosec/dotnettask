using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDPRTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Task2BinaryIdentidyController : ControllerBase
    {
        [HttpPost("analyze")]
        public IActionResult AnalyzeBinaryString([FromBody] string binaryString)
        {
            if (string.IsNullOrEmpty(binaryString))
            {
                return BadRequest("Input string cannot be null or empty.");
            }

            bool isGood = IsGoodBinaryString(binaryString);
            return Ok(new { IsGood = isGood });
        }

        private bool IsGoodBinaryString(string binaryString)
        {
            int count0 = 0, count1 = 0;

            foreach (char c in binaryString)
            {
                if (c == '0') count0++;
                else if (c == '1') count1++;

                // Check for conditions
                if (count0 > count1) return false; // More 0s than 1s at any point
            }

            // Check for equal number of 0s and 1s
            return count0 == count1;
        }
    }
}
