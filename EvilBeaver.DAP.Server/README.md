# EvilBeaver.DAP.Server

A lightweight and extensible RPC server for building Debug Adapters in C# using the [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification).

The server handles the transport, dispatching, and event sending, allowing you to focus on the debugging logic for your specific runtime.

## Features

- **Multiple Transports**: Supports `stdio` (standard input/output) and `TCP` connections.
- **Easy Integration**: Implement the `IDebugAdapter` interface to handle DAP requests.
- **Event Support**: Send events back to the client via `IClientChannel`.

## Quick Start

```csharp
var adapter = new MyDebugAdapter();
var server = new DapServer(new StdioTransport(), adapter);
await server.RunAsync(CancellationToken.None);
```

## Requirements

- .NET 8
