// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class DisassembleRequest : Request<DisassembleArguments>
{
    public DisassembleRequest() => Command = "disassemble";
}

public class DisassembleArguments
{
    [JsonPropertyName("memoryReference")]
    public string MemoryReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("instructionOffset")]
    public int? InstructionOffset { get; set; }

    [JsonPropertyName("instructionCount")]
    public int InstructionCount { get; set; }

    [JsonPropertyName("resolveSymbols")]
    public bool? ResolveSymbols { get; set; }
}

public class DisassembleResponse : Response<DisassembleResponseBody>
{
}

public class DisassembleResponseBody
{
    [JsonPropertyName("instructions")]
    public DisassembledInstruction[] Instructions { get; set; } = default!;
}
