namespace King.Azure.Unit.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Xunit;

    public class TableStorageTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public void Constructor()
        {
            new TableStorage("TestTable", ConnectionString);
        }

        [Fact]
        public void IsITableStorage()
        {
            //Assert.IsNotNull(new TableStorage("TestTable", ConnectionString) as ITableStorage);
        }

        [Fact]
        public void PartitionKey()
        {
            Assert.Equal("PartitionKey", TableStorage.PartitionKey);
        }

        [Fact]
        public void RowKey()
        {
            Assert.Equal("RowKey", TableStorage.RowKey);
        }

        [Fact]
        public void Timestamp()
        {
            Assert.Equal("Timestamp", TableStorage.Timestamp);
        }

        [Fact]
        public void ETag()
        {
            Assert.Equal("ETag", TableStorage.ETag);
        }

        [Fact]
        public void ConstructorTableNull()
        {
            //Assert.That(() => new TableStorage(null, ConnectionString), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorAccountTableNull()
        {
            //Assert.That(() => new TableStorage(null, CloudStorageAccount.Parse(ConnectionString)), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorConnectionStringNull()
        {
            //Assert.That(() => new TableStorage("TestTable", (string)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);
            Assert.Equal(name, t.Name);
        }

        [Fact]
        public void Client()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);
            //Assert.IsNotNull(t.Client);
        }

        [Fact]
        public void Reference()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);
            //Assert.IsNotNull(t.Reference);
        }

        [Fact]
        public void InsertDictionaryNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.InsertOrReplace((IDictionary<string, object>)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void QueryFunctionNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Query<TableEntity>(null, 1000), Throws.TypeOf<ArgumentNullException>());
        }


        [Fact]
        public void QueryFunctionResultsNegative()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Query<TableEntity>(i => i.PartitionKey == "hi", -100), Throws.TypeOf<InvalidOperationException>());
        }

        [Fact]
        public void QueryTableQueryNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Query<TableEntity>(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void QueryDictionaryQueryNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Query((TableQuery)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void DeleteEntityNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Delete((ITableEntity)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void DeleteEntitiesNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            //Assert.That(() => t.Delete((IEnumerable<ITableEntity>)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void BatchOne()
        {
            var items = new List<ITableEntity>();
            items.Add(new TableEntity());

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);
            Assert.Equal(1, batches.Count());
            Assert.Equal(1, batches.First().Count());
        }

        [Fact]
        public void BatchNone()
        {
            var items = new List<ITableEntity>();

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);
            Assert.Equal(0, batches.Count());
        }

        [Fact]
        public void BatchThousandsDifferentPartitions()
        {
            var random = new Random();
            var count = random.Next(2001, 10000);
            var items = new List<ITableEntity>();

            for (var i = 0; i < count; i++)
            {
                items.Add(new TableEntity() { PartitionKey = Guid.NewGuid().ToString() });
            }

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);
            Assert.Equal(count, batches.Count());
        }

        [Fact]
        public void BatchThousands()
        {
            var random = new Random();
            var count = random.Next(2001, 10000);
            var partition = Guid.NewGuid().ToString();
            var items = new List<ITableEntity>();

            for (var i = 0; i < count; i++)
            {
                items.Add(new TableEntity() { PartitionKey = partition });
            }

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);
            Assert.Equal(Math.Ceiling(((double)count / TableStorage.MaimumxInsertBatch)), batches.Count());

            var resultCount = 0;
            foreach (var b in batches)
            {
                resultCount += b.Count();
            }

            Assert.Equal(count, resultCount);
        }

        [Fact]
        public void ChunkThousands()
        {
            var random = new Random();
            var count = random.Next(2001, 15000);
            var partition = Guid.NewGuid().ToString();
            var items = new List<ITableEntity>();

            for (var i = 0; i < count; i++)
            {
                items.Add(new TableEntity() { PartitionKey = partition });
            }

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Chunk<ITableEntity>(items);
            Assert.Equal(Math.Ceiling(((double)count / TableStorage.MaimumxInsertBatch)), batches.Count());

            var resultCount = 0;
            foreach (var b in batches)
            {
                resultCount += b.Count();
            }

            Assert.Equal(count, resultCount);
        }

        [Fact]
        public void ChunkOne()
        {
            var items = new List<ITableEntity>();
            items.Add(new TableEntity());

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Chunk<ITableEntity>(items);

            Assert.Equal(1, batches.Count());
            Assert.Equal(1, batches.First().Count());
        }

        [Fact]
        public void ChunkNone()
        {
            var items = new List<ITableEntity>();

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Chunk<ITableEntity>(items);

            Assert.Equal(0, batches.Count());
        }

        [Fact]
        public void BatchDictionaryOne()
        {
            var items = new List<IDictionary<string, object>>();
            var dic = new Dictionary<string, object>();
            dic.Add(TableStorage.PartitionKey, Guid.NewGuid().ToString());
            items.Add(dic);

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);

            Assert.Equal(1, batches.Count());
            Assert.Equal(1, batches.First().Count());
        }

        [Fact]
        public void BatchDictionaryNone()
        {
            var items = new List<IDictionary<string, object>>();

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);

            Assert.Equal(0, batches.Count());
        }

        [Fact]
        public void BatchDictionaryThousandsDifferentPartitions()
        {
            var random = new Random();
            var count = random.Next(2001, 10000);
            var items = new List<IDictionary<string, object>>();

            for (var i = 0; i < count; i++)
            {
                var dic = new Dictionary<string, object>();
                dic.Add(TableStorage.PartitionKey, Guid.NewGuid().ToString());
                items.Add(dic);
            }

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);

            Assert.Equal(count, batches.Count());
        }

        [Fact]
        public void BatchDictionaryThousands()
        {
            var random = new Random();
            var count = random.Next(2001, 10000);
            var partition = Guid.NewGuid().ToString();
            var items = new List<IDictionary<string, object>>();

            for (var i = 0; i < count; i++)
            {
                var dic = new Dictionary<string, object>();
                dic.Add(TableStorage.PartitionKey, partition);
                items.Add(dic);
            }

            var name = Guid.NewGuid().ToString();
            var t = new TableStorage(name, ConnectionString);

            var batches = t.Batch(items);
            Assert.Equal(Math.Ceiling(((double)count / TableStorage.MaimumxInsertBatch)), batches.Count());

            var resultCount = 0;
            foreach (var b in batches)
            {
                resultCount += b.Count();
            }

            Assert.Equal(count, resultCount);
        }
    }
}