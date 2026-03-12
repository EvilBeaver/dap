// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Server;
using EvilBeaver.DAP.Server.Protocol;
using Moq;
using Xunit;

namespace EvilBeaver.DAP.Tests.Protocol;

public class MessageLoopTests
{
    [Fact]
    public async Task RunAsync_UnsupportedCommand_ReturnsErrorResponse()
    {
        // Arrange
        var request = new Request { Seq = 1, Command = "unknownCommand" };
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"unknownCommand\"}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json)}\r\n\r\n{json}";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        using var outputStream = new MemoryStream();
        
        var reader = new DapReader(inputStream);
        var writer = new DapWriter(outputStream);
        var adapterMock = new Mock<IDebugAdapter>();
        var nextSeq = 1;
        
        adapterMock.Setup(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var loop = new MessageLoop(reader, writer, adapterMock.Object, () => nextSeq++);
        var cts = new CancellationTokenSource();

        // Act
        // Run once and cancel
        var runTask = loop.RunAsync(cts.Token);
        await Task.Delay(100); // Give it some time to process
        cts.Cancel();
        await runTask;

        // Assert
        outputStream.Position = 0;
        var responseReader = new DapReader(outputStream);
        var message = await responseReader.ReadMessageAsync();
        
        var response = Assert.IsType<ErrorResponse>(message);
        Assert.False(response.Success);
        Assert.Equal("notSupported", response.Message);
        Assert.Equal(1, response.RequestSeq);
        Assert.NotNull(response.Body?.Error);
        Assert.Equal(1001, response.Body.Error.Id);
        Assert.Contains("Unsupported command", response.Body.Error.Format);
    }

    [Fact]
    public async Task RunAsync_UnhandledException_ReturnsInternalError()
    {
        // Arrange
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"initialize\",\"arguments\":{\"adapterID\":\"test\"}}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json)}\r\n\r\n{json}";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        using var outputStream = new MemoryStream();
        
        var reader = new DapReader(inputStream);
        var writer = new DapWriter(outputStream);
        var adapterMock = new Mock<IDebugAdapter>();
        adapterMock.Setup(a => a.InitializeAsync(It.IsAny<InitializeRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));
        adapterMock.Setup(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var nextSeq = 1;
        var loop = new MessageLoop(reader, writer, adapterMock.Object, () => nextSeq++);
        var cts = new CancellationTokenSource();

        // Act
        var runTask = loop.RunAsync(cts.Token);
        await Task.Delay(100);
        cts.Cancel();
        await runTask;

        // Assert
        outputStream.Position = 0;
        var responseReader = new DapReader(outputStream);
        var message = await responseReader.ReadMessageAsync();
        
        var response = Assert.IsType<ErrorResponse>(message);
        Assert.False(response.Success);
        Assert.Equal("internalError", response.Message);
        Assert.Equal(1003, response.Body?.Error?.Id);
        Assert.Contains("Test exception", response.Body?.Error?.Format);
    }

    [Fact]
    public async Task RunAsync_DisconnectRequest_CompletesAfterSendingResponse()
    {
        // Arrange
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"disconnect\"}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json)}\r\n\r\n{json}";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        using var outputStream = new MemoryStream();

        var reader = new DapReader(inputStream);
        var writer = new DapWriter(outputStream);
        var adapterMock = new Mock<IDebugAdapter>();
        adapterMock.Setup(a => a.DisconnectAsync(It.IsAny<DisconnectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DisconnectResponse());
        adapterMock.Setup(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var nextSeq = 1;
        var loop = new MessageLoop(reader, writer, adapterMock.Object, () => nextSeq++);

        // Act — цикл должен завершиться сам: после disconnect поток заканчивается (EOF)
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await loop.RunAsync(cts.Token);

        // Assert — ответ на disconnect отправлен
        outputStream.Position = 0;
        var responseReader = new DapReader(outputStream);
        var message = await responseReader.ReadMessageAsync();

        var response = Assert.IsType<Response>(message);
        Assert.True(response.Success);
        Assert.Equal("disconnect", response.Command);
        Assert.Equal(1, response.RequestSeq);
        adapterMock.Verify(a => a.DisconnectAsync(It.IsAny<DisconnectRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RunAsync_ClientClosesStream_CompletesAndNotifiesAdapter()
    {
        // Arrange — пустой поток симулирует немедленный EOF
        using var inputStream = new MemoryStream(Array.Empty<byte>());
        using var outputStream = new MemoryStream();

        var reader = new DapReader(inputStream);
        var writer = new DapWriter(outputStream);
        var adapterMock = new Mock<IDebugAdapter>();
        adapterMock.Setup(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var loop = new MessageLoop(reader, writer, adapterMock.Object, () => 1);

        // Act
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await loop.RunAsync(cts.Token);

        // Assert — цикл завершился без исключения, адаптер уведомлён об обрыве
        adapterMock.Verify(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RunAsync_StreamThrowsEndOfStream_CompletesAndNotifiesAdapter()
    {
        // Arrange — поток с корректным заголовком, но тело обрывается
        var truncated = "Content-Length: 100\r\n\r\n{\"seq\":1";
        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(truncated));
        using var outputStream = new MemoryStream();

        var reader = new DapReader(inputStream);
        var writer = new DapWriter(outputStream);
        var adapterMock = new Mock<IDebugAdapter>();
        adapterMock.Setup(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var loop = new MessageLoop(reader, writer, adapterMock.Object, () => 1);

        // Act
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await loop.RunAsync(cts.Token);

        // Assert — EndOfStreamException обработан штатно, адаптер уведомлён
        adapterMock.Verify(a => a.OnClientDisconnectedAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
