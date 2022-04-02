using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using Cadenza.Azure.Functions.Models;

namespace Cadenza.Azure.Overrides
{
    public static class AddOverride
    {
        [FunctionName("AddOverride")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("Overrides", Connection = "AzureWebJobsStorage")] CloudTable overridesTable,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var model = new OverrideEntity
            {
                PartitionKey = data.id,
                RowKey = data.propertyName,
                Item = data.item,
                ItemType = data.itemType,
                OriginalValue = data.originalValue,
                OverrideValue = data.overrideValue
            };

            var retrieveOperation = TableOperation.Retrieve<OverrideEntity>(model.PartitionKey, model.RowKey);

            var result = await overridesTable.ExecuteAsync(retrieveOperation);

            if (result.Result is OverrideEntity entityFound)
            {
                entityFound.OverrideValue = model.OverrideValue;
                var replaceOperation = TableOperation.Replace(entityFound);
                await overridesTable.ExecuteAsync(replaceOperation);
                log.LogInformation("Updated existing");
            }
            else
            {
                var insertOperation = TableOperation.Insert(model);
                await overridesTable.ExecuteAsync(insertOperation);
                log.LogInformation("Inserted new");
            }

            return new OkObjectResult("ok");
        }
    }
}
