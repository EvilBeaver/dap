using System.Text;
using EvilBeaver.DAP.Dto.Events;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Server.Protocol;
using Xunit;

namespace EvilBeaver.DAP.Tests.Protocol;

public class DapReaderTests
{
    [Fact]
    public async Task ReadMessageAsync_ValidInitializeRequest_ReturnsRequest()
    {
        // Arrange
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"initialize\",\"arguments\":{\"adapterID\":\"test\"}}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json)}\r\n\r\n{json}";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var reader = new DapReader(stream);

        // Act
        var message = await reader.ReadMessageAsync();

        // Assert
        Assert.NotNull(message);
        var request = Assert.IsType<InitializeRequest>(message);
        Assert.Equal(1, request.Seq);
        Assert.Equal("initialize", request.Command);
        Assert.Equal("test", request.Arguments?.AdapterId);
    }

    [Fact]
    public async Task ReadMessageAsync_ValidStoppedEvent_ReturnsEvent()
    {
        // Arrange
        var json = "{\"seq\":2,\"type\":\"event\",\"event\":\"stopped\",\"body\":{\"reason\":\"breakpoint\"}}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json)}\r\n\r\n{json}";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var reader = new DapReader(stream);

        // Act
        var message = await reader.ReadMessageAsync();

        // Assert
        Assert.NotNull(message);
        var @event = Assert.IsType<StoppedEvent>(message);
        Assert.Equal(2, @event.Seq);
        Assert.Equal("stopped", @event.EventType);
        Assert.Equal("breakpoint", @event.Body?.Reason);
    }

    [Fact]
    public async Task ReadMessageAsync_MultipleMessages_ReturnsAllMessages()
    {
        // Arrange
        var json1 = "{\"seq\":1,\"type\":\"event\",\"event\":\"initialized\"}";
        var json2 = "{\"seq\":2,\"type\":\"event\",\"event\":\"stopped\",\"body\":{\"reason\":\"pause\"}}";
        var content = $"Content-Length: {Encoding.UTF8.GetByteCount(json1)}\r\n\r\n{json1}" +
                      $"Content-Length: {Encoding.UTF8.GetByteCount(json2)}\r\n\r\n{json2}";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var reader = new DapReader(stream);

        // Act
        var msg1 = await reader.ReadMessageAsync();
        var msg2 = await reader.ReadMessageAsync();
        var msg3 = await reader.ReadMessageAsync();

        // Assert
        Assert.NotNull(msg1);
        Assert.IsType<InitializedEvent>(msg1);
        Assert.NotNull(msg2);
        Assert.IsType<StoppedEvent>(msg2);
        Assert.Null(msg3);
    }

    [Fact]
    public async Task ReadMessageAsync_EmptyStream_ReturnsNull()
    {
        // Arrange
        using var stream = new MemoryStream();
        var reader = new DapReader(stream);

        // Act
        var message = await reader.ReadMessageAsync();

        // Assert
        Assert.Null(message);
    }

    [Fact]
    public async Task ReadMessageAsync_InvalidHeader_ThrowsInvalidDataException()
    {
        // Arrange
        var content = "Invalid-Header: 123\r\n\r\n{}";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var reader = new DapReader(stream);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidDataException>(() => reader.ReadMessageAsync());
    }
}
