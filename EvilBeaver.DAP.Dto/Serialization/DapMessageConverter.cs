// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if NETSTANDARD2_0
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#else
using System.Text.Json;
using System.Text.Json.Serialization;
#endif
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Events;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Dto.ReverseRequests;

namespace EvilBeaver.DAP.Dto.Serialization;

#if NETSTANDARD2_0
public class DapMessageConverter : JsonConverter
{
    private static readonly Dictionary<string, Type> RequestTypes = new()
    {
        ["cancel"] = typeof(CancelRequest),
        ["initialize"] = typeof(InitializeRequest),
        ["configurationDone"] = typeof(ConfigurationDoneRequest),
        ["launch"] = typeof(LaunchRequest),
        ["attach"] = typeof(AttachRequest),
        ["restart"] = typeof(RestartRequest),
        ["disconnect"] = typeof(DisconnectRequest),
        ["terminate"] = typeof(TerminateRequest),
        ["breakpointLocations"] = typeof(BreakpointLocationsRequest),
        ["setBreakpoints"] = typeof(SetBreakpointsRequest),
        ["setFunctionBreakpoints"] = typeof(SetFunctionBreakpointsRequest),
        ["setExceptionBreakpoints"] = typeof(SetExceptionBreakpointsRequest),
        ["dataBreakpointInfo"] = typeof(DataBreakpointInfoRequest),
        ["setDataBreakpoints"] = typeof(SetDataBreakpointsRequest),
        ["setInstructionBreakpoints"] = typeof(SetInstructionBreakpointsRequest),
        ["continue"] = typeof(ContinueRequest),
        ["next"] = typeof(NextRequest),
        ["stepIn"] = typeof(StepInRequest),
        ["stepOut"] = typeof(StepOutRequest),
        ["stepBack"] = typeof(StepBackRequest),
        ["reverseContinue"] = typeof(ReverseContinueRequest),
        ["restartFrame"] = typeof(RestartFrameRequest),
        ["goto"] = typeof(GotoRequest),
        ["pause"] = typeof(PauseRequest),
        ["stackTrace"] = typeof(StackTraceRequest),
        ["scopes"] = typeof(ScopesRequest),
        ["variables"] = typeof(VariablesRequest),
        ["setVariable"] = typeof(SetVariableRequest),
        ["source"] = typeof(SourceRequest),
        ["threads"] = typeof(ThreadsRequest),
        ["terminateThreads"] = typeof(TerminateThreadsRequest),
        ["modules"] = typeof(ModulesRequest),
        ["loadedSources"] = typeof(LoadedSourcesRequest),
        ["evaluate"] = typeof(EvaluateRequest),
        ["setExpression"] = typeof(SetExpressionRequest),
        ["stepInTargets"] = typeof(StepInTargetsRequest),
        ["gotoTargets"] = typeof(GotoTargetsRequest),
        ["completions"] = typeof(CompletionsRequest),
        ["exceptionInfo"] = typeof(ExceptionInfoRequest),
        ["readMemory"] = typeof(ReadMemoryRequest),
        ["writeMemory"] = typeof(WriteMemoryRequest),
        ["disassemble"] = typeof(DisassembleRequest),
        ["locations"] = typeof(LocationsRequest),
        ["runInTerminal"] = typeof(RunInTerminalRequest),
        ["startDebugging"] = typeof(StartDebuggingRequest),
    };

