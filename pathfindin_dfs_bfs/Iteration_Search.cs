using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
     public class Iteration_Search
    {

        private static bool wypisane;
        private static bool iamstuck;

        public static void IS(int[,] grid, int row, int col,
            int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            List<Check_node> check_Nodes;
            int[] do_podania_kordyntow;
            list_cheked_node = Obliczenia.check_node(row, col, start_node);
            int[] pos = new int[] { start_node[0], start_node[1] };
            while (!Obliczenia.search_end && !wypisane)
            {
                foreach (var wew in list_cheked_node.ToList())
                {
                    do_podania_kordyntow = new int[] { wew.Row, wew.Col };
                    Program.arrival_node[wew.Row, wew.Col] = true;
                    check_Nodes = Obliczenia.check_node(row, col, do_podania_kordyntow);
                    check_Nodes.Remove(wew);

                }
                Thread.Sleep(500);

                if (iamstuck)
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



                        Obliczenia.Wypisz_Najkrotsza_droge(grid, start_node, node);
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
                Obliczenia.write_out_koszt(grid, row, col, start_node, end_node);

            }// while

        }//IS
    }
}
