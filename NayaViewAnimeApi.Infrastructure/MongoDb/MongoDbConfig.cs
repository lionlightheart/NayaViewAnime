
namespace NayaViewAnimeApi.Infrastructure.MongoDb
{
    public class MongoDbConfig
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
