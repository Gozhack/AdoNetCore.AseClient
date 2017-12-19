using System;
using System.IO;
using System.Text;
using AdoNetCore.AseClient.Enum;
using AdoNetCore.AseClient.Interface;
using AdoNetCore.AseClient.Internal;

namespace AdoNetCore.AseClient.Token
{
    /// <summary>
    /// Refer p. 205 TDS_DYNAMIC and p.213 TDS_DYNAMIC2
    /// </summary>
    internal class DynamicCommonToken : IToken
    {
        public TokenType Type { get; private set; }

        public DynamicOperationType OperationType { get; set; }
        public DynamicStatus Status { get; set; }
        public string Id { get; set; }
        public string Statement { get; set; }

        public DynamicCommonToken(TokenType type)
        {
            Type = type;
        }

        public void Write(Stream stream, Encoding enc)
        {
            Logger.Instance?.WriteLine($"-> {Type}: {OperationType}, {Status}, {Id}");
            var id = Encoding.ASCII.GetBytes(Id);
            var idLen = (byte)id.Length;
            var statement = Encoding.ASCII.GetBytes(Statement);
            var statementLen = Type == TokenType.TDS_DYNAMIC2
                ? statement.Length
                : (short) statement.Length;

            var totalLength = Type == TokenType.TDS_DYNAMIC2
                ? 7 + idLen + statementLen
                : (short) (5 + idLen + statementLen);
            
            stream.WriteByte((byte)Type);
            if (Type == TokenType.TDS_DYNAMIC2)
            {
                stream.WriteInt(totalLength);
            }
            else
            {
                stream.WriteShort((short) totalLength);
            }

            stream.WriteByte((byte)OperationType);
            stream.WriteByte((byte)Status);
            stream.WriteByte(idLen);
            stream.Write(id, 0, idLen);

            if (Type == TokenType.TDS_DYNAMIC2)
            {
                stream.WriteInt(statementLen);
            }
            else
            {
                stream.WriteShort((short)statementLen);
            }

            stream.Write(statement, 0, statementLen);
        }

        public void Read(Stream stream, Encoding enc, IFormatToken previousFormatToken)
        {
            var remainingLength = Type == TokenType.TDS_DYNAMIC2 ? stream.ReadInt() : stream.ReadShort();
            using (var ts = new ReadablePartialStream(stream, remainingLength))
            {
                OperationType = (DynamicOperationType) ts.ReadByte();
                Status = (DynamicStatus) ts.ReadByte();
                Id = stream.ReadByteLengthPrefixedString(Encoding.ASCII);
                if (Type == TokenType.TDS_DYNAMIC2)
                {
                    Statement = stream.ReadIntLengthPrefixedString(Encoding.ASCII);
                }
                else
                {
                    Statement = stream.ReadShortLengthPrefixedString(Encoding.ASCII);
                }
            }

            Logger.Instance?.WriteLine($"<- {Type}: {OperationType}, {Status}, {Id}");
        }
    }

    internal class DynamicToken : DynamicCommonToken
    {
        public DynamicToken() : base(TokenType.TDS_DYNAMIC)
        {
        }
    }

    internal class Dynamic2Token : DynamicCommonToken
    {
        public Dynamic2Token() : base(TokenType.TDS_DYNAMIC2)
        {
        }

        public static Dynamic2Token Create(Stream stream, Encoding enc, IFormatToken previousFormatToken)
        {
            var t = new Dynamic2Token();
            t.Read(stream, enc, previousFormatToken);
            return t;
        }
    }
}
