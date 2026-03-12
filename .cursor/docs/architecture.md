# Архитектура EvilBeaver.DAP

## Обзор проекта

Проект реализует [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification) для .NET/C#. Целевая аудитория — разработчики, создающие debug-адаптеры для языков и сред выполнения, интегрируемых с IDE (VS Code, Rider, Neovim и др.).

Решение состоит из двух библиотек:

| Проект | Статус | Назначение |
|---|---|---|
| `EvilBeaver.DAP.Dto` | реализован | DTO-классы всех сущностей протокола + сериализация |
| `EvilBeaver.DAP.Server` | реализован | RPC-сервер: чтение/запись DAP-сообщений через поток |

---

## EvilBeaver.DAP.Dto

### Назначение

Типобезопасное представление всех сообщений и типов протокола DAP. Не содержит никакой логики транспорта или диспетчеризации.

### Структура пространств имён

```
EvilBeaver.DAP.Dto
├── Base/               — базовые типы протокола
│   ├── ProtocolMessage — абстрактный корень иерархии (seq, type)
│   ├── Request         — запрос клиента/адаптера (+ типизированный Request<TArgs>)
│   ├── Response        — ответ на запрос (+ типизированный Response<TBody>)
│   ├── Event           — событие от адаптера (+ типизированный Event<TBody>)
│   └── ErrorResponse   — ответ с ошибкой (body.error: Message)
│
├── Requests/           — все запросы клиента → адаптер (Initialize, Launch, ...)
├── Events/             — все события адаптер → клиент (Stopped, Output, ...)
├── ReverseRequests/    — reverse-запросы адаптер → клиент (RunInTerminal, StartDebugging)
├── Types/              — вспомогательные типы (Capabilities, StackFrame, Variable, ...)
└── Serialization/
    ├── DapSerializer       — точка входа: Serialize / Deserialize
    └── DapMessageConverter — JsonConverter<ProtocolMessage> с полиморфной десериализацией
```

### Иерархия классов

```
ProtocolMessage (abstract)
├── Request
│   └── Request<TArgs>      : Request
│       ├── InitializeRequest
│       ├── LaunchRequest
│       ├── SetBreakpointsRequest
│       └── ... (37 конкретных запросов)
├── Response
│   └── Response<TBody>     : Response
│       └── ErrorResponse
└── Event
    └── Event<TBody>        : Event
        ├── InitializedEvent
        ├── StoppedEvent
        ├── OutputEvent
        └── ... (17 конкретных событий)
```

Reverse-запросы (`RunInTerminalRequest`, `StartDebuggingRequest`) — это `Request`-классы, которые физически инициируются адаптером, а не клиентом; с точки зрения сериализации они ничем не отличаются от обычных запросов.

### Сериализация

`DapSerializer` — статический фасад над JSON-библиотекой:

- На `net8.0` используется `System.Text.Json`
- На `netstandard2.0` используется `Newtonsoft.Json`
- **camelCase** для имён свойств
- null-поля не сериализуются
- Полиморфная десериализация через `DapMessageConverter`:
  - читает поле `type` → выбирает `Request` / `Response` / `Event`
  - для `request` читает поле `command` → ищет конкретный тип в словаре `RequestTypes`
  - для `event` читает поле `event` → ищет конкретный тип в словаре `EventTypes`
  - если тип не распознан — возвращает базовый класс (`Request`, `Response`, `Event`)

### Формат сообщения DAP (транспортный уровень)

Каждое сообщение передаётся в виде пары:

```
Content-Length: <N>\r\n
\r\n
<N байт UTF-8 JSON>
```

Разбор этого заголовка выходит за пределы `EvilBeaver.DAP.Dto` и является ответственностью транспортного слоя (`EvilBeaver.DAP.Server`).

---

## EvilBeaver.DAP.Server

### Назначение

Готовый к использованию RPC-сервер, принимающий и отправляющий DAP-сообщения через произвольный поток (`Stream`). Реализует:

- чтение/запись заголовков `Content-Length`
- диспетчеризацию входящих запросов к методам адаптера
- отправку событий из кода адаптера в любой момент

Библиотека не содержит готовых транспортов для конкретных источников (stdio, TCP и т.д.). Хост-приложение самостоятельно открывает нужные потоки и передаёт их в `StreamTransport`.

### Структура

```
EvilBeaver.DAP.Server
├── Transport/
│   ├── ITransport              — интерфейс: свойства Input / Output (Stream)
│   └── StreamTransport         — реализация поверх пары произвольных Stream
│
├── Protocol/
│   ├── DapReader               — чтение заголовков Content-Length + тела
│   ├── DapWriter               — запись заголовков + тела в Stream
│   └── MessageLoop             — основной цикл чтения и диспетчеризации
│
├── IClientChannel              — канал для отправки событий клиенту
├── IDebugAdapter               — интерфейс пользовательского адаптера
│
└── DapServer                   — точка входа: транспорт + адаптер + цикл
    — RunAsync(CancellationToken)
    — SendEventAsync(Event)
```

### Жизненный цикл сервера

