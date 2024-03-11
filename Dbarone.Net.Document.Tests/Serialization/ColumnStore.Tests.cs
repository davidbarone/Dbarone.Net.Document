using Dbarone.Net.Csv;
using Xunit;

namespace Dbarone.Net.Document.Tests;

public class ColumnStoreTests
{

    [Fact]
    public void TestAgeDataset()
    {
        // Use Age dataset:
        using (var s = File.OpenRead(@"..\..\..\Datasets\AgeDataset-V1.csv"))
        {
            CsvConfiguration configuration = new CsvConfiguration
            {
                HasHeader = true,
                LineSeparator = "\n",
                ProcessRowHandler = (int record, string[] headers, ref object[]? values) =>
                {
                    values![7] = int.Parse((string)values[7]);
                    values![8] = int.Parse((string)values[8]);
                    values![10] = int.Parse((string)values[10]);
                    return true;
                }
            };
            CsvReader csv = new CsvReader(s, configuration);
            var data = csv.Read();

            DictionaryDocument dd = new DictionaryDocument();
            // Create new DocumentArray
            DocumentArray da = new DocumentArray(data.Select(d => new DocumentValue()));
        }
    }
}