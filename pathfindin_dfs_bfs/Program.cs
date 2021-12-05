using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace pathfindin_dfs_bfs
{
    class Program
    {
        //rzedy i kolmny generowane losowo
        static int row = Obliczenia.Give_Random_Number(3, 6);
        static int col = Obliczenia.Give_Random_Number(3, 6);
        public static bool[,] visted_node = Obliczenia.Enter_false_visted(row, col);
        public static bool[,] arrival_node = Obliczenia.Enter_false_visted(row, col);

        static void Main()
        {
            
            //pozycja starowa
            //przypisane są w funkcji main
            int[] start_node;
            int[] end_node;

            //generowanie pola
            int[,] grid = Obliczenia.Enter_value_to_grid(row, col);

            //
            //List<int[,]> search_node;

            start_node = new int[] { Obliczenia.Give_Random_Number(0, row), Obliczenia.Give_Random_Number(0, col) };

            do
            {
                end_node = new int[] { Obliczenia.Give_Random_Number(0, row), Obliczenia.Give_Random_Number(0, col) };
                //Console.WriteLine("WEW"); 
            }
            while (start_node[0] == end_node[0] && start_node[1] == end_node[1]);

            Obliczenia.write_out(grid, row, col, start_node, end_node);



            int[,] grid_bfs = grid;
            Szukanie_DFS.DFS(grid, row, col, start_node, end_node);
            BFS.BFS_M(grid_bfs, row, col, start_node, end_node);
            //Iteration_Search.IS(grid_bfs, row, col, start_node, end_node);


        }
    }
}
