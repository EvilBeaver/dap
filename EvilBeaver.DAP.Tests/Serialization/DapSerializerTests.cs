// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Events;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Dto.Serialization;
using System.Text.Json;
using Xunit;

namespace EvilBeaver.DAP.Tests.Serialization;

public class DapSerializerTests
{
    [Fact]
    public void Serialize_InitializeRequest_ReturnsCorrectJson()
    {
        // Arrange
        var request = new InitializeRequest
        {
            Seq = 1,
            Arguments = new InitializeRequestArguments
            {
                AdapterId = "test-adapter",
                LinesStartAt1 = true,
                ColumnsStartAt1 = true,
                PathFormat = "path"
            }
        };

        // Act
        var json = DapSerializer.Serialize(request);

        // Assert
        Assert.Contains("\"type\":\"request\"", json);
        Assert.Contains("\"command\":\"initialize\"", json);
        Assert.Contains("\"adapterID\":\"test-adapter\"", json);
        Assert.Contains("\"linesStartAt1\":true", json);
    }

    [Fact]
    public void Deserialize_InitializeRequest_ReturnsTypedRequest()
    {
        // Arrange
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"initialize\",\"arguments\":{\"adapterID\":\"test-adapter\"}}";

        // Act
        var message = DapSerializer.Deserialize(json);

        // Assert
        Assert.NotNull(message);
        var request = Assert.IsType<InitializeRequest>(message);
        Assert.Equal(1, request.Seq);
        Assert.Equal("initialize", request.Command);
        Assert.NotNull(request.Arguments);
        Assert.Equal("test-adapter", request.Arguments.AdapterId);
    }

    [Fact]
    public void Deserialize_StoppedEvent_ReturnsTypedEvent()
    {
        // Arrange
        var json = "{\"seq\":2,\"type\":\"event\",\"event\":\"stopped\",\"body\":{\"reason\":\"breakpoint\",\"threadId\":1}}";

        // Act
        var message = DapSerializer.Deserialize(json);

        // Assert
        Assert.NotNull(message);
        var @event = Assert.IsType<StoppedEvent>(message);
        Assert.Equal(2, @event.Seq);
        Assert.Equal("stopped", @event.EventType);
        Assert.NotNull(@event.Body);
        Assert.Equal("breakpoint", @event.Body.Reason);
        Assert.Equal(1, @event.Body.ThreadId);
    }

    [Fact]
    public void RoundTrip_OutputEvent_PreservesData()
    {
        // Arrange
        var original = new OutputEvent
        {
            Seq = 42,
            Body = new OutputEventBody
            {
                Category = OutputCategory.Stdout,
                Output = "Hello, World!\n"
            }
        };

        // Act
        var json = DapSerializer.Serialize(original);
        var deserialized = DapSerializer.Deserialize(json) as OutputEvent;

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Seq, deserialized.Seq);
        Assert.Equal(original.EventType, deserialized.EventType);
        Assert.NotNull(deserialized.Body);
        Assert.Equal(original.Body.Category, deserialized.Body.Category);
        Assert.Equal(original.Body.Output, deserialized.Body.Output);
    }

    [Fact]
    public void Deserialize_UnknownRequest_ReturnsBaseRequest()
    {
        // Arrange
        var json = "{\"seq\":3,\"type\":\"request\",\"command\":\"unknownCommand\",\"arguments\":{\"foo\":\"bar\"}}";

        // Act
        var message = DapSerializer.Deserialize(json);

        // Assert
        Assert.NotNull(message);
        var request = Assert.IsType<Request>(message);
        Assert.Equal("unknownCommand", request.Command);
    }

    [Fact]
    public void Deserialize_LaunchRequest_PopulatesExtensionDataForAdapterSpecificProperties()
    {
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"launch\",\"arguments\":{\"noDebug\":true,\"program\":\"/app/bin\",\"customFlag\":42}}";

        var message = DapSerializer.Deserialize(json);

        Assert.NotNull(message);
        var launch = Assert.IsType<LaunchRequest>(message);
        Assert.NotNull(launch.Arguments);
        Assert.True(launch.Arguments.NoDebug);
        Assert.NotNull(launch.Arguments.AdditionalData);
        Assert.Equal(2, launch.Arguments.AdditionalData.Count);
        Assert.True(launch.Arguments.AdditionalData.ContainsKey("program"));
        Assert.True(launch.Arguments.AdditionalData.ContainsKey("customFlag"));
        Assert.Equal("/app/bin", launch.Arguments.AdditionalData["program"].GetString());
        Assert.Equal(42, launch.Arguments.AdditionalData["customFlag"].GetInt32());
    }

    [Fact]
    public void Deserialize_AttachRequest_PopulatesExtensionDataForAdapterSpecificProperties()
    {
        var json = "{\"seq\":2,\"type\":\"request\",\"command\":\"attach\",\"arguments\":{\"processId\":7,\"pipeName\":\"dbg\"}}";

        var message = DapSerializer.Deserialize(json);

        Assert.NotNull(message);
        var attach = Assert.IsType<AttachRequest>(message);
        Assert.NotNull(attach.Arguments.AdditionalData);
        Assert.Equal(2, attach.Arguments.AdditionalData.Count);
        Assert.Equal(7, attach.Arguments.AdditionalData["processId"].GetInt32());
        Assert.Equal("dbg", attach.Arguments.AdditionalData["pipeName"].GetString());
    }

    private sealed class SampleLaunchExtras
    {
        public string? Program { get; set; }
        public int CustomFlag { get; set; }
    }

    [Fact]
    public void DeserializeAdditionalProperties_MapsExtensionDataToAdapterDto()
    {
        var json = "{\"seq\":1,\"type\":\"request\",\"command\":\"launch\",\"arguments\":{\"noDebug\":false,\"program\":\"x.exe\",\"customFlag\":99}}";
        var launch = Assert.IsType<LaunchRequest>(DapSerializer.Deserialize(json));

        var extras = launch.Arguments.DeserializeAdditionalProperties<SampleLaunchExtras>();

        Assert.Equal("x.exe", extras.Program);
        Assert.Equal(99, extras.CustomFlag);
    }
}
