﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameplayTimeTracker;

public class ProcessTracker
{
    List<Tile> _tilesList;
    List<String> _exeNames;
    TileContainer _tileContainer;
    private string runningText = "Running!";
    private string notRunningText = "";

    public ProcessTracker()
    {
    }

    public void InitializeProcessTracker(TileContainer tileContainer)
    {
        _tileContainer = tileContainer;
        _exeNames = _tileContainer.GetExecutableNames();
        _tilesList = _tileContainer.GetTiles();

        foreach (var exeName in _exeNames)
        {
            Console.WriteLine($"Exe name: {exeName}");
        }
    }

    public void HandleProcesses()
    {
        _tilesList = _tileContainer.GetTiles();
        var runningProcesses = Process.GetProcesses();

        Console.WriteLine("=================");
        foreach (var tile in _tilesList)
        {
            var newExeName = System.IO.Path.GetFileNameWithoutExtension(tile.ExePath);
            var isRunning =
                runningProcesses.Any(p => p.ProcessName.Equals(newExeName, StringComparison.OrdinalIgnoreCase));
            if (isRunning)
            {
                tile.IsRunning = true;

                if (tile.wasRunning == false)
                {
                    tile.wasRunning = true;
                    tile.ResetLastPlaytime();
                    tile.UpdatePlaytimeText();
                    Console.WriteLine("Updating bars from ProcessTracker - HandleProcesses");
                    _tileContainer.UpdatePlaytimeBars();
                    Console.WriteLine($"Setting new last playtime for {newExeName}");
                }

                if (!tile.runningTextBlock.Text.Equals(runningText))
                {
                    tile.runningTextBlock.Text = runningText;
                }

                tile.CurrentPlaytime++;
                tile.CalculatePlaytimeFromSec(tile.CurrentPlaytime);
            }
            else
            {
                if (tile.IsRunning)
                {
                    if (!tile.runningTextBlock.Text.Equals(notRunningText))
                    {
                        tile.runningTextBlock.Text = notRunningText;
                    }

                    tile.wasRunning = false;
                    tile.IsRunning = false;
                }
            }

            tile.ToggleBgImageColor(isRunning);
        }

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}");
    }


    // public async Task TrackProcessesAsync()
    // {
    //     Console.WriteLine("Starting process tracking...");
    //
    //     Stopwatch stopwatch = new Stopwatch();
    //     while (true)
    //     {
    //         stopwatch.Restart();
    //         var runningProcesses = Process.GetProcesses();
    //         Console.WriteLine("=================");
    //         string runningString = "Running: ";
    //         string notRunningString = "Not running: ";
    //         foreach (var tile in _tilesList)
    //         {
    //             var newExeName = System.IO.Path.GetFileNameWithoutExtension(tile.ExePath);
    //             var isRunning =
    //                 runningProcesses.Any(p => p.ProcessName.Equals(newExeName, StringComparison.OrdinalIgnoreCase));
    //             if (isRunning)
    //             {
    //                 tile.IsRunning = true;
    //                 if (tile.wasRunning == false)
    //                 {
    //                     tile.wasRunning = true;
    //                     tile.ResetLastPlaytime();
    //                     tile.UpdatePlaytimeText();
    //                     _tileContainer.UpdatePlaytimeBars();
    //                     _tileContainer.InitSave();
    //                     Console.WriteLine($"Setting new last playtime for {newExeName}");
    //                 }
    //
    //                 tile.runningTextBlock.Text = "Running!";
    //                 tile.CurrentPlaytime++;
    //                 // Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - {newExeName} is running.");
    //                 runningString += $"{tile.GameName} | ";
    //
    //                 tile.CalculatePlaytimeFromSec(tile.CurrentPlaytime);
    //             }
    //             else
    //             {
    //                 // Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - {newExeName} is not running.");
    //                 tile.runningTextBlock.Text = "";
    //                 tile.wasRunning = false;
    //                 notRunningString += $"{tile.GameName} | ";
    //             }
    //
    //             tile.ToggleBgImageColor(isRunning);
    //         }
    //
    //         // _tileContainer.SortByProperty("IsRunning", false);
    //         Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}");
    //         // Console.WriteLine($"{runningString}");
    //         // Console.WriteLine($"{notRunningString}");
    //         stopwatch.Stop();
    //         await Task.Delay(1000 - (int)stopwatch.ElapsedMilliseconds);
    //     }
    // }
}