﻿using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Ifs.Fnd.AccessProvider;
using Ifs.Fnd.AccessProvider.PLSQL;

namespace Frends.Community.IFSAccessProvider
{
    public class IFSAccessProvider
    {
        /// <summary>
        /// Task for performing queries in Oracle databases using IFS Access Provider: http://ifsscan-odemo-2.cloudapp.net/ifsdoc/f1doc/foundation1/050_development/200_all_ref_manuals/070_dotnetap_ref/html/R_Project_APDocumentation.htm
        /// See documentation at https://github.com/CommunityHiQ/Frends.Community.IFSAccessProvider.IFSAccessProvider
        /// </summary>
        /// <param name="query"></param>
        /// <param name="output"></param>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Result }</returns>
        public static async Task<Output> Query([PropertyTab] QueryProperties queryInput, [PropertyTab] OutputProperties output, [PropertyTab] ConnectionProperties connection, CancellationToken cancellationToken)
        {
            var conn = new FndConnection(connection.Address, connection.Username, connection.Password)
            {
                AsynchronousMode = true,
                ConnectionTimeout = connection.TimeoutSeconds,
                CatchExceptions = false
            };

            var command = new FndPLSQLSelectCommand(conn, queryInput.Query);

            foreach (var param in queryInput.Parameters)
            {
                command.BindVariables.Add(Extensions.CreateFndParameter(param));
            }

            var queryResult = await command.ToJsonAsync(output, cancellationToken);
            
            return new Output { Result = queryResult };
        }

        public static async Task<Output> Command([PropertyTab] CommandProperties commandInput, [PropertyTab] ConnectionProperties connection, CancellationToken cancellationToken)
        {
            var conn = new FndConnection(connection.Address, connection.Username, connection.Password)
            {
                AsynchronousMode = true,
                ConnectionTimeout = connection.TimeoutSeconds,
                CatchExceptions = false
            };

                var command = new FndPLSQLCommand(conn, commandInput.Command);

            foreach (var param in commandInput.Parameters)
            {
                command.BindVariables.Add(Extensions.CreateFndParameter(param));
            }

            command.ExecuteNonQuery();

            return new Output { Result = "Command executed" };
        }
    }
}