// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class VariablesRequest : Request<VariablesArguments>
{
    public VariablesRequest() => Command = "variables";
}

public class VariablesArguments
{
    [JsonPropertyName("variablesReference")]
    public int VariablesReference { get; set; }

    [JsonPropertyName("filter")]
    public string? Filter { get; set; }

    [JsonPropertyName("start")]
    public int? Start { get; set; }

    [JsonPropertyName("count")]
    public int? Count { get; set; }

    [JsonPropertyName("format")]
    public ValueFormat? Format { get; set; }
}

public static class VariablesFilter
{
    public const string Indexed = "indexed";
    public const string Named = "named";
}

public class VariablesResponse : Response<VariablesResponseBody>
{
}

public class VariablesResponseBody
{
    [JsonPropertyName("variables")]
    public Variable[] Variables { get; set; } = default!;
}
