// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class StackFrame
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("source")]
    public Source? Source { get; set; }

    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("column")]
    public int Column { get; set; }

    [JsonPropertyName("endLine")]
    public int? EndLine { get; set; }

    [JsonPropertyName("endColumn")]
    public int? EndColumn { get; set; }

    [JsonPropertyName("canRestart")]
    public bool? CanRestart { get; set; }

    [JsonPropertyName("instructionPointerReference")]
    public string? InstructionPointerReference { get; set; }

    [JsonPropertyName("moduleId")]
    public object? ModuleId { get; set; }

    [JsonPropertyName("presentationHint")]
    public string? PresentationHint { get; set; }
}

public static class StackFramePresentationHint
{
    public const string Normal = "normal";
    public const string Label = "label";
    public const string Subtle = "subtle";
}
