using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class Thread
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
