using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Ifs.Fnd.AccessProvider;
using Ifs.Fnd.AccessProvider.PLSQL;
using Ifs.Fnd.Data;

#pragma warning disable 1591

namespace Frends.Community.IFSAccessProvider
{
    public class IFSAccessProvider
    {
        /// <summary>
        /// Task for performing queries IFS Access Provider: http://ifsscan-odemo-2.cloudapp.net/ifsdoc/f1doc/foundation1/050_development/default.htm
        /// See documentation at https://github.com/CommunityHiQ/Frends.Community.IFSAccessProvider
        /// </summary>
        /// <param name="queryInput"></param>
        /// <param name="output"></param>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Result, string Message }</returns>
        public static async Task<Output> Query([PropertyTab] QueryProperties queryInput, [PropertyTab] OutputProperties output, [PropertyTab] ConnectionProperties connection, CancellationToken cancellationToken)
        {
            var conn = new FndConnection(connection.Address, connection.Username, connection.Password)
            {
                AsynchronousMode = connection.AsynchronousMode,
                ConnectionTimeout = connection.TimeoutSeconds,
                CatchExceptions = false
            };

            var command = new FndPLSQLSelectCommand(conn, queryInput.Query);

            foreach (var param in queryInput.Parameters)
            {
                command.BindVariables.Add(Extensions.CreateFndParameter(param));
            }

            var queryResult = await command.ToJsonAsync(output, cancellationToken);

            return new Output { Result = queryResult, Success = true, Message = null };
        }

        /// <summary>
        /// Task for performing commands using IFS Access Provider: http://ifsscan-odemo-2.cloudapp.net/ifsdoc/f1doc/foundation1/050_development/default.htm
        /// See documentation at https://github.com/CommunityHiQ/Frends.Community.IFSAccessProvider
        /// </summary>
        /// <param name="commandInput"></param>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Result, string Message }</returns>
        public static Output Command([PropertyTab] CommandProperties commandInput, [PropertyTab] ConnectionProperties connection, CancellationToken cancellationToken)
        {
            var conn = new FndConnection(connection.Address, connection.Username, connection.Password)
            {
                AsynchronousMode = connection.AsynchronousMode,
                ConnectionTimeout = connection.TimeoutSeconds,
                CatchExceptions = false
            };

            var command = new FndPLSQLCommand(conn, commandInput.Command);

            foreach (var param in commandInput.Parameters)
            {
                command.BindVariables.Add(Extensions.CreateFndParameter(param));
            }

            command.ExecuteNonQuery();

            return new Output { Result = "Command executed", Success = true, Message = null };
        }
    }
}
