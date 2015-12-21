using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NPoco.Tests.Common
{
    public class RideSharkRealDBTest : BaseDBTest
    {

        [SetUp]
        public void SetUp()
        {

            IDbConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings["RSDBConnection"]);
            Database = new Database(dbConnection);
        }
    }
}
