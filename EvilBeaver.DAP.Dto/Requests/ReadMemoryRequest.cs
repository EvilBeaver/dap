// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ReadMemoryRequest : Request<ReadMemoryArguments>
{
    public ReadMemoryRequest() => Command = "readMemory";
}

public class ReadMemoryArguments
{
    [JsonPropertyName("memoryReference")]
    public string MemoryReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class ReadMemoryResponse : Response<ReadMemoryResponseBody>
{
}

public class ReadMemoryResponseBody
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;

    [JsonPropertyName("unreadableBytes")]
    public int? UnreadableBytes { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }
}
