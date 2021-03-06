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
        public static List<List<Koszt_Ruchu>> koszt_Ruchu = new();
        public static bool search_end;

        public static int Give_Random_Number(int x, int y)
        {
            Random rnd = new();
            return rnd.Next(x, y);
        } // Give_Random_Number

        /// <summary>
        /// Jesli potrzeba mozna zmienic w kodzie zakres defult 1-5
        /// </summary>
        /// <returns>liczba od 1 do 5</returns>
        public static int Give_Koszt_Ruchu()
        {
            Random rnd = new();
            return rnd.Next(1, 5);
        } // Give_Koszt_Ruchu

        public static bool[,] Enter_false_visted(int row, int col)
        {
            bool[,] visited_node = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    visited_node[i, j] = false;
                    //Console.WriteLine(visited_node[i, j]);
                }
            }
            //Console.WriteLine();
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

        public static void Make_Cost_Node(int row, int col)
        {   
            for (int i = 0; i < row; i++)
            {
                koszt_Ruchu.Add(new List<Koszt_Ruchu>());
                for (int j = 0; j < col; j++)
                {
                    #region koszt ruchu
                    //dla node w punkcie zerowym
                    if (i == 0 && j == 0)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() }
                        });
                    }
                    // dla rzedow w kolumnie 0
                    else if (j == 0 && i > 0 && i < row - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() }
                        });
                    }
                    // dla col w rzedzie 0
                    else if (j > 0 && i == 0 && j < col - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() }
                        });
                    }
                    // dla srodkowych pol
                    else if (j > 0 && i > 0 && i < row - 1 && j < col - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() },
                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] }

                        });
                    }
                    // dla prawy gorny rog
                    else if (i == 0 && j == col - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() },
                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] }

                        });
                    }
                    // dla konca kolumny srodkowy przedzial 
                    else if (i > 0 && i < row - 1 && j == col - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },
                            Node_South = new int[] { i + 1, j, Give_Koszt_Ruchu() },
                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] }

                        });
                    }
                    //lewy dolny rog
                    else if (j == 0 && i == row - 1)
                    {
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            //Node_West = new int[] { koszt_Ruchu[j - 1].Node_South[0], koszt_Ruchu[j - 1].Node_South[1], koszt_Ruchu[j - 1].Node_South[2] }

                        });
                    }
                    //dolna krawedz srodek
                    // problem null
                    else if (j > 0 && i == row - 1 && j < col - 1)
                    {
                        //Console.WriteLine("row "+koszt_Ruchu[i - 1][j].Node_1[0] +" col "+ koszt_Ruchu[i - 1][j].Node_1[1] +" Kosz to " + koszt_Ruchu[i - 1][j].Node_South[2]);
                        //Console.WriteLine("row "+ koszt_Ruchu[i][j - 1].Node_1[0] + " col "+ koszt_Ruchu[i][j - 1].Node_1[1] + " Kosz to " + koszt_Ruchu[i][j - 1].Node_East[2]);
                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },
                            Node_East = new int[] { i, j + 1, Give_Koszt_Ruchu() },
                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] }

                        });
                    }
                    // prawy dolny rog
                    else if (j == col - 1 && i == row - 1)
                    {
                        //Console.WriteLine("row " + koszt_Ruchu[i - 1][j].Node_1[0] + " col " + koszt_Ruchu[i - 1][j].Node_1[1] + " Kosz to " + koszt_Ruchu[i - 1][j].Node_South[2]);
                        //Console.WriteLine("row " + koszt_Ruchu[i][j - 1].Node_1[0] + " col " + koszt_Ruchu[i][j - 1].Node_1[1] + " Kosz to " + koszt_Ruchu[i][j - 1].Node_East[2]);

                        koszt_Ruchu[i].Add(item: new Koszt_Ruchu
                        {
                            Node_1 = new int[] { i, j },
                            Node_North = new int[] { koszt_Ruchu[i - 1][j].Node_1[0], koszt_Ruchu[i - 1][j].Node_1[1], koszt_Ruchu[i - 1][j].Node_South[2] },

                            Node_West = new int[] { koszt_Ruchu[i][j - 1].Node_1[0], koszt_Ruchu[i][j - 1].Node_1[1], koszt_Ruchu[i][j - 1].Node_East[2] }

                        });

                    }


                    
                    #endregion
                }
            }
        }
        
        public static int[,] Enter_value_to_grid_Menu(int row, int col)
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

        /// <summary>
        /// wyswietlenie tablicy. Pamietaj o zakonczeniu skrytpu o ustaweniu search_end na false
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="start_node"></param>
        /// <param name="end_node"></param>
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

                    if (grid[i, j] == 6)
                    {
                        
                    }

                    else if (Program.visted_node[i, j] == true && i == end_node[0] && j == end_node[1])
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
                    if (grid[i, j] == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(grid[i, j]);
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 2)
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
        /// wyswietlenie tablicy. Pamietaj o zakonczeniu skrytpu o ustaweniu search_end na false
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="start_node"></param>
        /// <param name="end_node"></param>
        public static void write_out_koszt(int[,] grid, int row, int col, int[] start_node, int[] end_node)
        {
            int count = 0;
            for (int i = 0; i < row; i++)
            {
                int kolumna_z_petli = 0;
                for (int j = 0; j < col; j++)
                {

                    if (i == start_node[0] && j == start_node[1])
                    {
                        grid[i, j] = 1;
                    }
                    if (i == end_node[0] && j == end_node[1])
                    {
                        grid[i, j] = 4;
                    }

                    if (grid[i, j] == 6)
                    {

                    }

                    else if (Program.visted_node[i, j] == true && i == end_node[0] && j == end_node[1])
                    {
                        grid[i, j] = 5;
                        search_end = true;
                    }
                    else if (Program.arrival_node[i, j] && grid[i, j] != 1)
                    {
                        grid[i, j] = 3;

                    }
                    else if (Program.visted_node[i, j] == true && grid[i, j] != 1 && grid[i, j] != 3 && !Program.arrival_node[i, j])
                    {
                        grid[i, j] = 2;
                    }

                    if (j != col-1)
                    {
                        #region COLOR
                        switch (grid[i, j])
                        {
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Red;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Green;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            default:
                                //Console.Write(grid[i, j]);
                                break;
                        }
                        #endregion
                        Console.Write($"{grid[i, j]} ");
                        Console.ResetColor();
                        Console.Write($"<--{koszt_Ruchu[i][j].Node_East[2]}--> ");
                    }
                    
                    if (j == col -1)
                    {
                        #region COLOR
                        switch (grid[i, j])
                        {
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Red;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Green;
                                //Console.Write(grid[i, j]);
                                //Console.ResetColor();
                                break;
                            default:
                                //Console.Write(grid[i, j]);
                                break;
                        }
                        #endregion
                        Console.Write($"{grid[i, j]} ");
                        Console.ResetColor();
                    }
                    count++;
                    kolumna_z_petli = j;

                }
                Console.WriteLine();
                if (i != row - 1)
                {
                    for (int ii = 0; ii < 5; ii++)
                    {
                        switch (ii)
                        {
                            case 0:
                                {
                                    for (int jj = 0; jj < col; jj++)
                                    {
                                        Console.Write("|         ");
                                    }

                                    break;
                                }

                            case 1:
                                {
                                    for (int jj = 0; jj < col; jj++)
                                    {
                                        Console.Write($"|         ");
                                    }

                                    break;
                                }

                            case 2:
                                {
                                    for (int jj = 0; jj < col; jj++)
                                    {
                                        Console.Write($"{koszt_Ruchu[i][jj].Node_South[2]}         ");
                                    }

                                    break;
                                }

                            case 3:
                                {
                                    for (int jj = 0; jj < col; jj++)
                                    {
                                        Console.Write($"|         ");
                                    }

                                    break;
                                }

                            case 4:
                                {
                                    for (int jj = 0; jj < col; jj++)
                                    {
                                        Console.Write($"|         ");
                                    }

                                    break;
                                }
                        }
                        Console.WriteLine();
                    }
                }

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
                //Console.WriteLine();
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

        public static int[,] Wypisz_Najkrotsza_droge(int[,] grid, int[] start_node, Check_node node)
        {
            #region wypisz sciezke
            int uwu = 0;


            for (int i = 0; i <= Math.Abs(start_node[0] - node.Row); i++)
            {
                for (int ii = 0; ii <= Math.Abs(start_node[1] - node.Col); ii++)
                {
                    if (start_node[1] - node.Col > 0)
                    {
                        grid[node.Row, node.Col + ii] = 6;
                    }
                    else if (start_node[1] - node.Col < 0)
                    {
                        grid[node.Row, node.Col - ii] = 6;
                    }
                    else if (start_node[1] - node.Col == 0)
                    {
                        grid[node.Row, node.Col] = 6;
                    }
                    uwu = ii;
                }

                if (start_node[1] - node.Col > 0)
                {
                    if (start_node[0] - node.Row > 0)
                    {
                        grid[node.Row + i, node.Col + uwu] = 6;
                    }
                    else if (start_node[0] - node.Row < 0)
                    {
                        grid[node.Row - i, node.Col + uwu] = 6;
                    }
                    else if (start_node[0] - node.Col == 0)
                    {
                        grid[node.Row, node.Col] = 6;
                    }
                }
                else if (start_node[1] - node.Col < 0)
                {
                    if (start_node[0] - node.Row > 0)
                    {
                        grid[node.Row + i, node.Col - uwu] = 6;
                    }
                    else if (start_node[0] - node.Row < 0)
                    {
                        grid[node.Row - i, node.Col - uwu] = 6;
                    }
                    else if (start_node[0] - node.Col == 0)
                    {
                        grid[node.Row, node.Col] = 6;
                    }

                }
                else if (start_node[0] - node.Col == 0)
                {
                    grid[node.Row, node.Col] = 6;
                }
            }
            return grid;
            #endregion
        }

        public static bool Sprawdz_Czy_To_Koniec(int[,] grid, int[] start_node, int[] end_node, List<Check_node> list_cheked_node)
        {
            foreach (var node in list_cheked_node)
            {
                //sprawdz czy node przeszukany jest zgodny z nodem docelowym
                if (node.Row == end_node[0] && node.Col == end_node[1]) // powinno być grid[node.Row, node.Col] == 4 // ale w/e
                {
                    Console.WriteLine(node.Row + "," + node.Col + " Cel jest w tej pozycji ");

                    //Console.WriteLine("Row " + (start_node[0] - node.Row));
                    //Console.WriteLine("Col " + (start_node[1] - node.Col));
                    Console.WriteLine($"Suma ruchów {Math.Abs(start_node[1] - node.Col) + Math.Abs(start_node[0] - node.Row)}");

                    Wypisz_Najkrotsza_droge(grid, start_node, node);
                    return true;
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
            return false;
        }//Sprawdz_Czy_To_Koniec

    }//class
}