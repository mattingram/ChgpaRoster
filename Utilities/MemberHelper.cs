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
        private static CloudTable _table;
        public static CloudTable table => _table ?? Init();

        public static CloudTable Init()
        {
            _table = CloudTableHelper.Create("Members");
            return table;
        }

        public static Member GetMemberByUshpa(string ushpa)
        {
            var members = GetMembersByFilter($"USHPA eq '{ushpa}'");
            return members.Any() ? members.First() : null;
        }

        public static Member GetMemberByEmail(string email)
        {
            var members = GetMembersByFilter($"Email eq '{email}' or SecondaryEmail eq '{email}'");
            return members.Any() ? members.First() : null;
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

        public static IActionResult GetJsonWithFilter(string filter)
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
            return results.Result.ToList();
        }
        
        public static string FormatExpirationDate(Member member)
        {
            if (member == null)
            {
                return "Not found.";
            }
            if (member.ExpirationDate == DateTime.MinValue)
            {
                return "Active Member (No Expiration)";
            }
            if (member.ExpirationDate < DateTime.Now)
            {
                return "Expired: " + member.ExpirationDate?.ToShortDateString();
            }
            return $"Active Member ({member?.ExpirationDate?.ToShortDateString()})";
        }
    }
}