using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Szukanie_Uniform_Cost
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
            check_Nodes.Add(new Check_node { Row = pos[0], Col = pos[1] });

            while (check_Nodes.Count > 0 || !wypisane)
            {


                //koszt operacji w punkcjie starowym zawsze bedzie najmniejszy, ponieważ kosztu nie ma 
                var min_node = check_Nodes.Min(r => r.Koszt);
                var uwu = check_Nodes.Where(r => r.Koszt == min_node);

                foreach (var wew in uwu.ToList())
                {
                    pos[0] = wew.Row;
                    pos[1] = wew.Col;
                    koszt_operacji = wew.Koszt;
                    //Console.WriteLine(koszt_operacji + "UwU szukam node"); // debug
                    //check_Nodes.Remove(wew);
                }

                 node_Koszt = Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Daj_Liste_Z_node(grid);

                if (node_Koszt.Count > 0)
                {
                    foreach (var a_node in node_Koszt)
                    {
                        var min = node_Koszt.Min(r => r.Koszt);
                        if (a_node.Row == end_node[0] && a_node.Col == end_node[1])
                        {
                            koszt_operacji += a_node.Koszt;
                            check_Nodes_goal.Add(new Check_node { Row = a_node.Row, Col = a_node.Col, Koszt = koszt_operacji });
                            Program.arrival_node[pos[0], pos[1]] = true;
                            //Console.WriteLine(koszt_operacji + " Do goal");
                            var node_Koszt_2 = Obliczenia.koszt_Ruchu[check_Nodes[0].Row][check_Nodes[0].Col].Daj_Liste_Z_node(grid);
                            if (node_Koszt_2.Count > 0)
                            {
                                var min_kosszt_2 = node_Koszt_2.Min(d => d.Koszt);
                                var node_checker_2 = check_Nodes.Where(r => r.Koszt == min_kosszt_2);
                                foreach (var node_2 in node_checker_2.ToList())
                                {
                                    //przeszukaj czy jest node z mniejszym kosztem niz sciezka do goal
                                    var goal_koszt = check_Nodes_goal.Where(r => r.Koszt < (koszt_operacji + node_2.Koszt));
                                    foreach (var goal in goal_koszt)
                                    {
                                        check_Nodes.Add(new Check_node { Row = a_node.Row, Col = a_node.Col, Koszt = koszt_operacji });
                                    }
                                    
                                }
                                var node_del = check_Nodes.Where(r => r.Koszt > min_kosszt_2);
                                foreach (var Node_delete in node_del.ToList())
                                {
                                    check_Nodes.Remove(Node_delete);
                                }
                                
                                node_Koszt_2.Clear();
                               
                            }
                            break; 
                        }

                        //else if (a_node.Koszt == min && check_Nodes[0].Koszt <= koszt_operacji)
                        else if (a_node.Koszt == min && check_Nodes.Min(r => r.Koszt) < koszt_operacji)
                        {
                           koszt_operacji += a_node.Koszt;
                           pos[0] = a_node.Row;
                           pos[1] = a_node.Col;

                           
                           list_cheked_node = Obliczenia.check_node(row, col, pos);
                           var node_Koszt_2 = Obliczenia.koszt_Ruchu[pos[0]][pos[1]].Daj_Liste_Z_node(grid);
                           if (node_Koszt_2.Count > 0)
                           {
                               var min_kosszt_2 = node_Koszt_2.Min(d => d.Koszt);
                               var node_checker_2 = check_Nodes.Where(r => r.Koszt == min_kosszt_2);
                               foreach (var node_2 in node_checker_2)
                               {
                                   koszt_operacji += node_2.Koszt;
                               }
                               node_Koszt_2.Clear();
                           }


                           //if (a_node.Row == end_node[0] && a_node.Col == end_node[1])
                           //{
                           //   Console.WriteLine($"Koszt wynosi {koszt_operacji}");
                           //}
                           //Console.WriteLine($"Jestem w Row {a_node.Row} i Col {a_node.Col}");
                           Program.arrival_node[pos[0], pos[1]] = true;
                           check_Nodes.Add(new Check_node { Row = a_node.Row, Col = a_node.Col, Koszt = koszt_operacji });
                           node_Koszt.RemoveAt(0);
                           //check_Nodes.RemoveAt(0);
                           break;
                        }

                        else if (a_node.Koszt == min )
                        {
                            bool only_to_if = false;
                            var where_is_lowest_value = check_Nodes.Where((r) => r.Koszt < a_node.Koszt);
                            foreach (var lowest_value_node in where_is_lowest_value)
                            {
                                koszt_operacji += a_node.Koszt;
                                pos[0] = a_node.Row;
                                pos[1] = a_node.Col;
                                check_Nodes.Add(new Check_node { Row = a_node.Row, Col = a_node.Col, Koszt = koszt_operacji });
                                list_cheked_node = Obliczenia.check_node(row, col, pos);
                                Program.arrival_node[pos[0], pos[1]] = true;
                                only_to_if = true;
                                break;
                            }
                            if (!only_to_if && check_Nodes.Min(r => r.Koszt) <= koszt_operacji)
                            {
                                koszt_operacji += a_node.Koszt;
                                pos[0] = a_node.Row;
                                pos[1] = a_node.Col;
                                check_Nodes.Add(new Check_node { Row = a_node.Row, Col = a_node.Col, Koszt = koszt_operacji });
                                list_cheked_node = Obliczenia.check_node(row, col, pos);
                                Program.arrival_node[pos[0], pos[1]] = true;
                            }

                            //check_Nodes.RemoveAt(0);
                            if (node_Koszt.Count > 1)
                            {
                                node_Koszt.Clear();
                            }
                            else
                            {
                                check_Nodes.RemoveAt(0);
                            }
                            
                            
                            break;
                        }

                        

                    }
                }
                else
                {

                    var node_remover = check_Nodes.Where(r => r.Row == pos[0] && r.Col == pos[1]);
                    foreach (var node in node_remover.ToList())
                    {
                        check_Nodes.Remove(node);
                    }
                  
                }

                koszt_operacji = 0;

                

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
