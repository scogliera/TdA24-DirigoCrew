using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class Lecturer
{
    public Lecturer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    [JsonPropertyName("uuid")] public Guid Uuid { get; init; } = Guid.NewGuid();
    [JsonPropertyName("title_before")] public string? TitleBefore { get; set; }
    [JsonPropertyName("first_name")] public required string FirstName { get; set; }
    [JsonPropertyName("middle_name")] public string? MiddleName { get; set; }
    [JsonPropertyName("last_name")] public required string LastName { get; set; }
    [JsonPropertyName("title_after")] public string? TitleAfter { get; set; }
    [JsonPropertyName("picture_url")] public string? PictureUrl { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("claim")] public string? Claim { get; set; }
    [JsonPropertyName("bio")] public string? Bio { get; set; }
    [JsonPropertyName("tags")] public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    [JsonPropertyName("price_per_hour")] public int? PricePerHour { get; set; }
    // TODO: Not known if contact can be nullable - most likely yes
    [JsonPropertyName("contact")] public ContactInfo? ContactInfo { get; set; }
}