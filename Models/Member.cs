using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Roster.Models
{
	public class Member : TableEntity
	{
		public Member() { }

		public Member(string lastName, string email)
		{
			PartitionKey = lastName;
			RowKey = email;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string MobilePhone { get; set; }
		public string SecondaryPhone { get; set; }
		public string Email { get; set; }
		public string SecondaryEmail { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string USHPA { get; set; }
		public string PrimaryRating { get; set; }
		public string SecondaryRating { get; set; }
		public bool HGObserver { get; set; }
		public bool PGObserver { get; set; }
		public bool HGInstructor { get; set; }
		public bool PGInstructor { get; set; }
		[Display(Name="Member Type")] public string MembershipType { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:d}")]
		[Display(Name="Joined")]
		public DateTime WebsiteJoinDate { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}")]
		[Display(Name="Expiration")]
		public DateTime? ExpirationDate { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}")]
		[Display(Name="Last Paid")]
		public DateTime? DateLastPaid { get; set; }
		public string PaymentMethod { get; set; }
		public decimal PaymentAmount { get; set; }
		public string PaymentStatus { get; set; }
		[Display(Name="Visiting")] public bool VisitingPilot { get; set; }
		public bool GoogleGroupMember { get; set; }
		public bool IgnoreWebsite { get; set; }
		public bool IgnoreGroupInvite { get; set; }
		public string WebsiteUsername { get; set; }
		public string ForumUsername { get; set; }
		public string HAMSign { get; set; }
		public string EmergencyContactName { get; set; }
		public string EmergencyContactPhone { get; set; }
		public bool Grandfathered { get; set; }
		public string Notes { get; set; }
		[DisplayFormat(DataFormatString = "{0:d}")] public DateTime Updated { get; set; }
		public bool DoNotSharePII { get; set; }
		public string DataSource { get; set; }
		public string TransactionId { get; set; }
		public int EntryId { get; set; }
	}
}