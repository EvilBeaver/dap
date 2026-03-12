# EvilBeaver.DAP.Server

A lightweight and extensible RPC server for building Debug Adapters in C# using the [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification).

The server handles the transport framing, request dispatching, and event sending, allowing you to focus on the debugging logic for your specific runtime.

## Features

- **Stream-based transport**: works with any pair of `Stream` objects — stdin/stdout, TCP, named pipes, in-memory pipes for testing.
- **Full DAP dispatch**: all ~40 DAP requests are dispatched automatically to your `IDebugAdapter` implementation.
- **Event support**: send events back to the client at any time via `IClientChannel`, including from background threads.

## Quick Start

Implement `IDebugAdapter`, provide streams, and run:

```csharp
var adapter = new MyDebugAdapter();

// The host is responsible for opening and closing the streams.
// Example: stdio adapter launched by the IDE as a child process.
var input = Console.OpenStandardInput();
var output = Console.OpenStandardOutput();

var server = new DapServer(new StreamTransport(input, output), adapter);
await server.RunAsync(CancellationToken.None);
```

`IDebugAdapter` exposes one lifecycle method and one method per DAP command:

```csharp
public class MyDebugAdapter : IDebugAdapter
{
    private IClientChannel _channel = null!;

    // Called once before the message loop starts.
    public Task OnServerStartAsync(IClientChannel channel, CancellationToken ct)
    {
        _channel = channel;
        return Task.CompletedTask;
    }

    public async Task<LaunchResponse> LaunchAsync(LaunchRequest request, CancellationToken ct)
    {
        // start the debuggee process...
        await _channel.SendEventAsync(new InitializedEvent(), ct);
        return new LaunchResponse { Success = true };
    }

    // ... implement the remaining IDebugAdapter methods
}
```

## Requirements

- .NET 8 or .NET Standard 2.0
