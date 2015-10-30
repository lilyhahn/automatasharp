using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Paisley.CellularAutomata
{
    class ElementaryCellularAutomaton
    {
        protected byte[] rule = new byte[8];
        public List<byte> World { get; protected set; }
        public List<List<byte>> History { get; protected set; }

        public ElementaryCellularAutomaton(){

        }

        public ElementaryCellularAutomaton(int ruleInt, int worldSize, List<byte> initWorld = null)
        {
            History = new List<List<byte>>();
            World = new List<byte>();
            Random random = new Random();
            if (initWorld != null)
            {
                World = initWorld;
            }
            else
            {
                for (int i = 0; i < worldSize; i++)
                {
                    World.Add((byte)random.Next(0, 2));
                }
            }
            string ruleString = Convert.ToString(ruleInt, 2).PadLeft(8, '0');
            for (int i = 0; i < ruleString.Length; i++)
            {
                rule[i] = (byte)Char.GetNumericValue(ruleString[i]);
            }
        }

        public virtual void Generate()
        {
            List<byte> nextWorld = new List<byte>();
            for (int i = 0; i < World.Count; i++)
            {
                nextWorld.Add(0);
            }
            for (int i = 0; i < World.Count; i++)
            {
                int mcell = World[i];
                int lcell = 0;
                int rcell = 0;
                if (i == 0)
                    lcell = World[World.Count - 1];
                else if (i == World.Count - 1)
                    rcell = World[0];
                else
                {
                    lcell = World[i - 1];
                    rcell = World[i + 1];
                }
                if (lcell == 1 && rcell == 1 && mcell == 1)
                {
                    nextWorld[i] = rule[0];
                }
                else if (lcell == 1 && rcell == 0 && mcell == 1)
                {
                    nextWorld[i] = rule[1];
                }
                else if (lcell == 1 && rcell == 1 && mcell == 0)
                {
                    nextWorld[i] = rule[2];
                }
                else if (lcell == 1 && rcell == 0 && mcell == 0)
                {
                    nextWorld[i] = rule[3];
                }
                else if (lcell == 0 && rcell == 1 && mcell == 1)
                {
                    nextWorld[i] = rule[4];
                }
                else if (lcell == 0 && rcell == 0 && mcell == 1)
                {
                    nextWorld[i] = rule[5];
                }
                else if (lcell == 0 && rcell == 1 && mcell == 0)
                {
                    nextWorld[i] = rule[6];
                }
                else if (lcell == 0 && rcell == 0 && mcell == 0)
                {
                    nextWorld[i] = rule[7];
                }
            }
            History.Add(World);
            World = nextWorld;
        }

        new public string ToString()
        {
            string output = "";
            for(int x = 0; x < History.Count; x++){
                for(int y = 0; y < History[0].Count; y++){
                    output += History[x][y].ToString();
                }
                output += "\n";
            }
            return output;
        }
        public virtual Bitmap ToImage(int pixelScale = 1, List<Color> colors = null)
        {
            if (colors == null) {
                colors = new List<Color>() {
                    Color.White,
                    Color.Black
                };
            }
            Bitmap bmp = new Bitmap(World.Count * pixelScale, History.Count * pixelScale);
            for (int y = 0; y < History.Count * pixelScale; y += pixelScale)
            {
                for (int x = 0; x < World.Count * pixelScale; x += pixelScale)
                {
                    Color paintColor = colors[colors.Count - 1];
                    if (History[y][x] < colors.Count) {
                        paintColor = colors[History[y][x]];
                    }
                    for (int i = 0; i < pixelScale; i++)
                    {
                        for (int j = 0; j < pixelScale; j++){
                            bmp.SetPixel(x + j, y + i, paintColor);
                        }
                    }
                }
            }
            return bmp;
        }
    }
}