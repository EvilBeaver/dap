// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Интерфейс для взаимодействия с клиентом DAP (IDE).
/// Позволяет отправлять события на сторону IDE.
/// </summary>
public interface IClientChannel
{
    /// <summary>
    /// Отправляет асинхронное событие клиенту.
    /// </summary>
    /// <param name="event">Объект события для отправки.</param>
    /// <param name="ct">Токен отмены операции.</param>
    Task SendEventAsync(Event @event, CancellationToken ct = default);
    
    /// <summary>
    /// Отправляет ответ клиенту в рамках обработки запроса
    /// </summary>
    /// <param name="response">Исходный запрос</param>
    /// <param name="ct">Токен отмены операции</param>
    Task SendResponseAsync(Response response, CancellationToken ct = default);
}
