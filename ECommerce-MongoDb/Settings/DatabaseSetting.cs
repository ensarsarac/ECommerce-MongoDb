namespace ECommerce_MongoDb.Settings
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string CustomerCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
