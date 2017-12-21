﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AdoNetCore.AseClient.Tests.Integration
{
    [TestFixture]
    public class AseDataReaderTests
    {
        private const string AllTypesQuery =
@"SELECT 
    CAST(123 AS INT) AS [INT],
    CAST(NULL AS INT) AS [NULL_INT],
    CAST(123 AS SMALLINT) AS [SMALLINT],
    CAST(NULL AS SMALLINT) AS [NULL_SMALLINT],
    CAST(123 AS BIGINT) AS [BIGINT],
    CAST(NULL AS BIGINT) AS [NULL_BIGINT],
    CAST(123 AS TINYINT) AS [TINYINT],
    CAST(NULL AS TINYINT) AS [NULL_TINYINT],

    CAST(123 AS UNSIGNED INT) AS [UNSIGNED_INT],
    CAST(NULL AS UNSIGNED INT) AS [NULL_UNSIGNED_INT],
    CAST(123 AS UNSIGNED SMALLINT) AS [UNSIGNED_SMALLINT],
    CAST(NULL AS UNSIGNED SMALLINT) AS [NULL_UNSIGNED_SMALLINT],
    CAST(123 AS UNSIGNED BIGINT) AS [UNSIGNED_BIGINT],
    CAST(NULL AS UNSIGNED BIGINT) AS [NULL_UNSIGNED_BIGINT],
    CAST(123 AS UNSIGNED TINYINT) AS [UNSIGNED_TINYINT],
    CAST(NULL AS UNSIGNED TINYINT) AS [NULL_UNSIGNED_TINYINT],

    CAST(123.45 AS REAL) AS [REAL],
    CAST(NULL AS REAL) AS [NULL_REAL],
    CAST(123.45 AS DOUBLE PRECISION) AS [DOUBLE_PRECISION],
    CAST(NULL AS DOUBLE PRECISION) AS [NULL_DOUBLE_PRECISION],
    CAST(123.45 AS NUMERIC(18,6)) AS [NUMERIC],
    CAST(NULL AS NUMERIC(18,6)) AS [NULL_NUMERIC],

    CAST(123.4567 AS MONEY) AS [MONEY],
    CAST(NULL AS MONEY) AS [NULL_MONEY],
    CAST(123.4567 AS SMALLMONEY) AS [SMALLMONEY],
    CAST(NULL AS SMALLMONEY) AS [NULL_SMALLMONEY],

    CAST(1 AS BIT) AS [BIT],
    CAST(0Xc9f317f51cdb45ba903e82bb4bfed8ef AS BINARY(16)) AS [BINARY],
    CAST(NULL AS BINARY(16)) AS [NULL_BINARY],
    CAST(0Xc9f317f51cdb45ba903e82bb4bfed8ef AS VARBINARY(16)) AS [VARBINARY],
    CAST(NULL AS VARBINARY(16)) AS [NULL_VARBINARY],
    CAST(0Xc9f317f51cdb45ba903e82bb4bfed8ef AS IMAGE) AS [IMAGE],
    CAST(NULL AS IMAGE) AS [NULL_IMAGE],

    CAST('Hello world' AS VARCHAR) AS [VARCHAR],
    CAST(NULL AS VARCHAR) AS [NULL_VARCHAR],
    CAST('Hello world' AS CHAR) AS [CHAR],
    CAST(NULL AS CHAR) AS [NULL_CHAR],
    CAST('Hello world' AS UNIVARCHAR) AS [UNIVARCHAR],
    CAST(NULL AS UNIVARCHAR) AS [NULL_UNIVARCHAR],
    CAST('Hello world' AS UNICHAR) AS [UNICHAR],
    CAST(NULL AS UNICHAR) AS [NULL_UNICHAR],
    CAST('Hello world' AS TEXT) AS [TEXT],
    CAST(NULL AS TEXT) AS [NULL_TEXT],
    CAST('Hello world' AS UNITEXT) AS [UNITEXT],
    CAST(NULL AS UNITEXT) AS [NULL_UNITEXT],

    --CAST('Apr 15 1987 10:23:00.000000PM' AS BIGDATETIME) AS [BIGDATETIME],
    --CAST(NULL AS BIGDATETIME) AS [NULL_BIGDATETIME],
    CAST('Apr 15 1987 10:23:00.000PM' AS DATETIME) AS [DATETIME],
    CAST(NULL AS DATETIME) AS [NULL_DATETIME],
    CAST('Apr 15 1987 10:23:00PM' AS SMALLDATETIME) AS [SMALLDATETIME],
    CAST(NULL AS SMALLDATETIME) AS [NULL_SMALLDATETIME],
    CAST('Apr 15 1987' AS DATE) AS [DATE],
    CAST(NULL AS DATE) AS [NULL_DATE],

    --CAST('11:59:59.999999 PM' AS BIGTIME) AS [BIGTIME],
    --CAST(NULL AS BIGTIME) AS [NULL_BIGTIME],
    CAST('23:59:59:997' AS TIME) AS [TIME],
    CAST(NULL AS TIME) AS [NULL_TIME]
";

        private readonly Dictionary<string, string> _connectionStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ConnectionStrings.json"));

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt32_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetInt32(ordinal), 123);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt32_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetInt32(ordinal));
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt16_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetInt16(ordinal), 123);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt16_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetInt16(ordinal));
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt64_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetInt64(ordinal), 123);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetInt64_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetInt64(ordinal));
        }


        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt32_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetUInt32(ordinal), 123u);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt32_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetUInt32(ordinal));
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt16_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetUInt16(ordinal), 123u);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt16_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetUInt16(ordinal));
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt64_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetUInt64(ordinal), 123u);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetUInt64_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetUInt64(ordinal));
        }

        [TestCase("BINARY")]
        [TestCase("VARBINARY")]
        [TestCase("IMAGE")]
        public void GetBytes_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetBytes(ordinal, 0, new byte[16], 0, 16), 16);
        }

        [TestCase("BINARY")]
        [TestCase("VARBINARY")]
        [TestCase("IMAGE")]
        public void GetBytes_WithNullValue_ReturnsNull(string aseType)
        {
            using (var connection = new AseConnection(_connectionStrings["default"]))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = AllTypesQuery;

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        var targetFieldOrdinal = reader.GetOrdinal($"NULL_{aseType}");

                        Assert.IsTrue(reader.Read());
                        Assert.AreEqual(0, reader.GetBytes(targetFieldOrdinal, 0, new byte[16], 0, 16));
                    }
                }
            }
        }

        [TestCase("BINARY")]
        public void GetGuid_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType,
                (reader, ordinal) => reader.GetGuid(ordinal),
                new Guid(new byte[] {0xc9, 0xf3, 0x17, 0xf5, 0x1c, 0xdb, 0x45, 0xba, 0x90, 0x3e, 0x82, 0xbb, 0x4b, 0xfe, 0xd8, 0xef}));
        }

        [TestCase("BINARY")]
        public void GetGuid_WithNullValue_ReturnsNull(string aseType)
        {
            GetHelper_WithNullValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetGuid(ordinal), Guid.Empty);
        }

        [TestCase("VARCHAR", "Hello world")]
        [TestCase("CHAR", "Hello world")]
        [TestCase("UNIVARCHAR", "Hello world")]
        [TestCase("UNICHAR", "Hello world")]
        [TestCase("TEXT", "Hello world")]
        [TestCase("UNITEXT", "Hello world")]
        public void GetString_WithValue_CastSuccessfully(string aseType, string expectedValue)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetString(ordinal), expectedValue);
        }

        [TestCase("VARCHAR")]
        [TestCase("CHAR")]
        [TestCase("UNIVARCHAR")]
        [TestCase("UNICHAR")]
        [TestCase("TEXT")]
        [TestCase("UNITEXT")]
        public void GetString_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetString(ordinal));
        }



        [TestCase("DATE", "Apr 15 1987")]
        [TestCase("DATETIME", "Apr 15 1987 10:23:00.000PM")]
        [TestCase("SMALLDATETIME", "Apr 15 1987 10:23:00PM")]
        [TestCase("BIGDATETIME", "Apr 15 1987 10:23:00.000000PM", Ignore = "true", IgnoreReason = "BIGDATETIME is not supported yet")]
        public void GetDateTime_WithValue_CastSuccessfully(string aseType, string expectedDateTime)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetDateTime(ordinal), DateTime.Parse(expectedDateTime));
        }

        [TestCase("DATE")]
        [TestCase("DATETIME")]
        [TestCase("SMALLDATETIME")]
        [TestCase("BIGDATETIME", Ignore = "true", IgnoreReason = "BIGDATETIME is not supported yet")]
        public void GetDateTime_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetDateTime(ordinal));
        }
        [TestCase("TIME", "23:59:59.997")]
        [TestCase("BIGTIME", "11:59:59.999999 PM", Ignore = "true", IgnoreReason = "BIGTIME is not supported yet")]
        public void GetTimeSpan_WithValue_CastSuccessfully(string aseType, string expectedTimeSpan)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetTimeSpan(ordinal), TimeSpan.Parse(expectedTimeSpan));
        }

        [TestCase("TIME")]
        [TestCase("BIGTIME", Ignore = "true", IgnoreReason = "BIGTIME is not supported yet")]
        public void GetTimeSpan_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetTimeSpan(ordinal));
        }

        [TestCase("BIT")]
        public void GetBoolean_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetBoolean(ordinal), true);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetByte_WithValue_CastSuccessfully(string aseType)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetByte(ordinal), 123);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetByte_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetByte(ordinal));
        }

        [TestCase("INT", 123)]
        [TestCase("BIGINT", 123)]
        [TestCase("SMALLINT", 123)]
        [TestCase("TINYINT", 123)]
        [TestCase("UNSIGNED_INT", 123)]
        [TestCase("UNSIGNED_BIGINT", 123)]
        [TestCase("UNSIGNED_SMALLINT", 123)]
        [TestCase("UNSIGNED_TINYINT", 123)]
        [TestCase("REAL", 123.45f)]
        [TestCase("DOUBLE_PRECISION", 123.45f)]
        [TestCase("NUMERIC", 123.45f)]
        [TestCase("MONEY", 123.4567f)]
        [TestCase("SMALLMONEY", 123.4567f)]
        public void GetFloat_WithValue_CastSuccessfully(string aseType, float expectedValue)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetFloat(ordinal), expectedValue);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetFloat_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetFloat(ordinal));
        }

        [TestCase("INT", 123d)]
        [TestCase("BIGINT", 123d)]
        [TestCase("TINYINT", 123d)]
        [TestCase("SMALLINT", 123d)]
        [TestCase("UNSIGNED_INT", 123d)]
        [TestCase("UNSIGNED_BIGINT", 123d)]
        [TestCase("UNSIGNED_SMALLINT", 123d)]
        [TestCase("UNSIGNED_TINYINT", 123d)]
        [TestCase("REAL", 123.45d)]
        [TestCase("DOUBLE_PRECISION", 123.45d)]
        [TestCase("NUMERIC", 123.45d)]
        [TestCase("MONEY", 123.4567d)]
        [TestCase("SMALLMONEY", 123.4567d)]
        public void GetDouble_WithValue_CastSuccessfully(string aseType, double expectedValue)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetDouble(ordinal), expectedValue);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetDouble_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetDouble(ordinal));
        }

        [TestCase("INT", 123)]
        [TestCase("BIGINT", 123)]
        [TestCase("TINYINT", 123)]
        [TestCase("SMALLINT", 123)]
        [TestCase("UNSIGNED_INT", 123)]
        [TestCase("UNSIGNED_BIGINT", 123)]
        [TestCase("UNSIGNED_SMALLINT", 123)]
        [TestCase("UNSIGNED_TINYINT", 123)]
        [TestCase("REAL", 123.45d)]
        [TestCase("DOUBLE_PRECISION", 123.45d)]
        [TestCase("NUMERIC", 123.45d)]
        [TestCase("MONEY", 123.4567d)]
        [TestCase("SMALLMONEY", 123.4567d)]
        public void GetDecimal_WithValue_CastSuccessfully(string aseType, decimal expectedValue)
        {
            GetHelper_WithValue_TCastSuccessfully(aseType, (reader, ordinal) => reader.GetDecimal(ordinal), expectedValue);
        }

        [TestCase("INT")]
        [TestCase("BIGINT")]
        [TestCase("SMALLINT")]
        [TestCase("TINYINT")]
        [TestCase("UNSIGNED_INT")]
        [TestCase("UNSIGNED_BIGINT")]
        [TestCase("UNSIGNED_SMALLINT")]
        [TestCase("UNSIGNED_TINYINT")]
        [TestCase("REAL")]
        [TestCase("DOUBLE_PRECISION")]
        [TestCase("NUMERIC")]
        [TestCase("MONEY")]
        [TestCase("SMALLMONEY")]
        public void GetDecimal_WithNullValue_ThrowsInvalidCastException(string aseType)
        {
            GetHelper_WithNullValue_ThrowsInvalidCastException(aseType, (reader, ordinal) => reader.GetDecimal(ordinal));
        }

        private void GetHelper_WithValue_TCastSuccessfully<T>(string columnName, Func<AseDataReader, int, T> testMethod, T expectedValue)
        {
            using (var connection = new AseConnection(_connectionStrings["default"]))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = AllTypesQuery;

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        var targetFieldOrdinal = reader.GetOrdinal(columnName);

                        Assert.IsTrue(reader.Read());

                        T value = testMethod(reader, targetFieldOrdinal);

                        if (expectedValue is float || expectedValue is double)
                        {
                            Assert.That(expectedValue, Is.EqualTo(value).Within(0.1));
                        }
                        else if (expectedValue is string)
                        {
                            Assert.AreEqual(expectedValue, (value as string)?.Trim());
                        }
                        else
                        {
                            Assert.AreEqual(expectedValue, value);
                        }

                        Assert.IsFalse(reader.Read());
                        Assert.IsFalse(reader.NextResult());
                    }
                }
            }
        }

        private void GetHelper_WithNullValue_ThrowsInvalidCastException<T>(string columnName, Func<AseDataReader, int, T> testMethod)
        {
            using (var connection = new AseConnection(_connectionStrings["default"]))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = AllTypesQuery;

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        var targetFieldOrdinal = reader.GetOrdinal($"NULL_{columnName}");

                        Assert.IsTrue(reader.Read());

                        Assert.Throws<InvalidCastException>(() => testMethod(reader, targetFieldOrdinal));
                    }
                }
            }
        }

        private void GetHelper_WithNullValue_TCastSuccessfully<T>(string columnName, Func<AseDataReader, int, T> testMethod, T expectedValue)
        {
            GetHelper_WithValue_TCastSuccessfully($"NULL_{columnName}", testMethod, expectedValue);
        }
    }
}
