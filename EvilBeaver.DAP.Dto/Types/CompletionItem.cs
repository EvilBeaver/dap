// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class CompletionItem
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("sortText")]
    public string? SortText { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("start")]
    public int? Start { get; set; }

    [JsonPropertyName("length")]
    public int? Length { get; set; }

    [JsonPropertyName("selectionStart")]
    public int? SelectionStart { get; set; }

    [JsonPropertyName("selectionLength")]
    public int? SelectionLength { get; set; }
}

public static class CompletionItemType
{
    public const string Method = "method";
    public const string Function = "function";
    public const string Constructor = "constructor";
    public const string Field = "field";
    public const string Variable = "variable";
    public const string Class = "class";
    public const string Interface = "interface";
    public const string Module = "module";
    public const string Property = "property";
    public const string Unit = "unit";
    public const string Value = "value";
    public const string Enum = "enum";
    public const string Keyword = "keyword";
    public const string Snippet = "snippet";
    public const string Text = "text";
    public const string Color = "color";
    public const string File = "file";
    public const string Reference = "reference";
    public const string CustomColor = "customcolor";
}
