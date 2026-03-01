// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class CompletionsRequest : Request<CompletionsArguments>
{
    public CompletionsRequest() => Command = "completions";
}

public class CompletionsArguments
{
    [JsonPropertyName("frameId")]
    public int? FrameId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    [JsonPropertyName("column")]
    public int Column { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }
}

public class CompletionsResponse : Response<CompletionsResponseBody>
{
}

public class CompletionsResponseBody
{
    [JsonPropertyName("targets")]
    public CompletionItem[] Targets { get; set; } = default!;
}
