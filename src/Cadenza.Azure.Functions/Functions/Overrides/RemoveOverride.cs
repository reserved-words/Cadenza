using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace Cadenza.Azure.Overrides
{
    public static class RemoveOverride
    {
        [FunctionName("RemoveOverride")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            [Table("Overrides", Connection = "AzureWebJobsStorage")] CloudTable overridesTable,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var partitionKey = data.id.ToString();
            var rowKey = data.propertyName.ToString();

            try
            {
                var retrieveOperation = TableOperation.Retrieve<TableEntity>(partitionKey, rowKey);

                var result = await overridesTable.ExecuteAsync(retrieveOperation);

                if (result.Result is TableEntity entityFound)
                {
                    await overridesTable.ExecuteAsync(TableOperation.Delete(entityFound));
                    return new OkObjectResult("removed");
                }
                else
                {
                    return new NotFoundObjectResult("not found");
                }

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
