using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MaximStartsev.FunctionalSql.Test
{
    [TestClass]
    public class FuConnectionTests
    {
        [TestMethod]
        public void NullConstructorArgumentError()
        {
            IDbConnection dbConnection = null;
            Assert.ThrowsException<ArgumentNullException>(() => FuConnection.Create(dbConnection));
        }
        [TestMethod]
        public void NotNullConstructorArgumentError()
        {
            var connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Encrypt = False; TrustServerCertificate = True";
            using (var connection = new SqlConnection(connectionString))
            {
                Assert.IsNotNull(FuConnection.Create(connection));
            }
        }
    }
}