```
DapServer.RunAsync(CancellationToken)
    │
    ├── IDebugAdapter.OnServerStartAsync(IClientChannel, ct)   ← адаптер получает канал событий
    │
    └── MessageLoop
          ┌──────────────────────────────────────────────────┐
          │  DapReader.ReadMessageAsync()                    │  ← Content-Length + JSON
          │  DapMessageConverter.Read()                      │  ← полиморфная десериализация
          │  IDebugAdapter.<соответствующий метод>(req, ct)  │  ← пользовательский код
          │  DapWriter.WriteMessageAsync(response)           │  ← отправляет Response обратно
          └──────────────────────────────────────────────────┘
```

### Транспорт

Библиотека предоставляет `StreamTransport`, принимающий два отдельных потока: входной и выходной. Хост-приложение отвечает за создание потоков и управление их жизненным циклом.

Типичные сценарии использования:

| Сценарий | Как передать потоки |
|---|---|
| VS Code запускает адаптер как дочерний процесс (stdio) | `Console.OpenStandardInput()` / `Console.OpenStandardOutput()` |
| TCP-соединение | `TcpClient.GetStream()` в качестве обоих потоков |
| Тестирование | `MemoryStream` или `Pipe` |

`StreamTransport` не закрывает переданные потоки — это обязанность хоста.

### API адаптера

#### IClientChannel

Минимальный интерфейс для отправки событий в IDE. Передаётся адаптеру **один раз** при запуске сервера через `IDebugAdapter.OnServerStartAsync`. Адаптер хранит ссылку и использует её в любой момент — в том числе из фоновых потоков при срабатывании точек останова.

```csharp
public interface IClientChannel
{
    Task SendEventAsync(Event @event, CancellationToken ct = default);
}
```

#### IDebugAdapter

Пользовательский код реализует этот интерфейс. Каждый метод соответствует одному DAP-запросу. `IClientChannel` не передаётся в каждый метод — он предоставляется один раз через `OnServerStartAsync` до начала цикла обработки сообщений.

Интерфейс содержит методы для всех запросов DAP-протокола: `InitializeAsync`, `LaunchAsync`, `AttachAsync`, `SetBreakpointsAsync`, `ContinueAsync`, `DisconnectAsync` и т.д. (всего ~40 методов).

```csharp
public interface IDebugAdapter
{
    /// <summary>
    /// Вызывается перед запуском цикла сообщений.
    /// Адаптер должен сохранить <paramref name="channel"/> для последующей отправки событий.
    /// </summary>
    Task OnServerStartAsync(IClientChannel channel, CancellationToken ct);

    Task<InitializeResponse> InitializeAsync(InitializeRequest request, CancellationToken ct);
    Task<LaunchResponse> LaunchAsync(LaunchRequest request, CancellationToken ct);
    Task<DisconnectResponse> DisconnectAsync(DisconnectRequest request, CancellationToken ct);
    // ... остальные запросы протокола
}
```

#### Пример реализации адаптера

```csharp
public class MyDebugAdapter : IDebugAdapter
{
    private IClientChannel _channel = null!;

    public Task OnServerStartAsync(IClientChannel channel, CancellationToken ct)
    {
        _channel = channel;
        return Task.CompletedTask;
    }

    public async Task<LaunchResponse> LaunchAsync(LaunchRequest request, CancellationToken ct)
    {
        // запустить отлаживаемый процесс...

        await _channel.SendEventAsync(new InitializedEvent(), ct);

        return new LaunchResponse { Success = true };
    }

    public async Task<ContinueResponse> ContinueAsync(ContinueRequest request, CancellationToken ct)
    {
        // возобновить выполнение...

        // позднее, из фонового потока, при срабатывании точки останова:
        await _channel.SendEventAsync(new StoppedEvent
        {
            Body = new StoppedEvent.EventBody
            {
                Reason = "breakpoint",
                ThreadId = 1,
                AllThreadsStopped = true
            }
        }, ct);

        return new ContinueResponse { Success = true };
    }
}
```

#### Запуск сервера

Хост-приложение открывает потоки и передаёт их в `StreamTransport`. Хост отвечает за закрытие потоков после завершения `RunAsync`.

```csharp
// Пример: stdio-адаптер, запускаемый IDE как дочерний процесс
var input = Console.OpenStandardInput();
var output = Console.OpenStandardOutput();

var adapter = new MyDebugAdapter();
var server = new DapServer(new StreamTransport(input, output), adapter);
await server.RunAsync(CancellationToken.None);

// Потоки закрываются хостом здесь, после завершения сервера
```

### Зависимости между проектами

```
EvilBeaver.DAP.Server
    └── зависит от → EvilBeaver.DAP.Dto
```

`EvilBeaver.DAP.Dto` не имеет внешних зависимостей на `net8.0`. На `netstandard2.0` зависит от `Newtonsoft.Json`.

---

## Решение (solution)

```
EvilBeaver.DAP.sln
├── EvilBeaver.DAP.Dto/         — net8.0; netstandard2.0
└── EvilBeaver.DAP.Server/      — net8.0; netstandard2.0
```

Целевые платформы: **.NET 8** и **.NET Standard 2.0**. Nullable annotations включены.
