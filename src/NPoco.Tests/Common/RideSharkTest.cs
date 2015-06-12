using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NPoco.DatabaseTypes;
using NUnit.Framework;

namespace NPoco.Tests.Common
{
    public class RideSharkTest : BaseDBTest  
    {
        public List<RideSharkTestData > InMemoryRideSharkTestDataset { get; set; }

        public const string NullStringDatum = "NullStringDatum";
        public const string NullBoolDatum = "NullBoolDatum";
        public const string StringIs123Datum = "StringIs123Datum";
        public const string TrueBoolDatum = "TrueBoolDatum";
        public const string FalseBoolDatum = "FalseBoolDatum";

        public RideSharkTest (): base()
        {
            InMemoryRideSharkTestDataset = new List<RideSharkTestData>();
        }

        [SetUp]
        public void SetUp()
        {
            var testDBType = Convert.ToInt32(ConfigurationManager.AppSettings["TestDBType"]);
            switch (testDBType)
            {
                case 1: // SQLite In-Memory
                    TestDatabase = new InMemoryDatabase();
                    Database = new Database(TestDatabase.Connection);
                    break;

                case 2: // SQL Local DB
                    TestDatabase = new SQLLocalDatabase();
                    Database = new Database(TestDatabase.Connection, new SqlServer2008DatabaseType(), IsolationLevel.ReadUncommitted); // Need read uncommitted for the transaction tests
                    break;

                case 3: // SQL Server
                case 4: // SQL CE
                case 5: // MySQL
                case 6: // Oracle
                case 7: // Postgres
                    Assert.Fail("Database platform not supported for unit testing");
                    return;

                case 8: // Firebird
                    TestDatabase = new FirebirdDatabase();
                    Database = new Database(TestDatabase.Connection, new FirebirdDatabaseType(), IsolationLevel.ReadUncommitted);
                    break;

                default:
                    Assert.Fail("Unknown database platform specified");
                    return;
            }

            // Insert test data
            InsertRideSharkTestData();       
        }

        [TearDown]
        public void CleanUp()
        {
            if (TestDatabase == null) return;

            TestDatabase.CleanupDataBase();
            TestDatabase.Dispose();
        }

        protected void InsertRideSharkTestData()
        {
            var id = 0;
            var nullStringTest = new RideSharkTestData
            {
                Id= id++,
                Description = NullStringDatum,
                TextData = null
            };
            Database.Insert(nullStringTest);
            InMemoryRideSharkTestDataset.Add(nullStringTest);

            var nullBoolDatum = new RideSharkTestData
            {
                Id = id++,
                Description = NullBoolDatum,
            };
            Database.Insert(nullBoolDatum);
            InMemoryRideSharkTestDataset.Add(nullBoolDatum);

            var stringIs123Datum = new RideSharkTestData
            {
                Id = id++,
                Description = StringIs123Datum ,
                TextData = "123"
            };
            Database.Insert(stringIs123Datum);
            InMemoryRideSharkTestDataset.Add(stringIs123Datum);

            var trueBoolDatum = new RideSharkTestData
            {
                Id = id++,
                Description = TrueBoolDatum ,
                BoolValue = true
            };
            Database.Insert(trueBoolDatum );
            InMemoryRideSharkTestDataset.Add(trueBoolDatum );

            var falseBoolDatum = new RideSharkTestData
            {
                Id = id++,
                Description = FalseBoolDatum ,
                BoolValue = false
            };
            Database.Insert(falseBoolDatum );
            InMemoryRideSharkTestDataset.Add(falseBoolDatum);


            var count = Database.ExecuteScalar<int>("SELECT COUNT(Id) FROM RideSharkTestData");
            Assert.AreEqual(InMemoryRideSharkTestDataset.Count, count,
                "Test RideShark Data not in sync db has " + count + " records, but the in memory copy has only " +
                InMemoryRideSharkTestDataset.Count + " records.");
            System.Diagnostics.Debug.WriteLine("Created " + count + " units of data for RideShark specific tests.");
        }


        public List<RideSharkTestData> FetchDescription(string desc)
        {
            return Database.Fetch<RideSharkTestData>("Description = @0", desc);
        }
    }
}
