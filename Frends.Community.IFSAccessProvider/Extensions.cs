using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ifs.Fnd.AccessProvider.PLSQL;
using Ifs.Fnd.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Frends.Community.IFSAccessProvider
{
    internal static class Extensions
    {
        public static dynamic GetValueAttribute(QueryParameter param)
        {
            switch (param.DataType)
            {
                case QueryParameterType.Binary:
                    return new FndBinaryAttribute(param.Value);
                case QueryParameterType.Text:
                    return new FndTextAttribute(param.Value);
                case QueryParameterType.LongText:
                    return new FndLongTextAttribute(param.Value);
                case QueryParameterType.Decimal:
                    return new FndDecimalAttribute(param.Value);
                case QueryParameterType.Number:
                    return new FndNumberAttribute(param.Value);
                case QueryParameterType.Timestamp:
                    return new FndTimeStampAttribute(param.Value);
                default:
                    throw new ArgumentException("Parameter type not recognized");
            }
        }

        public static FndBindVariable CreateFndParameter(QueryParameter parameter)
        {
            return new FndBindVariable
            {
                Direction = FndBindVariableDirection.In,
                Name = parameter.Name,
                Value = GetValueAttribute(parameter)
            };
        }

        /// <summary>
        /// Write query results to json string or file
        /// </summary>
        /// <param name="command"></param>
        /// <param name="output"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> ToJsonAsync(this FndPLSQLSelectCommand command, OutputProperties output, CancellationToken cancellationToken)
        {
            var culture = string.IsNullOrWhiteSpace(output.CultureInfo) ? CultureInfo.InvariantCulture : new CultureInfo(output.CultureInfo);

            // utf-8 as default encoding
            Encoding encoding = string.IsNullOrWhiteSpace(output.OutputFile?.Encoding) ? Encoding.UTF8 : Encoding.GetEncoding(output.OutputFile.Encoding);

            // create json result
            using (var fileWriter = output.OutputToFile ? new StreamWriter(output.OutputFile.Path, false, encoding) : null)
            using (var writer = output.OutputToFile ? new JsonTextWriter(fileWriter) : new JTokenWriter() as JsonWriter)
            {
                writer.Formatting = Formatting.Indented;
                writer.Culture = culture;

                // start array
                await writer.WriteStartArrayAsync(cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
                FndDataTable reader = command.ExecuteReader();
                for (var j = 0; j < reader.Rows.Count; j++)
                {
                    foreach (FndDataColumn a in reader.Columns)
                    {
                        var resultValue = Convert.ToString(reader.Rows[j][a.Name]);

                        // start row object
                        await writer.WriteStartObjectAsync(cancellationToken);

                        // add row element name
                        await writer.WritePropertyNameAsync(a.Name, cancellationToken);
                        // add row element value
                        await writer.WriteValueAsync(resultValue, cancellationToken);
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }

                await writer.WriteEndObjectAsync(cancellationToken); // end row object

                cancellationToken.ThrowIfCancellationRequested();

                // end array
                await writer.WriteEndArrayAsync(cancellationToken);

                if (output.OutputToFile && output.OutputFile != null)
                {
                    return output.OutputFile.Path;
                }

                return ((JTokenWriter)writer).Token.ToString();
            }
        }
    }
}
