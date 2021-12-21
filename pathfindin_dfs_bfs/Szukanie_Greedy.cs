using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Szukanie_Greedy
    {
        private static bool wypisane;
        private static bool iamstuck;


        public static void Greedy(int[,] grid, int row, int col,
            int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            List<Node_koszt> node_Koszt;
            
            list_cheked_node = Obliczenia.check_node(row, col, start_node);

            int[] pos = new int[] { start_node[0], start_node[1] };
            int koszt_operacji = 0;
            foreach (var item in list_cheked_node)
            {
                

                //kierunek_z_max_kosztem = (Node_koszt)node_Koszt.Where(r => r.Koszt == max);
                //Console.WriteLine(kierunek_z_max_kosztem);

                if (item.Row == end_node[0] && item.Col == end_node[1]) // powinno być grid[node.Row, node.Col] == 4 // ale w/e
                {
                    Console.WriteLine(item.Row + "," + item.Col + " Cel jest w tej pozycji ");

                    Console.WriteLine($"Suma ruchów {Math.Abs(start_node[1] - item.Col) + Math.Abs(start_node[0] - item.Row)}");
                    //koszt_operacji++;
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

                }
            }
            while (!Obliczenia.search_end && !wypisane)
            {

                node_Koszt = Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Daj_Liste_Z_node(grid);

                var max = node_Koszt.Max(r => r.Koszt);
                foreach (var a_node in node_Koszt)
                {
                    if (a_node.Koszt == max)
                    {
                        koszt_operacji += a_node.Koszt;
                        pos[0] = a_node.Row;
                        pos[1] = a_node.Col;
                        list_cheked_node = Obliczenia.check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        break;
                    }
                }


                foreach (var node in list_cheked_node)
                {
                    //sprawdz czy node przeszukany jest zgodny z nodem docelowym
                    if (node.Row == end_node[0] && node.Col == end_node[1]) // powinno być grid[node.Row, node.Col] == 4 // ale w/e
                    {
                        Console.WriteLine(node.Row + "," + node.Col + " Cel jest w tej pozycji ");

                        //Console.WriteLine("Row " + (start_node[0] - node.Row));
                        //Console.WriteLine("Col " + (start_node[1] - node.Col));
                        //Console.WriteLine($"Suma ruchów {Math.Abs(start_node[1] - node.Col) + Math.Abs(start_node[0] - node.Row)}");
                        //koszt_operacji++;
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
                Obliczenia.write_out_koszt(grid, row, col, start_node, end_node);
                Thread.Sleep(500);
            }// while

        }//bfs

    }
}
