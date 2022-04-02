using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Cadenza.Azure.Functions.Models;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

namespace Cadenza.Azure.Logs
{
    public static class ClearLog
    {
        [FunctionName("ClearLog")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            [Table("Logs", Connection = "AzureWebJobsStorage")] CloudTable logTable,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            List<string> idsToClear = new List<string>();

            try
            {
                idsToClear = JsonConvert.DeserializeObject<List<string>>(requestBody);
            }
            catch
            {
                return new BadRequestObjectResult("Please include in the request body a list of ApplicationName / LogID pairs to clear");
            }

            var cleared = 0;
            var notFound = 0;
            var alreadyCleared = 0;

            foreach (var id in idsToClear)
            {
                var retrieveOperation = TableOperation.Retrieve<LogEntity>(PartitionKey.Log.ToString(), id);
                var result = await logTable.ExecuteAsync(retrieveOperation);

                if (result.Result is LogEntity entityFound)
                {
                    if (entityFound.Cleared)
                    {
                        log.LogInformation("Found but already cleared");
                        alreadyCleared++;
                    }
                    else
                    {
                        entityFound.Cleared = true;
                        var replaceOperation = TableOperation.Replace(entityFound);
                        await logTable.ExecuteAsync(replaceOperation);
                        log.LogInformation("Found and cleared");
                        cleared++;
                    }
                }
                else
                {
                    log.LogInformation("Not found");
                    notFound++;
                }
            }

            var response =
                (cleared > 0 ? $"{cleared} logs cleared. " : "")
                + (notFound > 0 ? $"{notFound} logs not found. " : "")
                + (alreadyCleared > 0 ? $"{alreadyCleared} logs found but already cleared. " : "");

            return new OkObjectResult(response);
        }
    }
}
