using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Roster.Models
{
	public class Registration : TableEntity
	{
		public Registration() { }

		public Registration(string rowKey)
		{
			PartitionKey = "Registration";
			RowKey = rowKey;
		}
    }
}