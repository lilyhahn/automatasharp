using System;
using System.Collections.Generic;
using System.Drawing;
using Paisley.CellularAutomata;

class Program
{
    static void Main(string[] args)
    {
        int width = 64;
        int generations = 128;
        int rule = 30;
        int scale = 1;
        string filename = "cells.png";
        string defaultFilename = filename;

        Console.Write("Enter the size of the world. [" + width + "] ");
        int defaultWidth = width;
        if (!Int32.TryParse(Console.ReadLine(), out width))
            width = defaultWidth;

        Console.Write("Enter the desired number of generations to run. [" + generations + "] ");
        int defaultGenerations = generations;
        if (!Int32.TryParse(Console.ReadLine(), out generations))
            generations = defaultGenerations;

        Console.Write("Enter the rule to use. [" + rule + "] ");
        int defaultRule = rule;
        if (!Int32.TryParse(Console.ReadLine(), out rule))
            rule = defaultRule;

        Console.Write("Enter the path where the generated image should be saved. [" + filename + "] ");
        filename = Console.ReadLine();

        Console.Write("Enter image scale multiplier. [" + scale + "] ");
        int defaultScale = scale;
        if (!Int32.TryParse(Console.ReadLine(), out scale))
            scale = defaultScale;

        CyclicElementaryCellularAutomaton ca = new CyclicElementaryCellularAutomaton(rule, width);
        Console.Write("Enter \"r\" for random starting seed or \"s\" for single cell. [r] ");
        string choice = Console.ReadLine();
        if (choice == "s")
        {
            List<byte> startingField = new List<byte>();
            for (int i = 0; i < width; i++)
            {
                startingField.Add(0);
                if (Math.Floor(width / 2f) == i)
                {
                    startingField[i] = 1;
                }
            }
            ca = new CyclicElementaryCellularAutomaton(rule, width, startingField);
        }

        for (int i = 0; i < generations; i++)
        {
            ca.Generate();
            
        }
        //Console.WriteLine(ca.ToString());
        try
        {
            ca.ToImage().Save(filename);
        }
        catch(ArgumentException)
        {
            ca.ToImage(scale).Save(defaultFilename);
        }
        Console.ReadLine();
    }
}
