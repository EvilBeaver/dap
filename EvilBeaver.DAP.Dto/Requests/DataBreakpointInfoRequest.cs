// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class DataBreakpointInfoRequest : Request<DataBreakpointInfoArguments>
{
    public DataBreakpointInfoRequest() => Command = "dataBreakpointInfo";
}

public class DataBreakpointInfoArguments
{
    [JsonPropertyName("variablesReference")]
    public int? VariablesReference { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("frameId")]
    public int? FrameId { get; set; }

    [JsonPropertyName("bytes")]
    public int? Bytes { get; set; }

    [JsonPropertyName("asAddress")]
    public bool? AsAddress { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}

public class DataBreakpointInfoResponse : Response<DataBreakpointInfoResponseBody>
{
}

public class DataBreakpointInfoResponseBody
{
    [JsonPropertyName("dataId")]
    public string? DataId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("accessTypes")]
    public string[]? AccessTypes { get; set; }

    [JsonPropertyName("canPersist")]
    public bool? CanPersist { get; set; }
}
