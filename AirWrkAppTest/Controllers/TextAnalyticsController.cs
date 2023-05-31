using AirWrkAppTest.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirWrkAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalyticsController : ControllerBase
    {
        // POST api/<TextAnalyticsController>
        [HttpPost("Analysis")]
        public ActionResult Analysis([FromBody] string value)
        {           
            if (!string.IsNullOrEmpty(value))
            {
                value = value.ToLower();
                Analysis analysis = new Analysis();
                var item = value.Replace(" ", "").Replace(".","").Replace("?","");
                analysis.charCount = item.Length;
                analysis.wordCount = value.Count(c => c == ' ') + 1;
                analysis.sentenceCount = value.Count(c => c == '.'|| c == '?');

                BindFequentWord(value, analysis);


                return Ok(analysis);
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPost("similarities")]
        public ActionResult similarities([FromBody] SimilaritiesInput input)
        {
            if (!string.IsNullOrEmpty(input.text1) && !string.IsNullOrEmpty(input.text2))
            {
                decimal avg = 0;
                var value1 = input.text1.ToLower();
                var value2 = input.text2.ToLower();
                List<string> words1 = ((value1.Replace(".", "")).Replace("?", "")).Split(' ').ToList();
                List<string> words2 = ((value2.Replace(".", "")).Replace("?", "")).Split(' ').ToList();
                decimal firstValMatchCount = 0;
                decimal secoundValMatchCount = 0;
                foreach (string word in words1)
                {
                    if(value2.Contains(word))
                    {
                        firstValMatchCount++;
                    }
                }
                foreach (string word in words2)
                {
                    if (value1.Contains(word))
                    {
                        secoundValMatchCount++;
                    }
                }
                var fAvg = (firstValMatchCount / Convert.ToDecimal(words1.Count)) * 100;
                var sAvg = (secoundValMatchCount/ Convert.ToDecimal(words2.Count)) * 100;
                avg = (fAvg + sAvg) / 2;

                return Ok(avg);
            }
            else
            {
                return BadRequest();
            }
        }

        private void BindFequentWord(string value, Analysis analysis)
        {
            List<string> words = ((value.Replace(".","")).Replace("?","")).Split(' ').ToList();
            string longestword = string.Empty;
            string maxWord = string.Empty;
            int maxCount = 0;
            foreach(var item in words)
            {
                if(item.Length > longestword.Length)
                {
                    longestword = item;
                }
                if (string.IsNullOrEmpty(maxWord))
                {
                    maxWord = item;
                    maxCount = words.Where(c => c == item).Count();
                }
                else
                {
                    var count = words.Where(c => c == item).Count();
                    if(count > maxCount)
                    {
                        maxCount = count;
                        maxWord = item;
                    }
                }


            }
            analysis.mostFrequentWord = new Mostfrequentword() { frequency = maxCount, word = maxWord };
            analysis.longestWord = new Longestword() { word = longestword, length = longestword .Length};

        }
    }
}
