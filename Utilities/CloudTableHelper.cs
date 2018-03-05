using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Roster.Models;

namespace Roster.Utilities
{
    public static class MemberHelper
    {
        public static Member GetMemberByUshpa(string ushpa)
        {
            return CloudTableHelper.GetMembersByFilter($"USHPA eq '{ushpa}'").FirstOrDefault();
        }

        public static Member GetMemberByEmail(string email)
        {
            return CloudTableHelper.GetMembersByFilter($"Email eq '{email}' or SecondaryEmail eq '{email}'").FirstOrDefault();
        }
    }

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
			var results = GetMembersByFilter(filter);
			if (results == null)
			{
				return new EmptyResult();
			}
			return new JsonResult(results);
        }

        public static IEnumerable<Member> GetMembersByFilter(string filter)
        {
			var query = new TableQuery<Member>().Where(filter);

            var token = new TableContinuationToken();
			var results = table.ExecuteQuerySegmentedAsync(query, token);
			if (results == null)
			{
				return null;
			}
			//return new JsonResult(results.Result.FirstOrDefault().ExpirationDate);
            return results.Result.ToList();
        }
    }
}