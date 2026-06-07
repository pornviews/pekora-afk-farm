using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    private const string DefaultLaunchUrl =
        "pekora-player:1+launchmode:play+clientversion:2021M+gameinfo:eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzZXNzaW9uSWQiOiJhN2RiNTMwZC1lZmJmLTRlNjQtODUwYS03YmNjYzU2OTJjZTkiLCJjcmVhdGVkQXQiOjE3ODA3NzE0MTd9.JuLXHsyueBrUFmr7BdlzUHZJj5wS9KVenUT4r2bM3cK6qPHiRzs-CJiGu2QQSpb3i7cLOxfglj7lVQby1IcD3Q+placelauncherurl:https://www.pekora.zip/Game/PlaceLauncher.ashx?request=RequestGame&placeId=338693&isPartyLeader=false&gender=&isTeleport=true+k:l+client";

    private const int StayMinutes = 4;
    private const int RejoinDelaySeconds = 3;

    private static readonly string[] PlayerProcessNames =
    {
        "PekoraPlayer",
        "ProjectXPlayerBeta",
        "ProjectXPlayerBeta.exe"
    };

    static async Task Main()
    {
        Console.Title = "Pekora AFK Rejoiner";

        PrintAscii();
        Log("Pekora AFK Rejoiner loaded.", ConsoleColor.Green);

        Console.WriteLine();
        Log("Paste a Pekora launch URL or press ENTER to use default:", ConsoleColor.Cyan);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("> ");
        Console.ResetColor();

        string? input = Console.ReadLine();
        string launchUrl = string.IsNullOrWhiteSpace(input)
            ? DefaultLaunchUrl
            : input.Trim();

        Log("Using launch URL.", ConsoleColor.Yellow);
        Log("Press CTRL + C to stop.", ConsoleColor.DarkGray);

        int cycle = 1;

        while (true)
        {
            Console.WriteLine();
            Log($"Cycle #{cycle} started.", ConsoleColor.Magenta);

            LaunchGame(launchUrl);

            Log($"Waiting {StayMinutes} minutes inside game...", ConsoleColor.Cyan);
            await Task.Delay(TimeSpan.FromMinutes(StayMinutes));

            Log("Leaving game...", ConsoleColor.Yellow);
            ClosePekora();

            Log($"Rejoining in {RejoinDelaySeconds} seconds...", ConsoleColor.DarkCyan);
            await Task.Delay(TimeSpan.FromSeconds(RejoinDelaySeconds));

            cycle++;
        }
    }

    private static void LaunchGame(string launchUrl)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = launchUrl,
                UseShellExecute = true
            });

            Log("Launch request sent successfully.", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            Log($"Launch failed: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static void ClosePekora()
    {
        bool foundProcess = false;

        foreach (string processName in PlayerProcessNames)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                try
                {
                    foundProcess = true;
                    Log($"Closing {process.ProcessName}...", ConsoleColor.Yellow);

                    process.Kill();
                    process.WaitForExit();

                    Log($"{process.ProcessName} closed.", ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    Log($"Could not close {processName}: {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        if (!foundProcess)
        {
            Log("No Pekora/Roblox player process found.", ConsoleColor.DarkYellow);
        }
    }

    private static void PrintAscii()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"___.                                           .__                     
\_ |__ ___.__. ______   ___________  _______  _|__| ______  _  ________
 | __ <   |  | \____ \ /  _ \_  __ \/    \  \/ /  |/ __ \ \/ \/ /  ___/
 | \_\ \___  | |  |_> >  <_> )  | \/   |  \   /|  \  ___/\     /\___ \ 
 |___  / ____| |   __/ \____/|__|  |___|  /\_/ |__|\___  >\/\_//____  >
     \/\/      |__|                     \/             \/           \/ ");
        Console.ResetColor();
    }

    private static void Log(string message, ConsoleColor color)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"[{DateTime.Now:HH:mm:ss}] ");

        Console.ForegroundColor = color;
        Console.WriteLine(message);

        Console.ResetColor();
    }
}