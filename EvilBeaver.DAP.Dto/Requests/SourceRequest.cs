// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SourceRequest : Request<SourceArguments>
{
    public SourceRequest() => Command = "source";
}

public class SourceArguments
{
    [JsonPropertyName("source")]
    public Source? Source { get; set; }

    [JsonPropertyName("sourceReference")]
    public int SourceReference { get; set; }
}

public class SourceResponse : Response<SourceResponseBody>
{
}

public class SourceResponseBody
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }
}
