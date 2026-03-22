# EvilBeaver.DAP

For English [click here](#english)

Библиотека для создания debug-адаптеров на C# по протоколу [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification).

DAP — стандарт Microsoft, позволяющий IDE (VS Code, Rider, Neovim и др.) работать с отладчиком любого языка или рантайма через единый интерфейс. Библиотека берёт на себя всю работу с протоколом, оставляя разработчику только логику отладки.

## Состав

| Пакет | Назначение |
|---|---|
| `EvilBeaver.DAP.Dto` | Типизированные классы для всех сообщений и типов протокола |
| `EvilBeaver.DAP.Server` | RPC-сервер: транспорт, диспетчеризация, отправка событий *(в разработке)* |

## Как это работает

Вы реализуете интерфейс `IDebugAdapter` — по одному методу на каждый DAP-запрос. Сервер принимает сообщения от IDE, десериализует их и вызывает нужный метод вашего адаптера. Через переданный в метод объект `IClientChannel` вы можете в любой момент отправить событие обратно клиенту — например, уведомить об остановке на точке останова.

Сервер поддерживает два транспорта:

- **stdio** — для запуска адаптера как дочернего процесса IDE (наиболее распространённый сценарий)
- **TCP** — для подключения к уже запущенному адаптеру

## Быстрый старт

```csharp
var adapter = new MyDebugAdapter();
var server = new DapServer(new StdioTransport(), adapter);
await server.RunAsync(CancellationToken.None);
```

Реализация адаптера сводится к наполнению методов `IDebugAdapter` логикой вашего рантайма. Все типы запросов, ответов и событий уже описаны в `EvilBeaver.DAP.Dto` и точно соответствуют спецификации протокола.

## Требования

- .NET 8

## Тесты

Проект `EvilBeaver.DAP.Tests` собирается под `net8.0` и `net481`: второй вариант запускается на **.NET Framework 4.8.1** и подключает **netstandard2.0**-сборки `Dto` и `Server`, чтобы прогонять код за `#if NETSTANDARD2_0`.

```bash
dotnet test                    # оба таргета
dotnet test -f net8.0         # только .NET 8
dotnet test -f net481         # только ветка netstandard2.0 (Windows + .NET Framework 4.8.1)
```

Для сборки `net481` нужен [developer pack / targeting pack .NET Framework 4.8.1](https://dotnet.microsoft.com/download/dotnet-framework/net481) (часто уже стоит вместе с Visual Studio). На Linux/macOS этот таргет обычно не собирается — там достаточно `dotnet test -f net8.0` или полный прогон в CI на Windows.

---

<a name="english"></a>
# EvilBeaver.DAP

A library for creating C# debug adapters using the [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification).

DAP is a Microsoft standard that allows IDEs (VS Code, Rider, Neovim, etc.) to work with a debugger of any language or runtime through a single interface. The library handles all the protocol work, leaving only the debugging logic to the developer.

## Components

| Package | Purpose |
|---|---|
| `EvilBeaver.DAP.Dto` | Typed classes for all protocol messages and types |
| `EvilBeaver.DAP.Server` | RPC server: transport, dispatching, event sending *(in development)* |

## How it works

You implement the `IDebugAdapter` interface — one method for each DAP request. The server receives messages from the IDE, deserializes them, and calls the appropriate method of your adapter. Through the `IClientChannel` object passed to the method, you can send events back to the client at any time — for example, to notify about a breakpoint hit.

The server supports two transports:

- **stdio** — for running the adapter as a child process of the IDE (the most common scenario)
- **TCP** — for connecting to an already running adapter

## Quick Start

```csharp
var adapter = new MyDebugAdapter();
var server = new DapServer(new StdioTransport(), adapter);
await server.RunAsync(CancellationToken.None);
```

Implementing the adapter boils down to filling the `IDebugAdapter` methods with your runtime logic. All request, response, and event types are already described in `EvilBeaver.DAP.Dto` and strictly follow the protocol specification.

## Requirements

- .NET 8

## Tests

`EvilBeaver.DAP.Tests` multi-targets `net8.0` and `net481`. The `net481` run executes on **.NET Framework 4.8.1** and references the **netstandard2.0** builds of `Dto` and `Server`, exercising `#if NETSTANDARD2_0` code paths.

```bash
dotnet test                    # both targets
dotnet test -f net8.0          # .NET 8 only
dotnet test -f net481          # netstandard2.0 path (Windows + .NET Framework 4.8.1)
```

Building `net481` requires the [.NET Framework 4.8.1 developer / targeting pack](https://dotnet.microsoft.com/download/dotnet-framework/net481) (often installed with Visual Studio). Linux/macOS builds typically skip or omit this TFM; use `dotnet test -f net8.0` locally or a Windows CI job for the full matrix.
