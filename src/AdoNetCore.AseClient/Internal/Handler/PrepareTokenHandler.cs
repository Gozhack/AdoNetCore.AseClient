using AdoNetCore.AseClient.Enum;
using AdoNetCore.AseClient.Interface;
using AdoNetCore.AseClient.Token;

namespace AdoNetCore.AseClient.Internal.Handler
{
    internal class PrepareTokenHandler : ITokenHandler
    {
        public bool CanHandle(TokenType type) => type == TokenType.TDS_DYNAMIC ||
                                                 type == TokenType.TDS_DYNAMIC2;

        private bool _acknowledgementReceived;

        public void Handle(IToken token)
        {
            switch (token)
            {
                case Dynamic2Token d2:
                    if (d2.OperationType == DynamicOperationType.TDS_DYN_ACK)
                    {
                        _acknowledgementReceived = true;
                    }
                    break;
                case DynamicToken d:
                    if (d.OperationType == DynamicOperationType.TDS_DYN_ACK)
                    {
                        _acknowledgementReceived = true;
                    }
                    break;
            }
        }

        public void AssertAcknowledgement()
        {
            if (!_acknowledgementReceived)
            {
                throw new AseException("Prepare() request was not acknowledged by server");
            }
        }
    }
}
