using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.ReverseRequests;

public class RunInTerminalRequest : Request<RunInTerminalRequestArguments>
{
    public RunInTerminalRequest() => Command = "runInTerminal";
}

public class RunInTerminalRequestArguments
{
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("cwd")]
    public string Cwd { get; set; } = default!;

    [JsonPropertyName("args")]
    public string[] Args { get; set; } = default!;

    [JsonPropertyName("env")]
    public Dictionary<string, string?>? Env { get; set; }

    [JsonPropertyName("argsCanBeInterpretedByShell")]
    public bool? ArgsCanBeInterpretedByShell { get; set; }
}

public static class RunInTerminalKind
{
    public const string Integrated = "integrated";
    public const string External = "external";
}

public class RunInTerminalResponse : Response<RunInTerminalResponseBody>
{
}

public class RunInTerminalResponseBody
{
    [JsonPropertyName("processId")]
    public int? ProcessId { get; set; }

    [JsonPropertyName("shellProcessId")]
    public int? ShellProcessId { get; set; }
}
