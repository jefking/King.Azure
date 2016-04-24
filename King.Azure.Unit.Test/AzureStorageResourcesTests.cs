namespace King.Azure.Unit.Test.Data
{
    using King.Azure.Data;
    using NUnit.Framework;

    [TestFixture]
    public class AzureStorageResourcesTests
    {
        private readonly string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public void Constructor()
        {
            new AzureStorageResources(ConnectionString);
        }

        [Fact]
        public void IsIAzureStorageResources()
        {
            Assert.IsNotNull(new AzureStorageResources(ConnectionString) as IAzureStorageResources);
        }

        [Fact]
        public void IsAzureStorage()
        {
            Assert.IsNotNull(new AzureStorageResources(ConnectionString) as AzureStorage);
        }
    }
}