using System.Text.Json.Serialization;

namespace FastAQ.Models.ApiResponeEntity;

public class ApiResponeEntity
{
    [JsonPropertyName("is_action_success")]
    public required bool IsSuccess { get; set; }

    [JsonPropertyName("result")]
    public object Result { get; set; }

    [JsonPropertyName("fail_info")]
    public string FailInfo { get; set; } = string.Empty;
    

}