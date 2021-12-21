using System;
using System.Collections.Generic;
using System.Threading;

namespace pathfindin_dfs_bfs
{
    public class Szukanie_DFS
    {
        //private static bool w_gore;
        private static bool wypisane;
        private static bool iamstuck;

        public static void DFS(int[,] grid, int row, int col,
             int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            list_cheked_node = Obliczenia.check_node(row, col, start_node);
            int[] pos = new int[] { start_node[0], start_node[1] };
            int koszt_operacji = 0;
            while (!Obliczenia.search_end && !wypisane)
            {

                // w góre
                if (pos[0] > 0 && grid[pos[0] - 1, pos[1]] != 3 && grid[pos[0] - 1, pos[1]] != 1)
                {
                    if (Program.visted_node[pos[0] - 1, pos[1]])
                    {
                        pos[0]--;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        koszt_operacji++;
                    }
                }
                // w lewo
                else if (pos[1] > 0 && grid[pos[0], pos[1] - 1] != 3 && grid[pos[0], pos[1] - 1] != 1)
                {
                    if (Program.visted_node[pos[0], pos[1] - 1])
                    {
                        pos[1]--;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        koszt_operacji++;
                    }
                }
                // w dół 
                else if (pos[0] < row - 1 && grid[pos[0] + 1, pos[1]] != 3 && grid[pos[0] + 1, pos[1]] != 1)
                {
                    if (Program.visted_node[pos[0] + 1, pos[1]])
                    {
                        pos[0]++;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        koszt_operacji++;
                    }
                }
                // w prawo
                else if (pos[1] < col - 1 && grid[pos[0], pos[1] + 1] != 3 && grid[pos[0], pos[1] + 1] != 1)
                {
                    if (Program.visted_node[pos[0], pos[1] + 1])
                    {
                        pos[1]++;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        koszt_operacji++;
                    }
                }
                else if (iamstuck)
                {
                    foreach (var item in list_cheked_node)
                    {
                        if (Program.visted_node[item.Row, item.Col] == true && grid[item.Row, item.Col] != 3 && grid[item.Row, item.Col] != 1)
                        {
                            pos[0] = item.Row;
                            pos[1] = item.Col;
                            iamstuck = false;
                            list_cheked_node = Obliczenia.check_node(row, col, pos);
                            break;
                        }
                    }
                }
                else
                {
                    iamstuck = true;
                }


                foreach (var node in list_cheked_node)
                {
                    //sprawdz czy node przeszukany jest zgodny z nodem docelowym
                    if (node.Row == end_node[0] && node.Col == end_node[1]) // powinno być grid[node.Row, node.Col] == 4 // ale w/e
                    {
                        Console.WriteLine(node.Row + "," + node.Col + " Cel jest w tej pozycji ");

                        //Console.WriteLine("Row " + (start_node[0] - node.Row));
                        //Console.WriteLine("Col " + (start_node[1] - node.Col));
                        Console.WriteLine($"Suma ruchów {Math.Abs(start_node[1] - node.Col) + Math.Abs(start_node[0] - node.Row)}");
                        koszt_operacji++;
                        grid = Obliczenia.Wypisz_Najkrotsza_droge(grid, start_node, node);
                        wypisane = true;
                        /*
                        foreach (var item in Obliczenia.koszt_Ruchu)
                        {

                            List<string> wypisz_kierunki = item.Daj_Liste_Z_Kosztami();
                            foreach (var kierunek in wypisz_kierunki)
                            {
                                Console.WriteLine(kierunek);
                            }
                            Console.WriteLine("============================");
                        }
                        */
                    }
                    else
                    {
                        //Console.WriteLine(node.Row + "," + node.Col + " Cel? " + node.Direction); // debug
                    }

                    if (grid[node.Row, node.Col] != 3)
                    {
                        Program.visted_node[node.Row, node.Col] = node.Visted_node;
                    }


                }

                Console.WriteLine();
                Console.WriteLine($"Koszt operacji wynosi: {koszt_operacji}");
                Obliczenia.write_out(grid, row, col, start_node, end_node);
                Thread.Sleep(500);
            }// while

        }//dfs
    }
}