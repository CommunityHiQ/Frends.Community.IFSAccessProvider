using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Frends.Community.IFSAccessProvider.Tests
{
    public class Tests
    {
        private const string Address = "";
        private const string Username = "";
        private const string Password = "";

        [Fact]
        public async Task ExecuteOracleCommand()
        {
            /* Create test table with the following script before running the test:
             * CREATE TABLE TestTable ( textField VARCHAR(255) );
             */

            var input = new CommandProperties
            {
                Command = "INSERT INTO TestTable (textField) VALUES ('unit test text')"
            };

            var conn = new ConnectionProperties
            {
                Address = Address,
                Username = Username,
                Password = Password
            };

            var result = await IFSAccessProvider.Command(input, conn, new CancellationToken());

            Assert.Equal("Command executed", result.Result);
        }

        [Fact]
        public async Task ExecuteOracleSelect()
        {
            QueryParameter[] qpList = new QueryParameter[1];
            qpList[0] = new QueryParameter { Name = "DESC", Value = "unit test text", DataType = QueryParameterType.Text };

            var input = new QueryProperties
            {
                Query = "SELECT * FROM TestTable WHERE textField LIKE :DESC",
                Parameters = qpList
            };

            var conn = new ConnectionProperties
            {
                Address = Address,
                Username = Username,
                Password = Password
            };

            var output = new OutputProperties { CultureInfo = null, OutputToFile = false };
            var result = await IFSAccessProvider.Query(input, output, conn, new CancellationToken());

            Assert.Equal("Command executed", result.Result);
        }
    }
}
