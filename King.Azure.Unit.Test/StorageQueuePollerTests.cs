namespace King.Azure.Unit.Test.Data
{
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage.Queue;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class StorageQueuePollerTests
    {
        const string ConnectionString = "UseDevelopmentStorage=true";

        [Fact]
        public void Constructor()
        {
            new StorageQueuePoller<object>("queue", ConnectionString);
        }

        [Fact]
        public void ConstructorStorageQueueNull()
        {
            //Assert.That(() => new StorageQueuePoller<object>(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void IsIStorageQueuePoller()
        {
            //Assert.IsNotNull(new StorageQueuePoller<object>("queue", ConnectionString) as IStorageQueuePoller<object>);
        }

        [Fact]
        public void Queue()
        {
            //var queue = Substitute.For<IStorageQueue>();
            //var poller = new StorageQueuePoller<object>(queue);
            //var returned = poller.Queue;
            //Assert.Equal(queue, returned);
        }

        //[Fact]
        //public async Task Poll()
        //{
        //    var msg = new CloudQueueMessage("data");
        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.Get().Returns(Task.FromResult(msg));

        //    var poller = new StorageQueuePoller<object>(queue);
        //    var returned = await poller.Poll();

        //    Assert.IsNotNull(returned);

        //    await queue.Received().Get();
        //}

        //[Fact]
        //public async Task PollGetNull()
        //{
        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.Get().Returns(Task.FromResult<CloudQueueMessage>(null));

        //    var poller = new StorageQueuePoller<object>(queue);
        //    var returned = await poller.Poll();

        //    Assert.IsNull(returned);

        //    await queue.Received().Get();
        //}

        //[Fact]
        //public void PollGetThrows()
        //{
        //    var msg = new CloudQueueMessage("data");
        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.Get().ReturnsForAnyArgs<object>(x => { throw new ApplicationException(); });

        //    var poller = new StorageQueuePoller<object>(queue);

        //    Assert.That(() => poller.Poll(), Throws.TypeOf<ApplicationException>());
        //}

        //[Fact]
        //public async Task PollMany()
        //{
        //    var msg = new CloudQueueMessage("data");
        //    var msgs = new List<CloudQueueMessage>(3);
        //    msgs.Add(msg);
        //    msgs.Add(msg);
        //    msgs.Add(msg);

        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.GetMany(3).Returns(Task.FromResult<IEnumerable<CloudQueueMessage>>(msgs));

        //    var poller = new StorageQueuePoller<object>(queue);
        //    var returned = await poller.PollMany(3);

        //    Assert.IsNotNull(returned);
        //    Assert.AreEqual(3, returned.Count());

        //    await queue.Received().GetMany(3);
        //}

        //[Fact]
        //public async Task PollGetManyNull()
        //{
        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.GetMany(3).Returns(Task.FromResult<IEnumerable<CloudQueueMessage>>(null));

        //    var poller = new StorageQueuePoller<object>(queue);
        //    var returned = await poller.PollMany(3);

        //    Assert.IsNull(returned);

        //    await queue.Received().GetMany(3);
        //}

        //[Fact]
        //public void PollGetManyThrows()
        //{
        //    var msg = new CloudQueueMessage("data");
        //    var queue = Substitute.For<IStorageQueue>();
        //    queue.GetMany().ReturnsForAnyArgs<object>(x => { throw new ApplicationException(); });

        //    var poller = new StorageQueuePoller<object>(queue);

        //    Assert.That(() => poller.PollMany(), Throws.TypeOf<ApplicationException>());
        //}
    }
}