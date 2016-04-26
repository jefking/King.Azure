﻿namespace King.Service.Integration
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Xunit;
    public class QueueTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";
        private const string QueueName = "testing";

        //[SetUp]
        public void SetUp()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);
            storage.CreateIfNotExists().Wait();
        }

        //[TearDown]
        public void TearDown()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);
            storage.Delete().Wait();
        }

        [Fact]
        public async Task CreateIfNotExists()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new StorageQueue(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);
        }

        [Fact]
        public async Task ConstructorAccount()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var account = CloudStorageAccount.Parse(ConnectionString);
            var storage = new StorageQueue(name, account, TimeSpan.FromSeconds(34));
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);
        }

        [Fact]
        public async Task RoundTrip()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);

            //var msg = new CloudQueueMessage(Guid.NewGuid().ToByteArray());
            //await storage.Send(msg);
            //var returned = await storage.Get();

            //Assert.Equal(msg.AsBytes, returned.AsBytes);
        }

        [Fact]
        public async Task RoundTripMsgAsObj()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);

            //var msg = new CloudQueueMessage(Guid.NewGuid().ToByteArray());
            //await storage.Send((object)msg);
            //var returned = await storage.Get();

            //Assert.Equal(msg.AsBytes, returned.AsBytes);
        }
        
        [Fact]
        public async Task RoundTripObject()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);
            var expected = Guid.NewGuid();
            await storage.Send(expected);

            var returned = await storage.Get();

            var guid = JsonConvert.DeserializeObject<Guid>(returned.AsString);

            Assert.Equal(expected, guid);
        }
        
        [Fact]
        public async Task ApproixmateMessageCount()
        {
            var random = new Random();
            var count = random.Next(1, 1000);
            var storage = new StorageQueue(QueueName, ConnectionString);
            for (var i = 0; i < count; i++)
            {
                await storage.Send(Guid.NewGuid());
            }

            var result = await storage.ApproixmateMessageCount();
            Assert.Equal(count, result);
        }

        [Fact]
        public async Task ApproixmateMessageCountNone()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);
            var result = await storage.ApproixmateMessageCount();
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Delete()
        {
            var storage = new StorageQueue(QueueName, ConnectionString);

            //var msg = new CloudQueueMessage(Guid.NewGuid().ToByteArray());
            //await storage.Send(msg);
            var returned = await storage.Get();
            await storage.Delete(returned);
        }

        [Fact]
        public async Task RoundTripMany()
        {
            var random = new Random();
            var count = random.Next(1, 25);

            var storage = new StorageQueue(QueueName, ConnectionString);

            for (var i = 0; i < count; i++)
            {
                //var msg = new CloudQueueMessage(Guid.NewGuid().ToByteArray());
                //await storage.Send(msg);
            }

            var returned = await storage.GetMany(count);

            Assert.Equal(count, returned.Count());
        }

        [Fact]
        public async Task GetManyNegative()
        {
            var random = new Random();
            var count = random.Next(1, 25);

            var storage = new StorageQueue(QueueName, ConnectionString);

            for (var i = 0; i < count; i++)
            {
                //var msg = new CloudQueueMessage(Guid.NewGuid().ToByteArray());
                //await storage.Send(msg);
            }

            var returned = await storage.GetMany(-1);

            Assert.Equal(1, returned.Count());
        }
    }
}