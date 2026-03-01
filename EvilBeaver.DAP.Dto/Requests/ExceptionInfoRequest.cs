// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ExceptionInfoRequest : Request<ExceptionInfoArguments>
{
    public ExceptionInfoRequest() => Command = "exceptionInfo";
}

public class ExceptionInfoArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public class ExceptionInfoResponse : Response<ExceptionInfoResponseBody>
{
}

public class ExceptionInfoResponseBody
{
    [JsonPropertyName("exceptionId")]
    public string ExceptionId { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("breakMode")]
    public string BreakMode { get; set; } = default!;

    [JsonPropertyName("details")]
    public ExceptionDetails? Details { get; set; }
}
