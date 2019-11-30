using System;
using System.Collections.Generic;
using TweetsAPI.Models;
using TweetsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace TweetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly TweetService _tweetService;
        private readonly List<Tweet> tweets;
        ILogger<TweetController> logger;

        public TweetController(TweetService tweetService, ILogger<TweetController> logger)
        {
            _tweetService = tweetService;
            tweets = _tweetService.Get();
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create(List<Tweet> tweets)
        {
            _tweetService.Create(tweets);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Tweet>> Get() =>
            _tweetService.Get();

        [HttpGet("MaxFollowers")]
        public ActionResult<string> GetMaxFollowers()
        {
            try
            {
                var top = tweets
                    .OrderByDescending(p => p.FollowersCount)
                    .Select(x => new { x.User, Followers = x.FollowersCount })
                    .Take(5);

                logger.LogInformation("5 users with most followers retrieved");
                return JsonConvert.SerializeObject(top);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return "";
            }
        }

        [HttpGet("TweetsGroup")]
        public ActionResult<string> GetTweetsGroup()
        {
            try
            {
                var groupedTweets = tweets
                    .GroupBy(x => x.CreationDate.Substring(11, 2))
                    .Select(x => new { Hora = x.Key, Count = x.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                logger.LogInformation("Tweets grouped by hours of the day");
                return JsonConvert.SerializeObject(groupedTweets);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return "";
            }
        }

        [HttpGet("TweetCountByHashtag")]
        public ActionResult<string> GetTweetCountByHashTag()
        {
            try
            {
                var groupedTweets = tweets
                    .GroupBy(x => new { x.Hashtag, x.Language })
                    .Select(x => new { x.Key.Hashtag, x.Key.Language, Count = x.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                return JsonConvert.SerializeObject(groupedTweets);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}