using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class ContactInfo
{
    // TODO: Must validate uniqueness of inputs
    [JsonPropertyName("telephone_numbers")] public required IEnumerable<string> TelephoneNumbers { get; set; }
    [JsonPropertyName("emails")] public required IEnumerable<string> Emails { get; set; }
}