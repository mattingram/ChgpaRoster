using System;
using Microsoft.AspNetCore.Mvc;
using Roster.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Roster.Utilities
{
    public static class CloudTableHelper
    {
        private static string _connectionString;
        private static CloudTable _table;
        private static CloudTable table => _table ?? Init();

        private static CloudTable Init()
        {
            _connectionString = Environment.GetEnvironmentVariable("connectionString");
			//string connectionString = CloudConfigurationManager.GetSetting("rosterStorage");
            
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("Members");
			_table.CreateIfNotExistsAsync();
            return _table;
        }
                
        public static Member GetMember(string lastName, string email)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Member>(lastName, email);
            var result = table.ExecuteAsync(retrieveOperation);
            if (result == null)
            {
                return null;
            }
            return result.Result.Result as Member;
        }

        public static IActionResult GetWithFilter(string filter)
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