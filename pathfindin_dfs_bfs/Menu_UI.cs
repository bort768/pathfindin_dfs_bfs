using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Menu_UI
    {
        private int[,] grid;
        private int row;

        public int[,] Grid { get { return grid; } set { } }
        public int Row { get { return row; } set { } }
        public Menu_UI(int[,] e_grid, int e_row, int e_col, int[] e_start_node, int[] e_end_node)
        {
            Grid = e_grid;
            Row = e_row;
        }
        public static void Wyswietl_Menu() 
        { 
            
        }

    }
}
