using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    // Zapytać o zliczanie kosztów i jak to się ma do iteracji
    public class Szukanie_Iteracyjne_Koszt
    {
        private static bool wypisane;
        private static bool iamstuck;
        private static List<Node_koszt> node_Koszt;
        private static int koszt_iteracji = 0;

        public static void IS(int[,] grid, int row, int col,
            int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            List<Check_node> check_Nodes = new();

            //int[] do_podania_kordyntow;
            int[] pos = new int[] { start_node[0], start_node[1] };
            int koszt_operacji = 0;

            list_cheked_node = Obliczenia.check_node(row, col, start_node);
            check_Nodes.Add(new Check_node { Row = pos[0], Col = pos[1] });


            foreach (var item in list_cheked_node)
            {


                //kierunek_z_max_kosztem = (Node_koszt)node_Koszt.Where(r => r.Koszt == max);
                //Console.WriteLine(kierunek_z_max_kosztem);

                if (item.Row == end_node[0] && item.Col == end_node[1]) // powinno być grid[node.Row, node.Col] == 4 // ale w/e
                {
                    Console.WriteLine(item.Row + "," + item.Col + " Cel jest w tej pozycji ");

                    Console.WriteLine($"Suma ruchów {Math.Abs(start_node[1] - item.Col) + Math.Abs(start_node[0] - item.Row)}");
                    koszt_iteracji++;
                    if (end_node[0] - pos[0] > 0)
                    {
                        koszt_operacji += Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Node_South[2];
                    }
                    else if (end_node[0] - pos[0] < 0)
                    {
                        koszt_operacji += Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Node_North[2];
                    }
                    else if (end_node[1] - pos[1] > 0)
                    {
                        koszt_operacji += Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Node_East[2];
                    }
                    else if (end_node[1] - pos[1] < 0)
                    {
                        koszt_operacji += Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Node_West[2];
                    }
                    //grid = Obliczenia.Wypisz_Najkrotsza_droge(grid, start_node, node);
                    wypisane = true;
                    Obliczenia.write_out_koszt(grid, row, col, start_node, end_node);
                    Console.WriteLine($"Iteracja wynosi: {koszt_iteracji}");
                    Console.WriteLine($"Koszt wynosi: {koszt_operacji}");
                }
            }

            while (!Obliczenia.search_end && !wypisane)
            {



                foreach (var node in check_Nodes.ToList())
                {
                    node_Koszt = Obliczenia.koszt_Ruchu[node.Row][node.Col].Daj_Liste_Z_node(grid);
                    foreach (var a_node in node_Koszt)
                    { 
                        koszt_operacji += a_node.Koszt;
                        check_Nodes.Add(new Check_node { Row = a_node.Row, Col = a_node.Col });
                        pos[0] = a_node.Row;
                        pos[1] = a_node.Col;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        
                    }
                    check_Nodes.Remove(node);
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
                        koszt_iteracji++;
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
                Console.WriteLine($"Iteracja wynosi: {koszt_iteracji}");
                Console.WriteLine($"Koszt wynosi: {koszt_operacji}");
                Obliczenia.write_out_koszt(grid, row, col, start_node, end_node);

            }// while
        }     
    }
}
