# Pekora AFK Rejoiner

A lightweight Windows console application that automatically joins a Pekora game, stays connected for a configurable amount of time, leaves, and rejoins in a continuous loop.

## Features

* Automatic game joining
* Automatic game leaving
* Automatic rejoining
* Colorized console output
* Timestamped logs
* Custom launch URL support
* Default game preset included
* Simple and lightweight
* Built with C# and .NET

## Requirements

* Windows 10/11
* .NET 8 SDK or newer
* Pekora Client installed
* Registered `pekora-player:` protocol handler

## Default Game

The application ships configured to join:

Natural Disaster Survival 2021

Place ID: `338693`

You may also provide a custom Pekora launch URL when the application starts.

## Usage

1. Launch the executable.
2. Press ENTER to use the default game.
3. Or paste a custom Pekora launch URL.
4. The application will:

   * Launch Pekora
   * Remain in-game for 4 minutes
   * Close the client
   * Wait briefly
   * Rejoin automatically
5. Press `CTRL + C` to stop the program.

## Console Output

The application provides:

* Colored status messages
* Join events
* Leave events
* Rejoin events
* Error reporting
* Timestamped activity logs

## Configuration

Default values can be modified inside `Program.cs`.

```csharp
private const int StayMinutes = 4;
private const int RejoinDelaySeconds = 8;
```

## Disclaimer

This software is intended for personal use, testing, and automation within Pekora. Users are responsible for complying with any server rules, policies, or terms that may apply to their environment.

## License

MIT License
