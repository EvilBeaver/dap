// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if NETSTANDARD2_0
global using JsonPropertyNameAttribute = Newtonsoft.Json.JsonPropertyAttribute;
global using JsonExtensionDataAttribute = Newtonsoft.Json.JsonExtensionDataAttribute;
global using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
#else
global using System.Text.Json.Serialization;
#endif
