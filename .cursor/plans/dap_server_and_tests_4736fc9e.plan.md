---
name: DAP Server and Tests
overview: Создать проект EvilBeaver.DAP.Server с транспортным слоем (DapReader/DapWriter для Content-Length framing) и тестовый проект, проверяющий корректность сериализации/десериализации DAP-сообщений через потоки.
todos:
  - id: server-csproj
    content: Создать EvilBeaver.DAP.Server.csproj с зависимостью на EvilBeaver.DAP.Dto
    status: completed
  - id: transport
    content: Реализовать ITransport и StreamTransport
    status: completed
  - id: dap-reader
    content: Реализовать DapReader — чтение Content-Length заголовков и десериализация из потока
    status: completed
  - id: dap-writer
    content: Реализовать DapWriter — сериализация и запись с Content-Length заголовками в поток
    status: completed
  - id: client-channel
    content: Реализовать IClientChannel и DapServer — точку входа серверной библиотеки
    status: completed
  - id: test-csproj
    content: Создать EvilBeaver.DAP.Tests.csproj (xUnit) с зависимостями на Server и Dto
    status: completed
  - id: test-reader
    content: Написать тесты DapReaderTests — чтение request/event из потока
    status: completed
  - id: test-writer
    content: Написать тесты DapWriterTests — запись событий в поток с проверкой формата
    status: completed
  - id: test-serialization
    content: Написать тесты DapSerializerTests — round-trip и полиморфная десериализация
    status: completed
  - id: update-sln
    content: Добавить оба проекта в EvilBeaver.DAP.sln
    status: completed
isProject: false
---

# Реализация DAP Server и тестов

## 1. Проект EvilBeaver.DAP.Server

Создать `EvilBeaver.DAP.Server/EvilBeaver.DAP.Server.csproj` — библиотека классов net8.0 с зависимостью на `EvilBeaver.DAP.Dto`.

### Структура проекта

```
EvilBeaver.DAP.Server/
├── EvilBeaver.DAP.Server.csproj
├── Transport/
│   ├── ITransport.cs
│   └── StreamTransport.cs
├── Protocol/
│   ├── DapReader.cs
│   └── DapWriter.cs
└── DapServer.cs
```

### Transport/ITransport.cs

Интерфейс транспорта, абстрагирующий потоки ввода/вывода:

```csharp
public interface ITransport : IAsyncDisposable
{
    Stream Input { get; }
    Stream Output { get; }
}
```

### Transport/StreamTransport.cs

Реализация поверх произвольных `Stream` (для тестов и stdio):

```csharp
public class StreamTransport : ITransport
{
    public StreamTransport(Stream input, Stream output) { ... }
    public Stream Input { get; }
    public Stream Output { get; }
}
```

### Protocol/DapReader.cs

Чтение DAP-сообщений из потока по спецификации:

- Парсинг заголовка `Content-Length: <N>\r\n\r\n`
- Чтение ровно N байт UTF-8 тела
- Десериализация через `DapSerializer.Deserialize(ReadOnlySpan<byte>)`

```csharp
public class DapReader
{
    public DapReader(Stream input) { ... }
    public async Task<ProtocolMessage?> ReadMessageAsync(CancellationToken ct = default) { ... }
}
```

Ключевая логика: читаем побайтово заголовки до `\r\n\r\n`, извлекаем `Content-Length`, затем читаем ровно указанное количество байт и десериализуем.

### Protocol/DapWriter.cs

Запись DAP-сообщений в поток:

- Сериализация через `DapSerializer.Serialize()`
- Формирование заголовка `Content-Length: <N>\r\n\r\n`
- Запись заголовка + тела в поток

```csharp
public class DapWriter
{
    public DapWriter(Stream output) { ... }
    public async Task WriteMessageAsync(ProtocolMessage message, CancellationToken ct = default) { ... }
}
```

### DapServer.cs

Точка входа сервера — связывает транспорт, reader/writer, и предоставляет метод `SendEventAsync`:

```csharp
public class DapServer : IClientChannel
{
    public DapServer(ITransport transport) { ... }
    public DapReader Reader { get; }
    public DapWriter Writer { get; }
    public Task SendEventAsync(Event @event, CancellationToken ct = default) { ... }
}
```

Интерфейс `IClientChannel` (канал для отправки событий клиенту):

```csharp
public interface IClientChannel
{
    Task SendEventAsync(Event @event, CancellationToken ct = default);
}
```

---

## 2. Тестовый проект EvilBeaver.DAP.Tests

Создать `EvilBeaver.DAP.Tests/EvilBeaver.DAP.Tests.csproj` — xUnit тестовый проект net8.0 с зависимостями на `EvilBeaver.DAP.Server` и `EvilBeaver.DAP.Dto`.

### Структура тестов

```
EvilBeaver.DAP.Tests/
├── EvilBeaver.DAP.Tests.csproj
├── Protocol/
│   ├── DapReaderTests.cs
│   └── DapWriterTests.cs
└── Serialization/
    └── DapSerializerTests.cs
```

### Тестовые сценарии

**DapReaderTests** — чтение сообщений из потока:

- Чтение `InitializeRequest` с корректным Content-Length
- Чтение `StoppedEvent` с телом
- Чтение нескольких последовательных сообщений
- Пустой поток возвращает null

**DapWriterTests** — запись сообщений в поток:

- Запись события `InitializedEvent` — проверка формата Content-Length + JSON
- Запись `OutputEvent` с телом — проверка корректности UTF-8

**DapSerializerTests** — сериализация/десериализация DTO:

- Round-trip для `InitializeRequest`
- Round-trip для `StoppedEvent` с телом
- Round-trip для `Response` с success/error
- Полиморфная десериализация: JSON с `type: "request"` -> конкретный тип запроса
- Полиморфная десериализация: JSON с `type: "event"` -> конкретный тип события
- Проверка camelCase именования полей в сериализованном JSON

---

## 3. Обновление Solution

Добавить оба новых проекта в [EvilBeaver.DAP.sln](EvilBeaver.DAP.sln).