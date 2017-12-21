// ReSharper disable InconsistentNaming
namespace AdoNetCore.AseClient.Enum
{
    internal enum DynamicOperationType : byte
    {
        /// <summary>
        /// This is a request to prepare stmt.
        /// Supports: Stmt value
        /// </summary>
        TDS_DYN_PREPARE = 0x01,
        /// <summary>
        /// This is a request to execute a prepared statement.
        /// Parameter names are not supported in the TDS_PARAMFMT associated with the TDS_DYN_EXEC
        /// </summary>
        TDS_DYN_EXEC = 0x02,
        /// <summary>
        /// Request to deallocate a prepared statement.
        /// </summary>
        TDS_DYN_DEALLOC = 0x04,
        /// <summary>
        /// This a request to prepare and execute stmt immediately
        /// Supports: Stmt value
        /// Parameters are not supported in the TDS_DYN_EXEC_IMMED data stream
        /// The IdLen argument must be 0 for a TDS_DYN_EXEC_IMMED data stream
        /// No results can be returned by a server in response to a TDS_DYN_EXEC_IMMED command.
        /// The only valid response is a TDS_DONE
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
