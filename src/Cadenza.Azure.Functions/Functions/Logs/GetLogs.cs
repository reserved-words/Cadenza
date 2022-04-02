using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using Cadenza.Azure.Functions.Models;
using System.Linq;

namespace Cadenza.Azure.Logs
{
    public static class GetLogs
    {
        [FunctionName("GetLogs")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Table("Logs", Connection = "AzureWebJobsStorage")] CloudTable logTable,
            ILogger log)
        {
            var filter = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PartitionKey.Log.ToString()),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForBool(nameof(LogEntity.Cleared), QueryComparisons.Equal, false));

            var query = new TableQuery<LogEntity>().Where(filter);

            var results = (await logTable.ExecuteQuerySegmentedAsync(query, null))
                .OrderByDescending(r => r.DateCreated)
                .ToList();

            return new OkObjectResult(results);
        }
    }
}
