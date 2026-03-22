// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// On error (whenever `success` is false), the body can provide more details.
/// </summary>
public class ErrorResponse : Response<ErrorResponseBody>
{
    public override string? ToString()
    {
        if (Body?.Error != null)
        {
            return BuildText(Body.Error);
        }
        else
        {
            return base.ToString();
        }
    }

    private string? BuildText(Message message)
    {
        if (string.IsNullOrEmpty(message.Format))
        {
            return "<No message text>";
        }

        var resultString = message.Format;
        if (message.Variables == null)
            return resultString;
        
        // Substitution
        foreach (var messageVariable in message.Variables)
        {
            resultString = resultString.Replace("{" + messageVariable.Key + "}", messageVariable.Value);
        }

        return resultString;
    }
}

public class ErrorResponseBody
{
    /// <summary>
    /// A structured error message.
    /// </summary>
    [JsonPropertyName("error")]
    public Message? Error { get; set; }
}
