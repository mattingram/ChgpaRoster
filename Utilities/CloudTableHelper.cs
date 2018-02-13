using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roster.Models;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
//using Microsoft.Azure;
//using Microsoft.Azure.CosmosDB.Table;
//using Microsoft.Azure.Storage;

namespace Roster.Utilities
{
    public class CloudTableHelper
    {
        private static string _connectionString;
        private static CloudTable table;

        public CloudTableHelper()
        {
            _connectionString = Environment.GetEnvironmentVariable("connectionString");
			//string connectionString = CloudConfigurationManager.GetSetting("rosterStorage");
            
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			table = tableClient.GetTableReference("Members");
			table.CreateIfNotExistsAsync();
        }
                
        public Member GetMember(string lastName, string email)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Member>(lastName, email);
            var result = table.ExecuteAsync(retrieveOperation);
            if (result == null)
            {
                return null;
            }
            return result.Result.Result as Member;
        }

        public IActionResult GetWithFilter(string filter)
        {
			var query = new TableQuery<Member>().Where(filter);

            var token = new TableContinuationToken();
			var results = table.ExecuteQuerySegmentedAsync(query, token);
			if (results == null)
			{
				return new EmptyResult();
			}
			return new JsonResult(results.Result);
        }
    }
}