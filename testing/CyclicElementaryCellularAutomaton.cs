using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Paisley.CellularAutomata {
    class CyclicElementaryCellularAutomaton : ElementaryCellularAutomaton{
        new protected int rule;

        public CyclicElementaryCellularAutomaton(int ruleInt, int worldSize, List<byte> initWorld = null){
            History = new List<List<byte>>();
            World = new List<byte>();
            rule = ruleInt;
            Random random = new Random();
            if (initWorld != null) {
                World = initWorld;
            }
            else {
                for (int i = 0; i < worldSize; i++) {
                    World.Add((byte)random.Next(0, rule));
                }
            }
        }

        public override void Generate() {
            List<byte> nextWorld = new List<byte>();
            for (int i = 0; i < World.Count; i++) {
                nextWorld.Add(0);
            }
            for (int i = 0; i < World.Count; i++) {
                int mcell = World[i];
                int lcell = 0;
                int rcell = 0;
                if (i == 0)
                    lcell = World[World.Count - 1];
                else if (i == World.Count - 1)
                    rcell = World[0];
                else {
                    lcell = World[i - 1];
                    rcell = World[i + 1];
                }
                if (lcell % rule == (mcell + 1) % rule){
                    nextWorld[i] = (byte)(lcell % rule);
                }
                else
                    nextWorld[i] = (byte) mcell;
                if (rcell % rule == (mcell + 1) % rule) {
                    nextWorld[i] = (byte) (rcell % rule);
                }
                else
                    nextWorld[i] = (byte) mcell;
            }
            History.Add(World);
            World = nextWorld;
        }

        public override Bitmap ToImage(int pixelScale = 1, List<Color> colors = null) {
            if (colors == null) {
                colors = new List<Color>() {
                    Color.FromArgb(127, 0, 255),
                    Color.FromArgb(255, 0, 127),
                    Color.FromArgb(255, 127, 0),
                    Color.FromArgb(127, 255, 0),
                    Color.FromArgb(0, 255, 127),
                    Color.FromArgb(0, 127, 255),
                    Color.FromArgb(255, 0, 255),
                    Color.FromArgb(255, 0, 0),
                    Color.FromArgb(255, 255, 0),
                    Color.FromArgb(0, 255, 0),
                    Color.FromArgb(0, 255, 255),
                    Color.FromArgb(0, 0, 255)
                };
            }
            return base.ToImage(pixelScale, colors);
        }
    }
}