    private static readonly Dictionary<string, Type> EventTypes = new()
    {
        ["initialized"] = typeof(InitializedEvent),
        ["stopped"] = typeof(StoppedEvent),
        ["continued"] = typeof(ContinuedEvent),
        ["exited"] = typeof(ExitedEvent),
        ["terminated"] = typeof(TerminatedEvent),
        ["thread"] = typeof(ThreadEvent),
        ["output"] = typeof(OutputEvent),
        ["breakpoint"] = typeof(BreakpointEvent),
        ["module"] = typeof(ModuleEvent),
        ["loadedSource"] = typeof(LoadedSourceEvent),
        ["process"] = typeof(ProcessEvent),
        ["capabilities"] = typeof(CapabilitiesEvent),
        ["progressStart"] = typeof(ProgressStartEvent),
        ["progressUpdate"] = typeof(ProgressUpdateEvent),
        ["progressEnd"] = typeof(ProgressEndEvent),
        ["invalidated"] = typeof(InvalidatedEvent),
        ["memory"] = typeof(MemoryEvent),
    };

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ProtocolMessage);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var type = jo["type"]?.Value<string>();

        Type targetType = type switch
        {
            "request" => jo["command"]?.Value<string>() is string cmd && RequestTypes.TryGetValue(cmd, out var t) ? t : typeof(Request),
            "response" => typeof(Response),
            "event" => jo["event"]?.Value<string>() is string evt && EventTypes.TryGetValue(evt, out var t) ? t : typeof(Event),
            _ => typeof(ProtocolMessage)
        };

        return jo.ToObject(targetType, serializer);
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
#else
public class DapMessageConverter : JsonConverter<ProtocolMessage>
{
    private static readonly Dictionary<string, Type> RequestTypes = new()
    {
        ["cancel"] = typeof(CancelRequest),
        ["initialize"] = typeof(InitializeRequest),
        ["configurationDone"] = typeof(ConfigurationDoneRequest),
        ["launch"] = typeof(LaunchRequest),
        ["attach"] = typeof(AttachRequest),
        ["restart"] = typeof(RestartRequest),
        ["disconnect"] = typeof(DisconnectRequest),
        ["terminate"] = typeof(TerminateRequest),
        ["breakpointLocations"] = typeof(BreakpointLocationsRequest),
        ["setBreakpoints"] = typeof(SetBreakpointsRequest),
        ["setFunctionBreakpoints"] = typeof(SetFunctionBreakpointsRequest),
        ["setExceptionBreakpoints"] = typeof(SetExceptionBreakpointsRequest),
        ["dataBreakpointInfo"] = typeof(DataBreakpointInfoRequest),
        ["setDataBreakpoints"] = typeof(SetDataBreakpointsRequest),
        ["setInstructionBreakpoints"] = typeof(SetInstructionBreakpointsRequest),
        ["continue"] = typeof(ContinueRequest),
        ["next"] = typeof(NextRequest),
        ["stepIn"] = typeof(StepInRequest),
        ["stepOut"] = typeof(StepOutRequest),
        ["stepBack"] = typeof(StepBackRequest),
        ["reverseContinue"] = typeof(ReverseContinueRequest),
        ["restartFrame"] = typeof(RestartFrameRequest),
        ["goto"] = typeof(GotoRequest),
        ["pause"] = typeof(PauseRequest),
        ["stackTrace"] = typeof(StackTraceRequest),
        ["scopes"] = typeof(ScopesRequest),
        ["variables"] = typeof(VariablesRequest),
        ["setVariable"] = typeof(SetVariableRequest),
        ["source"] = typeof(SourceRequest),
        ["threads"] = typeof(ThreadsRequest),
        ["terminateThreads"] = typeof(TerminateThreadsRequest),
        ["modules"] = typeof(ModulesRequest),
        ["loadedSources"] = typeof(LoadedSourcesRequest),
        ["evaluate"] = typeof(EvaluateRequest),
        ["setExpression"] = typeof(SetExpressionRequest),
        ["stepInTargets"] = typeof(StepInTargetsRequest),
        ["gotoTargets"] = typeof(GotoTargetsRequest),
        ["completions"] = typeof(CompletionsRequest),
        ["exceptionInfo"] = typeof(ExceptionInfoRequest),
        ["readMemory"] = typeof(ReadMemoryRequest),
        ["writeMemory"] = typeof(WriteMemoryRequest),
        ["disassemble"] = typeof(DisassembleRequest),
        ["locations"] = typeof(LocationsRequest),
        ["runInTerminal"] = typeof(RunInTerminalRequest),
        ["startDebugging"] = typeof(StartDebuggingRequest),
    };

    private static readonly Dictionary<string, Type> EventTypes = new()
    {
        ["initialized"] = typeof(InitializedEvent),
        ["stopped"] = typeof(StoppedEvent),
        ["continued"] = typeof(ContinuedEvent),
        ["exited"] = typeof(ExitedEvent),
        ["terminated"] = typeof(TerminatedEvent),
        ["thread"] = typeof(ThreadEvent),
        ["output"] = typeof(OutputEvent),
        ["breakpoint"] = typeof(BreakpointEvent),
        ["module"] = typeof(ModuleEvent),
        ["loadedSource"] = typeof(LoadedSourceEvent),
        ["process"] = typeof(ProcessEvent),
        ["capabilities"] = typeof(CapabilitiesEvent),
        ["progressStart"] = typeof(ProgressStartEvent),
        ["progressUpdate"] = typeof(ProgressUpdateEvent),
        ["progressEnd"] = typeof(ProgressEndEvent),
        ["invalidated"] = typeof(InvalidatedEvent),
        ["memory"] = typeof(MemoryEvent),
    };

    public override ProtocolMessage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("type", out var typeProp))
            return null;

        var type = typeProp.GetString();

        Type targetType = type switch
        {
            "request" => root.TryGetProperty("command", out var cmdProp) && RequestTypes.TryGetValue(cmdProp.GetString() ?? "", out var t) ? t : typeof(Request),
            "response" => typeof(Response), // Responses are usually handled by knowing the request, but we can have a generic one
            "event" => root.TryGetProperty("event", out var eventProp) && EventTypes.TryGetValue(eventProp.GetString() ?? "", out var t) ? t : typeof(Event),
            _ => typeof(ProtocolMessage)
        };

        return (ProtocolMessage?)JsonSerializer.Deserialize(root.GetRawText(), targetType, options);
    }

    public override void Write(Utf8JsonWriter writer, ProtocolMessage value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}
#endif
