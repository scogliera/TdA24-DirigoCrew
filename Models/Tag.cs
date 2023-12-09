using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class Tag
{
    [JsonPropertyName("uuid")] public Guid Uuid { get; init; } = Guid.NewGuid();
    [JsonPropertyName("name")] public required string Name { get; set; }
}