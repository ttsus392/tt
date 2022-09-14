using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Linq_Joins2.Models;

namespace Linq_Joins2.Controllers
{
   

    public class ReportDtls
    {
        public string ReportId { get; set; }
        public string UserId { get; set; }
        public string Feedback { get; set; }
        public bool? IsLiked { get; set; }
        public bool? IsFav { get; set; }

        public int? Rating { get; set; }

        public override bool Equals(object obj)
        {
            return ((ReportDtls)obj).ReportId == ReportId && ((ReportDtls)obj).UserId == UserId;
        }
        public override int GetHashCode()
        {
            return ReportId.GetHashCode()+UserId.GetHashCode();
        }

    }
   

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

       

      

        [HttpGet]
        public IActionResult Get()
        {
            linq_practiceContext _context=new linq_practiceContext();
            var res = from rf in _context.ReportFeedbacks
                      join rlf in _context.ReportLikeFavs
                      on rf.ReportId equals rlf.ReportId
                      into finalRes
                      from fr in finalRes.DefaultIfEmpty()
                      select new ReportDtls
                      {
                          ReportId=rf.ReportId,
                          UserId=rf.UserId,
                          Feedback=rf.Feedback,
                          IsLiked=(fr==null)?false:fr.Isliked,
                          IsFav= (fr == null) ? false : fr.IsFav,
                      };



            var res2 = from rf in _context.ReportLikeFavs
                       join rlf in _context.ReportFeedbacks
                      on rf.ReportId equals rlf.ReportId
                      into finalRes
                      from fr in finalRes.DefaultIfEmpty()
                      select new ReportDtls
                      {
                          ReportId = rf.ReportId,
                          UserId = rf.UserId,
                          Feedback =(fr==null)?"": fr.Feedback,
                          IsLiked = rf.Isliked,
                          IsFav = rf.IsFav
                      };

            var res3 = res.Union(res2);

            var res4 = from rf in res3
                       join rlf in _context.ReportRates
                      on rf.ReportId equals rlf.ReportId
                      into finalRes
                       from fr in finalRes.DefaultIfEmpty()
                       select new ReportDtls
                       {
                           ReportId = rf.ReportId,
                           UserId = rf.UserId,
                           Feedback = rf.Feedback,
                           IsLiked = rf.IsLiked,
                           IsFav = rf.IsFav,
                           Rating=(fr==null)?0:fr.Rating
                       };

            var res5 = from rf in _context.ReportRates 
                       join rlf in res3
                      on rf.ReportId equals rlf.ReportId
                      into finalRes
                       from fr in finalRes.DefaultIfEmpty()
                       select new ReportDtls
                       {
                           ReportId = rf.ReportId,
                           UserId = rf.UserId,
                           Feedback = (fr==null)?"":fr.Feedback,
                           IsLiked = (fr == null) ?false : (fr.IsLiked==null)?false:fr.IsLiked,
                           IsFav = (fr == null) ?false : fr.IsFav,
                           Rating = rf.Rating
                       };
            var fin = res4.AsEnumerable().Union(res5).Distinct();
           
            return Ok(fin);
        }
    }
}
