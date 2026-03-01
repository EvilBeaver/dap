// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionOptions
{
    [JsonPropertyName("path")]
    public ExceptionPathSegment[]? Path { get; set; }

    [JsonPropertyName("breakMode")]
    public string BreakMode { get; set; } = default!;
}

public static class ExceptionBreakMode
{
    public const string Never = "never";
    public const string Always = "always";
    public const string Unhandled = "unhandled";
    public const string UserUnhandled = "userUnhandled";
}
