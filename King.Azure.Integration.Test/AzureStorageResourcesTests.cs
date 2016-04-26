namespace King.Azure.Integration.Test.Data
{
    using King.Azure.Data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AzureStorageResourcesTests
    {
        private readonly string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public async Task TableNames()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new TableStorage(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var tables = await resources.TableNames();

            Assert.True(tables.Contains(name));
        }

        [Fact]
        public async Task Tables()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new TableStorage(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var tables = await resources.Tables();

            var exists = (from t in tables
                          where t.Name == name
                          select true).FirstOrDefault();

            Assert.True(exists);
        }

        [Fact]
        public async Task QueueNames()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new StorageQueue(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var queues = await resources.QueueNames();

            Assert.True(queues.Contains(name));
        }

        [Fact]
        public async Task Queues()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new StorageQueue(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var queues = await resources.Queues();

            var exists = (from q in queues
                          where q.Name == name
                          select true).FirstOrDefault();

            Assert.True(exists);

            await storage.Delete();
        }

        [Fact]
        public async Task ContainerNames()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new Container(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var containers = await resources.ContainerNames();

            Assert.True(containers.Contains(name));
        }

        [Fact]
        public async Task Containers()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new Container(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            var resources = new AzureStorageResources(ConnectionString);
            var containers = await resources.Containers();

            var exists = (from c in containers
                          where c.Name == name
                          select true).FirstOrDefault();

            Assert.True(exists);
        }
    }
}