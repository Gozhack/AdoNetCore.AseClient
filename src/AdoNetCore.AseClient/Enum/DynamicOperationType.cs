// ReSharper disable InconsistentNaming
namespace AdoNetCore.AseClient.Enum
{
    internal enum DynamicOperationType : byte
    {
        /// <summary>
        /// This is a request to prepare stmt.
        /// </summary>
        TDS_DYN_PREPARE = 0x01,
        /// <summary>
        /// This is a request to execute a prepared statement.
        /// </summary>
        TDS_DYN_EXEC = 0x02,
        /// <summary>
        /// Request to deallocate a prepared statement.
        /// </summary>
        TDS_DYN_DEALLOC = 0x04,
        /// <summary>
        /// This a request to prepare and execute stmt immediately
        /// </summary>
        TDS_DYN_EXEC_IMMED = 0x08,
        /// <summary>
        /// Is this used? If so what for?
        /// </summary>
        TDS_DYN_PROCNAME = 0x10,
        /// <summary>
        /// Acknowledge a dynamic command.
        /// </summary>
        TDS_DYN_ACK = 0x20,
        /// <summary>
        /// Send input format description.
        /// </summary>
        TDS_DYN_DESCIN = 0x40,
        /// <summary>
        /// Send output format description.
        /// </summary>
        TDS_DYN_DESCOUT = 0x80,
    }
}
