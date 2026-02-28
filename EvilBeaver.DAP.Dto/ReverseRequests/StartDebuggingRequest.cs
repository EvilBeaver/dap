// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.ReverseRequests;

public class StartDebuggingRequest : Request<StartDebuggingRequestArguments>
{
    public StartDebuggingRequest() => Command = "startDebugging";
}

public class StartDebuggingRequestArguments
{
    [JsonPropertyName("configuration")]
    public Dictionary<string, object> Configuration { get; set; } = default!;

    [JsonPropertyName("request")]
    public string RequestType { get; set; } = default!;
}

public static class StartDebuggingRequestType
{
    public const string Launch = "launch";
    public const string Attach = "attach";
}

public class StartDebuggingResponse : Response
{
}
