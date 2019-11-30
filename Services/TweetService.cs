using System.Collections.Generic;
using System.Linq;
using TweetsAPI.Models;
using MongoDB.Driver;

namespace TweetsAPI.Services
{
    public class TweetService
    {
        private readonly IMongoCollection<Tweet> _tweets;

        private readonly string connectionString = "mongodb://127.0.0.1:27017";
        private readonly string databaseName = "TweetsDB";
        private readonly string tweetsCollectionName = "tweets";

        public TweetService()
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);

            _tweets = db.GetCollection<Tweet>(tweetsCollectionName);
        }

        public void Create(List<Tweet> tweets)
        {
            _tweets.InsertMany(tweets);
        }

        public List<Tweet> Get() =>
            _tweets.Find(tweet => true).ToList();
    }
}
