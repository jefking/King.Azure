namespace King.Azure.Unit.Test.Data
{
    using King.Azure.Data;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class StorageQueueShardsTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public void Constructor()
        {
            var sqs = new StorageQueueShards("test", ConnectionString, 2);
            //Assert.AreEqual(2, sqs.Queues.Count());
        }

        [Fact]
        public void ConstructorConnectionNull()
        {
            //Assert.That(() => new StorageQueueShards("test", null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorNameNull()
        {
            //Assert.That(() => new StorageQueueShards(null, ConnectionString), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorQueuesNull()
        {
            //Assert.That(() => new StorageQueueShards(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void ConstructorQueuesEmpty()
        {
            //Assert.That(() => new StorageQueueShards(new IStorageQueue[0]), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorShardDefault()
        {
            var sqs = new StorageQueueShards("test", ConnectionString);
            //Assert.AreEqual(2, sqs.Queues.Count());
        }

        [Fact]
        public void IsIQueueShardSender()
        {
            //Assert.IsNotNull(new StorageQueueShards("test", ConnectionString) as IQueueShardSender<IStorageQueue>);
        }

        [Fact]
        public void IsIAzureStorage()
        {
            //Assert.IsNotNull(new StorageQueueShards("test", ConnectionString) as IAzureStorage);
        }

        [Fact]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var sqs = new StorageQueueShards(name, ConnectionString, 2);

            //Assert.AreEqual(name, sqs.Name);
        }

        [Fact]
        public void Queues()
        {
            var random = new Random();
            var i = (byte)random.Next(1, byte.MaxValue);
            var sqs = new StorageQueueShards("test", ConnectionString, i);

            //Assert.IsNotNull(sqs.Queues);
            //Assert.AreEqual(i, sqs.Queues.Count());
        }

        [Fact]
        public async Task CreateIfNotExists()
        {
            var random = new Random();
            var i = random.Next(1, byte.MaxValue);
            var qs = new List<IStorageQueue>();
            for (var j = 0; j < i; j++)
            {
                //var q = Substitute.For<IStorageQueue>();
                //q.CreateIfNotExists().Returns(Task.FromResult(true));
                //qs.Add(q);
            }
            var sqs = new StorageQueueShards(qs.ToArray());

            var success = await sqs.CreateIfNotExists();
            //Assert.IsTrue(success);

            foreach (var q in qs)
            {
                //await q.Received().CreateIfNotExists();
            }
        }

        [Fact]
        public async Task Delete()
        {
            var random = new Random();
            var i = random.Next(1, byte.MaxValue);
            var qs = new List<IStorageQueue>();
            for (var j = 0; j < i; j++)
            {
                //var q = Substitute.For<IStorageQueue>();
                //q.Delete().Returns(Task.FromResult(true));
                //qs.Add(q);
            }
            var sqs = new StorageQueueShards(qs.ToArray());

            await sqs.Delete();

            foreach (var q in qs)
            {
                //await q.Received().Delete();
            }
        }

        [Fact]
        public async Task Save()
        {
            var random = new Random();
            var i = (byte)random.Next(1, byte.MaxValue);
            var index = random.Next(0, i);

            var msg = new object();
            var qs = new List<IStorageQueue>();

            for (var j = 0; j < i; j++)
            {
                //var q = Substitute.For<IStorageQueue>();
                //q.Send(msg).Returns(Task.CompletedTask);
                //qs.Add(q);
            }

            var sqs = new StorageQueueShards(qs);

            await sqs.Save(msg, (byte)index);

            for (var j = 0; j < i; j++)
            {
                if (j == index)
                {
                    //await qs[j].Received().Send(msg);
                }
                else
                {
                    //await qs[j].DidNotReceive().Send(msg);
                }
            }
        }

        [Fact]
        public void Index()
        {
            var msg = new object();
            //var q = Substitute.For<IStorageQueue>();

            var qs = new List<IStorageQueue>();
            //qs.Add(q);
            //qs.Add(q);
            //qs.Add(q);

            var sqs = new StorageQueueShards(qs);

            var index = sqs.Index(0);

            //Assert.IsTrue(0 <= index && 3 > index);
        }
        
        //[Fact]
        //public void IndexBad([Values(0,255)] int val, [Values(0,0)] int expected)
        //{
        //    var msg = new object();
        //    var q = Substitute.For<IStorageQueue>();

        //    var qs = new List<IStorageQueue>();
        //    qs.Add(q);

        //    var sqs = new StorageQueueShards(qs);

        //    var index = sqs.Index((byte)val);

        //    Assert.AreEqual(expected, index);
        //}
    }
}