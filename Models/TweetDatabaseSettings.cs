using static TweetsAPI.Models.TweetDatabaseSettings;

namespace TweetsAPI.Models
{
    public class TweetDatabaseSettings : ITweetDatabaseSettings
    {
        public string TweetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public interface ITweetDatabaseSettings
        {
            string TweetsCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }
}
