using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using Cadenza.Azure.Functions.Models;
using System.Linq;

namespace Cadenza.Azure.Overrides
{
    public static class GetOverrides
    {
        [FunctionName("GetOverrides")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Table("Overrides", Connection = "AzureWebJobsStorage")] CloudTable overridesTable,
            ILogger log)
        {
            var query = new TableQuery<OverrideEntity>();

            var results = (await overridesTable.ExecuteQuerySegmentedAsync(query, null))
                .OrderBy(r => r.PartitionKey)
                .ThenBy(r => r.RowKey)
                .ToList();

            return new OkObjectResult(results);
        }
    }
}
