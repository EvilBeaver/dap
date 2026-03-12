// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if NETSTANDARD2_0
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#else
using System.Text.Json;
using System.Text.Json.Serialization;
#endif
using EvilBeaver.DAP.Dto.Base;
using System.Text;

namespace EvilBeaver.DAP.Dto.Serialization;

public static class DapSerializer
{
#if NETSTANDARD2_0
    private static readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Converters = { new DapMessageConverter() }
    };

    public static string Serialize(ProtocolMessage message)
    {
        return JsonConvert.SerializeObject(message, Settings);
    }

    public static ProtocolMessage? Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<ProtocolMessage>(json, Settings);
    }

    public static ProtocolMessage? Deserialize(byte[] utf8)
    {
        var json = Encoding.UTF8.GetString(utf8);
        return Deserialize(json);
    }
#else
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DapMessageConverter() }
    };

    public static string Serialize(ProtocolMessage message)
    {
        return JsonSerializer.Serialize(message, Options);
    }

    public static ProtocolMessage? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<ProtocolMessage>(json, Options);
    }

    public static ProtocolMessage? Deserialize(ReadOnlySpan<byte> utf8)
    {
        return JsonSerializer.Deserialize<ProtocolMessage>(utf8, Options);
    }
#endif
}
