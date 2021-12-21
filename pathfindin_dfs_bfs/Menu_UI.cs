using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Menu_UI
    {
        private static bool wyjscie;

        public static void Wyswietl_Menu(int[,] grid, int row, int col, int[] start_node, int[] end_node) 
        {
            Obliczenia.Make_Cost_Node(row, col);
            //int[,] uwu = grid;
            while (!wyjscie)
            {
                Obliczenia.search_end = false;
                Program.visted_node = Obliczenia.Enter_false_visted(row, col);
                Program.arrival_node = Obliczenia.Enter_false_visted(row, col);
                Obliczenia.checked_node.Clear();
                Console.WriteLine($"Row: {row} Col: {col}");
                grid = Obliczenia.Enter_value_to_grid_Menu(row, col); // problem jest ponieawż da nowe koszty
                //problem z wypisaniem po przeszukaniu (Grid jest taki sam jak wczesniej)
                Obliczenia.write_out(grid, row, col, start_node, end_node);
                //Obliczenia.write_out(uwu, row, col, start_node, end_node);
                Console.WriteLine();
                Console.WriteLine("1.DFS");
                Console.WriteLine("2.BFS");
                Console.WriteLine("3.IS");
                Console.WriteLine("======z kosztem ruchu======");
                Console.WriteLine("4.DFS");
                Console.WriteLine("5.BFS");
                Console.WriteLine("6.IS");
                Console.WriteLine("7.Greedy");
                Console.WriteLine("8.Uniform cost");
                Console.WriteLine("9.Exit");

                Console.Write("Wybierz skrypt: ");
                int c;
                try
                {
                    c = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    continue;
                    //throw;
                }
                

                
                
                //BFS.BFS_M(grid_bfs, row, col, start_node, end_node);
                //Iteration_Search.IS(grid_bfs, row, col, start_node, end_node);
                switch (c)
                {
                    case 1:
                        //int[,] grid_dfs = grid;
                        Szukanie_DFS.DFS(grid, row, col, start_node, end_node);     
                        break;
                    case 2:
                        //int[,] grid_bfs = grid;
                        BFS.BFS_M(grid, row, col, start_node, end_node);
                        //Obliczenia.search_end = false;
                        //Program.visted_node = Obliczenia.Enter_false_visted(row, col);
                        //Program.arrival_node = Obliczenia.Enter_false_visted(row, col);
                        break;
                    case 3:
                        //int[,] grid_dfs = grid;
                        Iteration_Search.IS(grid, row, col, start_node, end_node);
                        break;
                    case 4:
                        //int[,] grid_dfs = grid;
                        Szukanie_DFS_Koszt.DFS(grid, row, col, start_node, end_node);
                        break;
                    case 5:
                        Szukanie_BFS_Koszt.BFS_M(grid, row, col, start_node, end_node);
                        break;
                    case 6:
                        Szukanie_Iteracyjne_Koszt.IS(grid, row, col, start_node, end_node);
                        break;
                    case 7:
                        Szukanie_Greedy.Greedy(grid, row, col, start_node, end_node);
                        break;
                    case 8:
                        Szukanie_Uniform_Cost_2.Uniform(grid, row, col, start_node, end_node);
                        break;
                    case 9:
                        wyjscie = true;
                        break;
                    default:
                        break;
                }
               
            }

            
        }

    }
}
