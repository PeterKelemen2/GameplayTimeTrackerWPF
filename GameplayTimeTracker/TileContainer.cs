using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GameplayTimeTracker;

public class TileContainer
{
    private List<Tile> tilesList = new();

    // public TileContainer()
    // {
    // }

    public List<Tile> GetTiles()
    {
        return tilesList;
    }

    public double GetTilesTotalHeight()
    {
        double sum = 0;
        foreach (var tile in tilesList)
        {
            sum += tile.GetTotalHeight();
        }

        return sum;
    }

    public int GetListCount()
    {
        return tilesList.Count;
    }

    public void AddTile(Tile newTile)
    {
        try
        {
            if (tilesList.Count == 0)
            {
                newTile.Id = 1;
            }
            else
            {
                newTile.Id = tilesList.ElementAt(tilesList.Count - 1).Id + 1;
            }


            tilesList.Add(newTile);

            // This accounts for change in percentages when adding new tile
            double auxPlaytime = CalculateTotalPlaytime();
            foreach (var tile in tilesList)
            {
                tile.TotalPlaytimePercent = Math.Round(tile.TotalPlaytime / auxPlaytime, 2);
                tile.InitializeTile();
            }

            Console.WriteLine($"Tile added to TileContainer!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong adding new Tile to container! ({e})");
        }
    }

    public void ListTiles()
    {
        try
        {
            Console.WriteLine("\nTileContainer content:");
            Console.WriteLine($"Total Playtime: {CalculateTotalPlaytime()} min");
            foreach (var tile in tilesList)
            {
                Console.WriteLine(
                    $"Id: {tile.Id} | Name: {tile.Text} | Total: {tile.TotalPlaytime} min |" +
                    $" Total%: {tile.TotalPlaytimePercent} | Last: {tile.LastPlaytime} | " +
                    $"Last%: {tile.LastPlaytimePercent}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong listing TileContainer content!({e})");
        }
    }

    public void RemoveTileById(int id)
    {
        try
        {
            foreach (var tile in tilesList.ToList())
            {
                if (tile.Id.Equals(id))
                {
                    tilesList.Remove(tile);
                }
            }

            Console.WriteLine($"\nTile with id {id} removed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void UpdateTileById(int id, string propertyName, object newValue)
    {
        foreach (var tile in tilesList)
        {
            if (tile.Id.Equals(id))
            {
                var property = tile.GetType().GetProperty(propertyName);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(tile, newValue);
                }
            }
        }
    }

    public double CalculateTotalPlaytime()
    {
        double playtime = 0;
        foreach (var tile in tilesList)
        {
            playtime += tile.TotalPlaytime;
        }

        return playtime;
    }
}