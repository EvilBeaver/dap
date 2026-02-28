// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class LoadedSourcesRequest : Request<LoadedSourcesArguments>
{
    public LoadedSourcesRequest() => Command = "loadedSources";
}

public class LoadedSourcesArguments
{
}

public class LoadedSourcesResponse : Response<LoadedSourcesResponseBody>
{
}

public class LoadedSourcesResponseBody
{
    [JsonPropertyName("sources")]
    public Source[] Sources { get; set; } = default!;
}
