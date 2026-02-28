// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class StackFrameFormat : ValueFormat
{
    [JsonPropertyName("parameters")]
    public bool? Parameters { get; set; }

    [JsonPropertyName("parameterTypes")]
    public bool? ParameterTypes { get; set; }

    [JsonPropertyName("parameterNames")]
    public bool? ParameterNames { get; set; }

    [JsonPropertyName("parameterValues")]
    public bool? ParameterValues { get; set; }

    [JsonPropertyName("line")]
    public bool? Line { get; set; }

    [JsonPropertyName("module")]
    public bool? Module { get; set; }

    [JsonPropertyName("includeAll")]
    public bool? IncludeAll { get; set; }
}
