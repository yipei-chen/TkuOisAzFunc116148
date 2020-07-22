using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace TkuOisAzFunc116148
{
    public static class EmailEmployeeinfo
    {
        [FunctionName("EmailEmployeeinfo")]
        public static void Run(
            [QueueTrigger("employees", Connection = "AzureWebJobsStorage")]string rowKey,
            [Table("employees", "employees", "{queuetrigger}")] Employee employee,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
            ILogger log)
        {
            log.LogInformation($" Employee PartitionKey={employee.PartitionKey}, RowKey={employee.RowKey}, EmpName={employee.EmpName}, EmpEmail={employee.EmpEmail}, EmpName={employee.EmpName}");
            
            message = new SendGridMessage();
            message.From = new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
            message.Subject = $"{employee.RowKey} + 's personal information(dev)";
            message.HtmlContent = $"employee.RowKey={employee.RowKey}, employee.EmpName={employee.EmpName}, employee.EmpEmail={employee.EmpEmail}";
            message.AddTo("120096@o365.tku.edu.tw");
            
            log.LogInformation("Finished.");
        }
    }
}