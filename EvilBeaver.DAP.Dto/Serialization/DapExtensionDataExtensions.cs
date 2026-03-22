// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Requests;

#if NETSTANDARD2_0
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#else
using System.Text.Json;
#endif

namespace EvilBeaver.DAP.Dto.Serialization;

/// <summary>
/// Helpers for adapter-specific <c>launch</c>/<c>attach</c> arguments captured via JSON extension data.
/// </summary>
public static class DapExtensionDataExtensions
{
#if NETSTANDARD2_0
    /// <summary>
    /// Deserializes overflow properties from <paramref name="arguments"/> into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(this LaunchRequestArguments arguments, JsonSerializerSettings? settings = null)
        => DeserializeAdditionalProperties<T>(arguments.AdditionalData, settings);

    /// <summary>
    /// Deserializes overflow properties from <paramref name="arguments"/> into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(this AttachRequestArguments arguments, JsonSerializerSettings? settings = null)
        => DeserializeAdditionalProperties<T>(arguments.AdditionalData, settings);

    /// <summary>
    /// Deserializes a dictionary of extension properties into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(Dictionary<string, JToken>? extensionData, JsonSerializerSettings? settings = null)
    {
        settings ??= DapSerializer.ExtensionDeserializeSettings;
        if (extensionData is null || extensionData.Count == 0)
            return JsonConvert.DeserializeObject<T>("{}", settings)!;

        var jo = new JObject();
        foreach (var kv in extensionData)
            jo[kv.Key] = kv.Value;

        return jo.ToObject<T>(JsonSerializer.Create(settings))!;
    }
#else
    /// <summary>
    /// Deserializes overflow properties from <paramref name="arguments"/> into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(this LaunchRequestArguments arguments, JsonSerializerOptions? options = null)
        => DeserializeAdditionalProperties<T>(arguments.AdditionalData, options);

    /// <summary>
    /// Deserializes overflow properties from <paramref name="arguments"/> into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(this AttachRequestArguments arguments, JsonSerializerOptions? options = null)
        => DeserializeAdditionalProperties<T>(arguments.AdditionalData, options);

    /// <summary>
    /// Deserializes a dictionary of extension properties into <typeparamref name="T"/>.
    /// </summary>
    public static T DeserializeAdditionalProperties<T>(Dictionary<string, JsonElement>? extensionData, JsonSerializerOptions? options = null)
    {
        options ??= DapSerializer.ExtensionDeserializeOptions;
        if (extensionData is null || extensionData.Count == 0)
            return JsonSerializer.Deserialize<T>("{}", options)!;

        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartObject();
            foreach (var (key, value) in extensionData)
            {
                writer.WritePropertyName(key);
                value.WriteTo(writer);
            }
            writer.WriteEndObject();
        }

        return JsonSerializer.Deserialize<T>(stream.ToArray(), options)!;
    }
#endif
}
