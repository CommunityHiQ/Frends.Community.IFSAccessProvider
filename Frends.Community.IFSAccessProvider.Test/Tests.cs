using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Frends.Community.IFSAccessProvider.Tests
{
    public class Tests
    {
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
                Address = "",
                Username = "",
                Password = ""
            };

            var result = await IFSAccessProvider.Command(input, conn, new CancellationToken());

            Assert.Equal("Command executed", result.Result);
        }
    }
}
