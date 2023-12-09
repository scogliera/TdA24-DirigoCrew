using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class Tag
{
    [JsonPropertyName("uuid")] public required string Uuid { get; init; }
    [JsonPropertyName("name")] public required string Name { get; set; }
}