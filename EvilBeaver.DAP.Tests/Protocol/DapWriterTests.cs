// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text;
using EvilBeaver.DAP.Dto.Events;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Server.Protocol;
using Xunit;

namespace EvilBeaver.DAP.Tests.Protocol;

public class DapWriterTests
{
    [Fact]
    public async Task WriteMessageAsync_InitializedEvent_WritesCorrectFormat()
    {
        // Arrange
        using var stream = new MemoryStream();
        var writer = new DapWriter(stream);
        var @event = new InitializedEvent { Seq = 1 };

        // Act
        await writer.WriteMessageAsync(@event);

        // Assert
        var output = Encoding.UTF8.GetString(stream.ToArray());
        Assert.StartsWith("Content-Length: ", output);
        Assert.Contains("\r\n\r\n", output);
        Assert.Contains("\"type\":\"event\"", output);
        Assert.Contains("\"event\":\"initialized\"", output);
        Assert.Contains("\"seq\":1", output);
    }

    [Fact]
    public async Task WriteMessageAsync_OutputEventWithBody_WritesCorrectFormat()
    {
        // Arrange
        using var stream = new MemoryStream();
        var writer = new DapWriter(stream);
        var @event = new OutputEvent
        {
            Seq = 10,
            Body = new OutputEventBody
            {
                Category = OutputCategory.Stdout,
                Output = "Test output"
            }
        };

        // Act
        await writer.WriteMessageAsync(@event);

        // Assert
        var output = Encoding.UTF8.GetString(stream.ToArray());
        Assert.Contains("Content-Length: ", output);
        Assert.Contains("\"category\":\"stdout\"", output);
        Assert.Contains("\"output\":\"Test output\"", output);
    }

    [Fact]
    public async Task WriteMessageAsync_MultipleMessages_WritesSequentially()
    {
        // Arrange
        using var stream = new MemoryStream();
        var writer = new DapWriter(stream);
        var msg1 = new InitializedEvent { Seq = 1 };
        var msg2 = new StoppedEvent { Seq = 2, Body = new StoppedEventBody { Reason = "pause" } };

        // Act
        await writer.WriteMessageAsync(msg1);
        await writer.WriteMessageAsync(msg2);

        // Assert
        var output = Encoding.UTF8.GetString(stream.ToArray());
        var parts = output.Split(new[] { "Content-Length: " }, StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(2, parts.Length);
        Assert.Contains("\"event\":\"initialized\"", parts[0]);
        Assert.Contains("\"event\":\"stopped\"", parts[1]);
    }
}
