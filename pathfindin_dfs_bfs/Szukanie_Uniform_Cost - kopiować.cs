using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Szukanie_Uniform_Cost_2
    {
        private static bool wypisane;
        private static bool iamstuck;


        public static void Uniform(int[,] grid, int row, int col,
            int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            List<Node_koszt> node_Koszt;
            List<Check_node> check_Nodes = new();
            List<Check_node> check_Nodes_goal = new();

            int[] pos = new int[] { start_node[0], start_node[1] };
            int koszt_operacji = 0;

            list_cheked_node = Obliczenia.check_node(row, col, start_node);
            //check_Nodes.Add(new Check_node { Row = pos[0], Col = pos[1] });
            node_Koszt = Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Daj_Liste_Z_node(grid);
            foreach (var item in node_Koszt)
            {
                check_Nodes.Add(new Check_node { Row = item.Row, Col = item.Col, Koszt = item.Koszt});
            }

            while (check_Nodes.Count > 0)
            {

                var min_node = check_Nodes.Min(r => r.Koszt);
                var uwu = check_Nodes.Where(r => r.Koszt == min_node);
                if (check_Nodes_goal.Count > 0)
                {
                    var min_node_goal = check_Nodes_goal.Min(r => r.Koszt);
                    uwu = check_Nodes.Where(r => r.Koszt < min_node_goal);

                    var usuwanie = check_Nodes.Where(r => r.Koszt > min_node_goal);
                    foreach (var del in usuwanie.ToList())
                    {
                        check_Nodes.Remove(del);
                    }

                }          
                
                foreach (var wew in uwu.ToList())
                {
                    pos[0] = wew.Row;
                    pos[1] = wew.Col;
                    Program.arrival_node[pos[0], pos[1]] = true;
                    koszt_operacji = wew.Koszt;
                    node_Koszt = Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Daj_Liste_Z_node(grid);
                    list_cheked_node = Obliczenia.check_node(row, col, pos);
                    foreach (var item in node_Koszt)
                    {
                        if (wew.Row == end_node[0] && wew.Col == end_node[1])
                        {
                            
                            check_Nodes_goal.Add(new Check_node { Row = wew.Row, Col = wew.Col, Koszt = item.Koszt + koszt_operacji });                           
                        }
                        else
                        {
                            check_Nodes.Add(new Check_node { Row = item.Row, Col = item.Col, Koszt = item.Koszt + koszt_operacji });
                        }
                        
                    }
                    //Console.WriteLine(koszt_operacji + "UwU szukam node"); // debug
                    check_Nodes.Remove(wew);
                    
                    break;
                }
              


                Thread.Sleep(1000);
                //tak srednio potrzebne tutaj
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
                        if (check_Nodes_goal.Count > 0)
                        {
                            var lowest_value = check_Nodes_goal.Min(r => r.Koszt);
                            Console.WriteLine($"Koszt operacji wynosi: {lowest_value}");
                        }
                        
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
                
                //var lowest_value_where = check_Nodes_goal.Where(r => r.Koszt == lowest_value);
                Console.WriteLine();
                Console.WriteLine($"Koszt operacji wynosi: {koszt_operacji}");
                
                Obliczenia.write_out_koszt(grid, row, col, start_node, end_node);
                Thread.Sleep(500);
            }// while

        }//Uniform
    }
}
