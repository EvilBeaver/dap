// /*----------------------------------------------------------
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v.2.0. If a copy of the MPL
// was not distributed with this file, You can obtain one
// at http://mozilla.org/MPL/2.0/.
// ----------------------------------------------------------*/

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Thrown from adapter methods if adapter wants to return ErrorResponse instead of successful operation result.
/// </summary>
public class ErrorResponseException : ApplicationException
{
    public ErrorResponse ErrorResponse { get; }

    public ErrorResponseException(ErrorResponse errorResponse)
    {
        ErrorResponse = errorResponse;
    }
    
    public ErrorResponseException(string errorText)
    {
        ErrorResponse = new ErrorResponse
        {
            Body = new ErrorResponseBody
            {
                Error = new Message
                {
                    Format = errorText
                }
            }
        };
    }
    
    public override string Message => "Error response: " + ErrorResponse.ToString();
}