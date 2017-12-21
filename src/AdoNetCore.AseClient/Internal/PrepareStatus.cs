using System.Collections.Generic;

namespace AdoNetCore.AseClient.Internal
{
    internal enum PrepareStatus
    {
        /// <summary>
        /// Prepare() hasn't yet been called on the command
        /// </summary>
        NotEnabled,
        /// <summary>
        /// Prepare() has been called, and a prepare-execute type TDS token will be sent
        /// </summary>
        Pending,
        /// <summary>
        /// prepare-execute has been sent, so further calls will be just -execute
        /// </summary>
        Prepared,
        /// <summary>
        /// The command text has changed since the command was prepared. Need to prepare-execute again (possibly need to first remove the existing prepared statement?)
        /// </summary>
        Dirty
    }

    internal static class PrepareStatusExtensions
    {
        private static Dictionary<PrepareStatus, PrepareStatus> Transitions = new Dictionary<PrepareStatus, PrepareStatus>
        {
            {PrepareStatus.NotEnabled, PrepareStatus.Pending},
            {PrepareStatus.Pending, PrepareStatus.Prepared},
            {PrepareStatus.Dirty, PrepareStatus.Prepared},
            {PrepareStatus.Prepared, PrepareStatus.Dirty}
        };

        internal static bool CanTransitionTo(this PrepareStatus from, PrepareStatus to)
        {
            return Transitions.ContainsKey(from) && Transitions[from] == to;
        }
    }
}
