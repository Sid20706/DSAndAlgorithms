using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutoFac.Leetcode
{
    public class _407_Trapping_Rain_Water2
    {
        public class Cell : IComparable<Cell>
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int Height { get; set; }

            public Cell(int row, int col, int height)
            {
                Row = row;
                Col = col;
                Height = height;
            }

            public int CompareTo(Cell cell)
            {
                return Height - cell.Height;
            }
        }
        public static void Executer()
        {
            int[][] a = new int[][]
            {
               new int[]  {1, 4, 3, 1, 3, 2 },
               new int[]  {3, 2, 1, 3, 2, 4 },
               new int[]  {2, 3, 3, 2, 3, 1 },
            };

            int res = TrapRainWater2(a);
            int[][] b = new int[][]
           {
               new int[]  {12,13,1,12 },
               new int[]  {13,4,13,12 },
               new int[]  {13,8,10,12},
               new int[]  {12,13,12,12},
               new int[]  {13,13,13,13},
           };
            int res2 = TrapRainWater2(b);
        }

        public static int TrapRainWater2(int[][] heightMap)
        {
            int res = 0;

            int rl = heightMap.Length;
            if (rl <= 2)
                return res;

            int cl = heightMap[0].Length;
            if (cl <= 2)
                return res;

            bool[,] visited = new bool[rl, cl];
            DS.PriorityQueue<Cell> queue = new DS.PriorityQueue<Cell>();

            // Add all boundary cells 
            for (int r = 0; r < rl; r++)
            {
                visited[r, 0] = true;
                visited[r, cl - 1] = true;
                queue.Enqueue(new Cell(r, 0, heightMap[r][0]));
                queue.Enqueue(new Cell(r, cl - 1, heightMap[r][cl - 1]));
            }

            for (int c = 0; c < cl; c++)
            {
                visited[0, c] = true;
                visited[rl - 1, c] = true;
                queue.Enqueue(new Cell(0, c, heightMap[0][c]));
                queue.Enqueue(new Cell(rl - 1, c, heightMap[rl - 1][c]));
            }

            int[,] dirs = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            //do 1 BFS (4 directions)
            while (!queue.IsEmpty())
            {
                Cell c = queue.Dequeue();
                for (int i = 0; i < dirs.GetLength(0); i++)
                {
                    int ri = c.Row + dirs[i, 0];
                    int ci = c.Col + dirs[i, 1];
                    if (ri >= 0 && ri < rl && ci >= 0 && ci < cl && !visited[ri, ci]) //validate
                    {

                        visited[ri, ci] = true;
                        //If (ri,ci) height is lower, it can hold water and the amount of water should be c.Height - heightMap[ri][ci]
                        res += Math.Max(0, c.Height - heightMap[ri][ci]);  
                        // take max hieght 
                        queue.Enqueue(new Cell(ri, ci, Math.Max(heightMap[ri][ci] ,c.Height)));

                    }
                }
            }

            return res;
        }

        public static int TrapRainWater(int[][] heightMap)
        {

            int res = 0;

            int rl = heightMap.Length;
            if (rl <= 2)
                return res;

            int cl = heightMap[0].Length;
            if (cl <= 2)
                return res;
            int[,] leftRight = new int[rl, cl];
            int[,] rightLeft = new int[rl, cl];
            int[,] topBottom = new int[rl, cl];
            int[,] bottomTop = new int[rl, cl];

            for (int r = 1; r < rl - 1; r++)
            {
                leftRight[r, 0] = heightMap[r][0];
                for (int c = 1; c < cl; c++)
                {
                    leftRight[r, c] = Math.Max(heightMap[r][c], leftRight[r, c - 1]);

                }

                rightLeft[r, cl - 1] = heightMap[r][cl - 1];
                for (int c = cl - 2; c >= 0; c--)
                {
                    rightLeft[r, c] = Math.Max(heightMap[r][c], rightLeft[r, c + 1]);
                }
            }

            for (int c = 1; c < cl - 1; c++)
            {
                topBottom[0, c] = heightMap[0][c];
                for (int r = 1; r < rl; r++)
                {
                    topBottom[r, c] = Math.Max(heightMap[r][c], topBottom[r - 1, c]);
                }
                bottomTop[rl - 1, c] = heightMap[rl - 1][c];
                for (int r = rl - 2; r >= 0; r--)
                {
                    bottomTop[r, c] = Math.Max(heightMap[r][c], bottomTop[r + 1, c]);

                }
            }

            for (int r = 1; r < rl - 1; r++)
            {
                for (int c = 1; c < cl - 1; c++)
                {
                    int min = Math.Min(leftRight[r, c], Math.Min(rightLeft[r, c], Math.Min(topBottom[r, c], bottomTop[r, c])));
                    if (min > heightMap[r][c])
                        res += min - heightMap[r][c];

                }
            }
            return res;
        }
    }
}
