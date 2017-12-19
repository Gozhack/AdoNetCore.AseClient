// ReSharper disable InconsistentNaming
namespace AdoNetCore.AseClient.Enum
{
    internal enum DynamicStatus : byte
    {
        /// <summary>
        /// No status associated with this dynamic command.
        /// </summary>
        TDS_DYNAMIC_UNUSED = 0x00,
        /// <summary>
        /// Parameter data stream follows the dynamic command.
        /// </summary>
        TDS_DYNAMIC_HASARGS = 0x01,
        /// <summary>
        /// If this statement, as identified by id, has previously sent TDS_ROWFMT information and this information has not changed, do not resend TDS_ROWFMT
        /// </summary>
        TDS_DYNAMIC_SUPPRESS_FMT = 0x02,
    }
}
