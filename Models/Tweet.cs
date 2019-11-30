using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TweetsAPI.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("TweetId")]
        public long TweetId { get; set; }

        [BsonElement("User")]
        public string User { get; set; }

        [BsonElement("FollowersCount")]
        public string FollowersCount { get; set; }

        [BsonElement("Hashtag")]
        public string Hashtag { get; set; }

        [BsonElement("CreationDate")]
        public string CreationDate { get; set; }

        [BsonElement("Language")]
        public string Language { get; set; }
    }
}
