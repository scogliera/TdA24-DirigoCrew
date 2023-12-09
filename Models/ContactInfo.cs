using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class ContactInfo
{
    [JsonPropertyName("telephone_numbers")] public required HashSet<string> TelephoneNumbers { get; set; }
    [JsonPropertyName("emails")] public required HashSet<string> Emails { get; set; }
}