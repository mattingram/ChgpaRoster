using System;
using System.Runtime.Serialization;

namespace Roster.Models
{
	[DataContract]
	public class Registration
	{
		[DataMember(Name="id")] public int Id { get; set; }
		[DataMember(Name="2")] public string FirstName { get; set; }
		[DataMember(Name="1")] public string LastName { get; set; }
		[DataMember(Name="4")] public string MembershipType { get; set; }
		[DataMember(Name="10")] public string UserName { get; set; }
		[DataMember(Name="11")] public string Email { get; set; }
		[DataMember(Name="13")] public string Phone { get; set; }
		[DataMember(Name="25")] public string Ushpa { get; set; }
		[DataMember(Name="26")] public string Rating { get; set; }
		[DataMember(Name="29")] public string EmergencyContactName { get; set; }
		[DataMember(Name="28")] public string EmergencyContactPhone { get; set; }
		[DataMember(Name="date_created")] public string DateCreatedString { get; set; }
		public DateTime DateCreated => DateTime.Parse(DateCreatedString);
		[DataMember(Name="payment_status")] public string PaymentStatus { get; set; }
		[DataMember(Name="payment_amount")] public decimal? PaymentAmount { get; set; }
		[DataMember(Name="18")] public string PaymentMethod { get; set; }
		[DataMember(Name="status")] public string Status { get; set; }
		public bool Active => Status == "active";
	}

	[DataContract]
	public class GravityFormRegistrationResponse
	{
		[DataMember(Name="total_count")]
		public int Count { get; set; }

		[DataMember(Name="entries")]
		public Registration[] Registrations { get; set; }
	}

	[DataContract]
	public class GravityFormRegistration
	{
		[DataMember(Name="response")]
		public GravityFormRegistrationResponse Response { get; set; }
    }
}