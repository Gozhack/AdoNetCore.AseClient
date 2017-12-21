using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using AdoNetCore.AseClient.Internal;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AdoNetCore.AseClient.Tests.Integration
{
    [TestFixture]
    public class PreparedStatementTests
    {
        private readonly Dictionary<string, string> _connectionStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ConnectionStrings.json"));

        public PreparedStatementTests()
        {
            Logger.Enable();
        }

        [Test]
        public void PrepareSimpleTextCommand_Prepares()
        {
            using (var connection = new AseConnection(_connectionStrings["default"]))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = "select 1";

                command.Prepare();

                Assert.AreEqual(1, Convert.ToInt32(command.ExecuteScalar()));
                Assert.AreEqual(1, Convert.ToInt32(command.ExecuteScalar()));
                Assert.AreEqual(1, Convert.ToInt32(command.ExecuteScalar()));
                Assert.AreEqual(1, Convert.ToInt32(command.ExecuteScalar()));
                Assert.AreEqual(1, Convert.ToInt32(command.ExecuteScalar()));
            }
        }
    }
}
