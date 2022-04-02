using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using System;
using Cadenza.Azure.Functions.Models;
using System.Linq;

namespace Cadenza.Azure.Logs
{
    public static class DeleteClearedLogs
    {
        [FunctionName("DeleteClearedLogs")]
        public static async Task Run(
            [TimerTrigger("0 0 0 * * *")] TimerInfo myTimer,
            [Table("Logs", Connection = "AzureWebJobsStorage")] CloudTable logTable, 
            ILogger log)
        {
            var monthAgo = DateTime.Now.AddMonths(-1);
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            var clearedFilter = TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForDate("DateCreated", QueryComparisons.LessThan, monthAgo),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForBool("Cleared", QueryComparisons.Equal, true));

            var oldFilter = TableQuery.GenerateFilterConditionForDate("DateCreated", QueryComparisons.LessThan, threeMonthsAgo);

            var clearedQuery = new TableQuery<LogEntity>().Where(clearedFilter);
            var oldQuery = new TableQuery<LogEntity>().Where(oldFilter);

            var clearedErrors = (await logTable.ExecuteQuerySegmentedAsync(clearedQuery, null)).ToList();
            var oldErrors = (await logTable.ExecuteQuerySegmentedAsync(oldQuery, null)).ToList();

            foreach (var entity in clearedErrors)
            {
                await logTable.ExecuteAsync(TableOperation.Delete(entity));
            }

            foreach (var entity in oldErrors)
            {
                await logTable.ExecuteAsync(TableOperation.Delete(entity));
            }
        }
    }
}
