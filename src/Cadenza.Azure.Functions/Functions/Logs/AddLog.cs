using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Cadenza.Azure.Functions.Models;
using System;

namespace Cadenza.Azure.Logs
{
    public static class AddLog
    {
        [FunctionName("AddLog")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("Logs", Connection = "AzureWebJobsStorage")] IAsyncCollector<LogEntity> logTable,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string message = data.message;
            string levelString = data.level;
            string stackTrace = data.stackTrace;

            if (string.IsNullOrEmpty(message))
            {
                return new BadRequestObjectResult("Please include 'message' parameter");
            }

            if (string.IsNullOrEmpty(levelString))
            {
                return new BadRequestObjectResult("Please include 'level' parameter");
            }

            if (!int.TryParse(levelString, out int level) || level < 0 || level > 3)
            {
                return new BadRequestObjectResult("Level must be a number between 0 and 3");
            }

            var row = new LogEntity
            {
                PartitionKey = PartitionKey.Log.ToString(),
                RowKey = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                Message = message,
                Cleared = false,
                Level = level,
                StackTrace = stackTrace
            };

            await logTable.AddAsync(row);

            return new OkObjectResult("Message successfully logged");
        }
    }
}
