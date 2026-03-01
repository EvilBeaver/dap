// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class VariablePresentationHint
{
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("attributes")]
    public string[]? Attributes { get; set; }

    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }

    [JsonPropertyName("lazy")]
    public bool? Lazy { get; set; }
}

public static class VariableKind
{
    public const string Property = "property";
    public const string Method = "method";
    public const string Class = "class";
    public const string Data = "data";
    public const string Event = "event";
    public const string BaseClass = "baseClass";
    public const string InnerClass = "innerClass";
    public const string Interface = "interface";
    public const string MostDerivedClass = "mostDerivedClass";
    public const string Virtual = "virtual";
    public const string DataBreakpoint = "dataBreakpoint";
}

public static class VariableAttribute
{
    public const string Static = "static";
    public const string Constant = "constant";
    public const string ReadOnly = "readOnly";
    public const string RawString = "rawString";
    public const string HasObjectId = "hasObjectId";
    public const string CanHaveObjectId = "canHaveObjectId";
    public const string HasSideEffects = "hasSideEffects";
    public const string HasDataBreakpoint = "hasDataBreakpoint";
}

public static class VariableVisibility
{
    public const string Public = "public";
    public const string Private = "private";
    public const string Protected = "protected";
    public const string Internal = "internal";
    public const string Final = "final";
}
