﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AdoNetCore.AseClient.Enum;
using AdoNetCore.AseClient.Interface;
using AdoNetCore.AseClient.Token;

namespace AdoNetCore.AseClient.Internal
{
    internal class TokenParser : ITokenParser
    {
        public IToken[] Parse(Stream stream, Encoding enc)
        {
            return new List<IToken>(ParseInternal(stream, enc)).ToArray();
        }

        private IEnumerable<IToken> ParseInternal(Stream stream, Encoding enc)
        {
            IFormatToken previousFormatToken = null;
            while (stream.Position < stream.Length)
            {
                var tokenType = (TokenType)stream.ReadByte();

                if (Readers.ContainsKey(tokenType))
                {
                    var t = Readers[tokenType](stream, enc, previousFormatToken);

                    if (t is IFormatToken token)
                    {
                        Logger.Instance?.WriteLine($"**Set new format token**");
                        previousFormatToken = token;
                    }

                    yield return t;
                }
                else
                {
                    Logger.Instance?.WriteLine($"!!! Hit unknown token type {tokenType} !!!");
                    var t = new CatchAllToken(tokenType);
                    t.Read(stream, enc, previousFormatToken);
                    yield return t;
                }
            }
        }

        private static readonly Dictionary<TokenType, Func<Stream, Encoding, IFormatToken, IToken>> Readers = new Dictionary<TokenType, Func<Stream, Encoding, IFormatToken, IToken>>
        {
            {TokenType.TDS_ENVCHANGE, EnvironmentChangeToken.Create},
            {TokenType.TDS_EED, EedToken.Create },
            {TokenType.TDS_LOGINACK, LoginAckToken.Create },
            {TokenType.TDS_DONE, DoneToken.Create },
            {TokenType.TDS_CAPABILITY, CapabilityToken.Create },
            {TokenType.TDS_RETURNSTATUS, ReturnStatusToken.Create },
            {TokenType.TDS_DONEINPROC, DoneInProcToken.Create },
            {TokenType.TDS_DONEPROC, DoneProcToken.Create },
            {TokenType.TDS_ROWFMT2, RowFormat2Token.Create },
            {TokenType.TDS_CONTROL, ControlToken.Create },
            {TokenType.TDS_ROW, RowToken.Create },
            {TokenType.TDS_PARAMFMT, ParameterFormatToken.Create },
            {TokenType.TDS_PARAMFMT2, ParameterFormat2Token.Create },
            {TokenType.TDS_PARAMS, ParametersToken.Create },
            {TokenType.TDS_OPTIONCMD, OptionCommandToken.Create },
            {TokenType.TDS_DYNAMIC2, Dynamic2Token.Create }
        };
    }
}
