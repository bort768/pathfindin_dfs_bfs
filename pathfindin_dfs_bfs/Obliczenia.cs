using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Obliczenia
    {
        public static int Give_Random_Number(int x, int y)
        {
            Random rnd = new();
            return rnd.Next(x, y);
        } // Give_Random_Number

        public static bool[,] Enter_false_visted(int row, int col)
        {
            bool[,] visited_node = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    visited_node[i, j] = false;
                }
            }
            return visited_node;
        } //Enter_false_visted

        public static int[,] Enter_value_to_grid(int row, int col)
        {
            int[,] grid = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grid[i, j] = 0;
                }
            }
            return grid;
        }// Enter_value_to_grid

        public static void write_out(int[,] grid, int row, int col, int[] start_node, int[] end_node)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col ; j++)
                {
                    if (i == start_node[0] && j == start_node[1])
                    {
                        grid[i, j] = 1;
                    }
                    else if (i == end_node[0] && j == end_node[1])
                    {
                        grid[i, j] = 4;
                    }

                    Console.Write(grid[i, j]);

                }
                Console.WriteLine();
            }
        }//write out

        /// <summary>
        /// metoda ma zwracac liste int'ow. Jesli to możliwe w wszytkich kierunkach
        /// </summary>
        /// <param name="row"> ilosc rzedów</param>
        /// <param name="col"> ilosc kolumn</param>
        /// <param name="pos"> pozycja</param>
        /// <returns></returns>
        public static int[] check_node(int row, int col, int[] pos)
        {
            int[] check_node_list = new int[8];
            int i = pos[0];
            int j = pos[1];

            if (i < row)
            {
                
                check_node_list[0] = i+1;
                check_node_list[1] = j;
            }
            if (i > 1)
            {
                
                check_node_list[2] = i-1;
                check_node_list[3] = j;
            }
            if (j < col)
            {
                check_node_list[4] = i;
                check_node_list[5] = j+1;
            }
            if (j > 1)
            {
                check_node_list[6] = i;
                check_node_list[7] = j-1;
            }
            return check_node_list;
        }// check_node

        public static void DFS(int[,] grid, int row, int col,
            bool[,] visited_node, int[] start_node, int[] end_node)
        {
            int[] pos = { start_node[0], start_node[1] };

            int[] lists = check_node(row, col, pos);

            for (int i = 0; i < lists.Length/2; i++)
            {
                Console.WriteLine(lists[i]+","+lists[i+1]); 
            }
                //Thread.Sleep(30);

        }

    }//class
}