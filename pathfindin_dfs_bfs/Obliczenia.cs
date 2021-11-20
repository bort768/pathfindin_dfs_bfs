using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Obliczenia
    {
        public static List<Check_node> checked_node = new List<Check_node>();
        public static bool search_end;
        private static bool w_gore;

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
                    if (i == end_node[0] && j == end_node[1])
                    {
                        grid[i, j] = 4;
                    }

                    if (Program.visted_node[i, j] == true && i == end_node[0] && j == end_node[1])
                    {
                        grid[i, j] = 5;
                        search_end = true;
                    }
                    else if (Program.arrival_node[i, j] && grid[i, j] != 1)
                    {
                        grid[i, j] = 3;

                    }
                    else if(Program.visted_node[i, j] == true && grid[i, j] != 1 && grid[i, j] != 3 && !Program.arrival_node[i, j])
                    {
                        grid[i, j] = 2;
                    }

                    #region COLOR
                    if (grid[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else
                        Console.Write(grid[i, j]);
                    #endregion

                }
                Console.WriteLine();
            }
        }//write out

        /// <summary>
        /// metoda ma zwracac liste klas Check node. Jesli to możliwe w wszytkich kierunkach
        /// </summary>
        /// <param name="row"> ilosc rzedów</param>
        /// <param name="col"> ilosc kolumn</param>
        /// <param name="pos"> pozycja</param>
        /// <returns></returns>
        public static List<Check_node> check_node(int row, int col, int[] pos)
        {
            //int[] check_node_list = new int[8];
            int i = pos[0];
            int j = pos[1];
            
            

            //Console.WriteLine(i+" "+ j); // debug

            if (i < row-1) // dół
            {
                checked_node.Add(new Check_node { Row = i+1, Col = j, Visted_node = true, Direction = Helper_driectrion.DOWN });
                Console.WriteLine();
            }
            if (i > 0) // góra
            {
                checked_node.Add(new Check_node { Row = i - 1, Col = j, Visted_node = true, Direction = Helper_driectrion.UP });
            }
            if (j < col-1 ) // w prawo
            {
                checked_node.Add(new Check_node { Row = i, Col = j +1, Visted_node = true, Direction = Helper_driectrion.RIGHT });
            }
            if (j > 0) // w lewo
            {
                checked_node.Add(new Check_node { Row = i, Col = j-1, Visted_node = true, Direction = Helper_driectrion.LEFT });
            }

            return checked_node;
        }// check_node

        public static void DFS(int[,] grid, int row, int col,
             int[] start_node, int[] end_node)
        {
            List<Check_node> list_cheked_node;
            list_cheked_node = check_node(row, col, start_node);
            int[] pos = new int[] { start_node[0], start_node[1] };
            while (!search_end)
            {

                // w góre
                if (pos[0] > 0 && grid[pos[0] - 1, pos[1]] != 3 && grid[pos[0] - 1, pos[1]] != 1)
                {
                    if (Program.visted_node[pos[0] - 1, pos[1]])
                    {
                        pos[0]--;
                        list_cheked_node = check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                    }
                }
                // w lewo
                else if (pos[1] > 0 && grid[pos[0], pos[1] - 1] != 3 && grid[pos[0], pos[1] - 1] != 1)
                {
                    if (Program.visted_node[pos[0], pos[1] - 1])
                    {
                        pos[1]--;
                        list_cheked_node = check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                    }
                }
                // w dół 
                else if (pos[0] < row-1 && grid[pos[0] + 1, pos[1]] != 3 && grid[pos[0] + 1, pos[1]] != 1)
                {
                    if (Program.visted_node[pos[0] + 1, pos[1]] )
                    {
                        pos[0]++;
                        list_cheked_node = check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        
                    }
                }
                // w prawo
                else if (pos[1] < col && grid[pos[0], pos[1] + 1] != 3 && grid[pos[0], pos[1] + 1] != 1)
                {
                    if (Program.visted_node[pos[0], pos[1]+ 1] )
                    {
                        pos[1]++;
                        list_cheked_node = check_node(row, col, pos);
                        Program.arrival_node[pos[0], pos[1]] = true;
                        
                    }
                }
                
                foreach (var node in list_cheked_node)
                {
                    // row        // col
                    if (node.Row == end_node[0] && node.Col == end_node[1])
                    {
                        Console.WriteLine(node.Row + "," + node.Col + " Cel jest w tej pozycji ");
                    }
                    else 
                    {
                        //Console.WriteLine(node.Row + "," + node.Col + " Cel? " + node.Direction);
                    }

                    if (grid[node.Row, node.Col] != 3)
                    {
                        Program.visted_node[node.Row, node.Col] = node.Visted_node;
                    }

                                       
                }

                



                Thread.Sleep(500);

                Console.WriteLine();
                write_out(grid, row, col, start_node, end_node);
            }
            
        }

    }//class
}