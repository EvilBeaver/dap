# EvilBeaver.DAP.Dto

Typed classes and serialization logic for all [Debug Adapter Protocol (DAP)](https://microsoft.github.io/debug-adapter-protocol/specification) messages and types.

This package provides a complete set of DTOs (Data Transfer Objects) for requests, responses, and events defined in the DAP specification. It includes specialized JSON serialization logic to handle the protocol's message structure.

## Features

- **Full DAP Coverage**: Includes all types from the latest DAP specification.
- **Strongly Typed**: Every request, response, and event is represented by a C# class.
- **Custom Serialization**: Built-in `DapSerializer` and `DapMessageConverter` for seamless JSON handling.

## Requirements

- .NET 8
