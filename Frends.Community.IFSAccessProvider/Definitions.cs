using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.IFSAccessProvider
{
#pragma warning disable 1591

    /// <summary>
    /// Enumerator representing IFS parameter data types
    /// </summary>
    public enum QueryParameterType
    {
        Binary, Text, LongText, Decimal, Number, Timestamp
    }

    public class QueryProperties
    {
        [DisplayFormat(DataFormatString = "Sql")]
        [DefaultValue("SELECT ColumnName FROM TableName")]
        public string Query { get; set; }

        /// <summary>
        /// Parameters for the database query
        /// </summary>
        public QueryParameter[] Parameters { get; set; }
    }

    public class QueryParameter
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        [DefaultValue("ParameterName")]
        [DisplayFormat(DataFormatString = "Text")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the parameter
        /// </summary>
        [DefaultValue("Parameter value")]
        [DisplayFormat(DataFormatString = "Text")]
        public dynamic Value { get; set; }

        /// <summary>
        /// The type of the parameter
        /// </summary>
        [DefaultValue(QueryParameterType.Text)]
        public QueryParameterType DataType { get; set; }
    }

    public class OutputProperties
    {
        public JsonOutputProperties JsonOutput { get; set; }

        /// <summary>
        /// In case user wants to write results to a file instead of returning them to process
        /// </summary>
        public bool OutputToFile { get; set; }

        /// <summary>
        /// Output file properties
        /// </summary>
        [UIHint(nameof(OutputToFile), "", true)]
        public OutputFileProperties OutputFile { get; set; }
    }

    public class ConnectionProperties
    {
        /// <summary>
        /// IFS address
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Address { get; set; }

        /// <summary>
        /// IFS username
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Username { get; set; }

        /// <summary>
        /// IFS password
        /// </summary>
        [PasswordPropertyText]
        [DisplayFormat(DataFormatString = "Text")]
        public string Password { get; set; }

        /// <summary>
        /// Timeout value in seconds
        /// </summary>
        [DefaultValue(30)]
        public int TimeoutSeconds { get; set; }
    }

    /// <summary>
    /// Result to be returned from task
    /// </summary>
    public class Output
    {
        public string Result { get; set; }
    }
    
    /// <summary>
    /// Json output specific properties
    /// </summary>
    public class JsonOutputProperties
    {
        /// <summary>
        /// Specify the culture info to be used when parsing result to JSON. If this is left empty InvariantCulture will be used. List of cultures: https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx Use the Language Culture Name.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string CultureInfo { get; set; }
    }

    /// <summary>
    /// Properties for when user wants to write the result directly into a file
    /// </summary>
    public class OutputFileProperties
    {
        /// <summary>
        /// Query output filepath
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("c:\\temp\\output.csv")]
        public string Path { get; set; }

        /// <summary>
        /// Output file encoding
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("utf-8")]
        public string Encoding { get; set; }
    }
}