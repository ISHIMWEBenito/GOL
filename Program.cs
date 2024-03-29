﻿using System;

namespace GameOfLife {

    public class LifeSimulation {
        private int Heigth;
        private int Width;
        private bool[,] cells;

        public LifeSimulation(int Heigth, int Width) {
            this.Heigth = Heigth;
            this.Width = Width;
            cells = new bool[Heigth, Width];
            GenerateField();
        }

        public void DrawAndGrow() {
            DrawGame();
            Grow();
        }


        private void Grow() {
            for (int i = 0; i < Heigth; i++) {
                for (int j = 0; j < Width; j++) {
                    int numOfAliveNeighbors = GetNeighbors(i, j);

                    if (cells[i, j]) {
                        if (numOfAliveNeighbors < 2) {
                            cells[i, j] = false;
                        }

                        if (numOfAliveNeighbors > 3) {
                            cells[i, j] = false;
                        }
                    }
                    else {
                        if (numOfAliveNeighbors == 3) {
                            cells[i, j] = true;
                        }
                    }
                }
            }
        }

            private int GetNeighbors(int x, int y) {
            int NumOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++) {
                for (int j = y - 1; j < y + 2; j++) {
                    if (!((i < 0 || j < 0) || (i >= Heigth || j >= Width))) {
                        if (cells[i, j] == true) NumOfAliveNeighbors++;
                    }
                }
            }
            return NumOfAliveNeighbors;
        }


        private void DrawGame() {
            for (int i = 0; i < Heigth; i++) {
                for (int j = 0; j < Width; j++) {
                    Console.Write(cells[i, j] ? "x" : " ");
                    if (j == Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        private void GenerateField() {
            Random generator = new Random();
            int number;
            for (int i = 0; i < Heigth; i++) {
                for (int j = 0; j < Width; j++) {
                    number = generator.Next(2);
                    cells[i, j] = ((number == 0) ? false : true);
                }
            }
        }
    }

    internal class Program {

        private const int Heigth = 10;
        private const int Width = 30;
        private const uint MaxRuns = 100;

        private static void Main(string[] args) {
            int runs = 0;
            LifeSimulation sim = new LifeSimulation(Heigth, Width);  
          
            while (runs++ < MaxRuns) {
                sim.DrawAndGrow();

                System.Threading.Thread.Sleep(100);
            }
        }
    }
}