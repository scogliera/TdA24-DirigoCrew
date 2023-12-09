using System.Text.Json.Serialization;

namespace TeacherDigitalAgency.Models;

public class Error
{
    [JsonPropertyName("code")] public required int Code { get; set; }
    [JsonPropertyName("message")] public required string Message { get; set; }
}