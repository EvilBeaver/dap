using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ValueFormat
{
    [JsonPropertyName("hex")]
    public bool? Hex { get; set; }
}
