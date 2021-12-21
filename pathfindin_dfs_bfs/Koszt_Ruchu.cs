using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Koszt_Ruchu
    {
        

        public int[] Node_1 { get; set; }

        /// <summary>
        /// index 0 to row, 1 to col, a 2 to koszt ruchu
        /// </summary>
        public int[] Node_North { get; set; }
        /// <summary>
        /// index 0 to row, 1 to col, a 2 to koszt ruchu
        /// </summary>
        public int[] Node_East { get; set; }
        /// <summary>
        /// index 0 to row, 1 to col, a 2 to koszt ruchu
        /// </summary>
        public int[] Node_South { get; set; }
        /// <summary>
        /// index 0 to row, 1 to col, a 2 to koszt ruchu
        /// </summary>
        public int[] Node_West { get; set; }

        public List<string> Daj_Liste_Z_Kosztami()
        {
            List<string> lista_do_wyplucia = new();
            if (Node_North != null)
            {
                lista_do_wyplucia.Add( $"koszt ruchu z {Node_1[0]},{Node_1[1]} do {Node_North[0]} {Node_North[1]} wynosi {Node_North[2]}  \n" ) ;
            }
            if (Node_East != null)
            {
                lista_do_wyplucia.Add($"koszt ruchu z {Node_1[0]},{Node_1[1]} do {Node_East[0]} {Node_East[1]} wynosi {Node_East[2]}  \n");
            }
            if (Node_South != null)
            {
                lista_do_wyplucia.Add($"koszt ruchu z {Node_1[0]},{Node_1[1]} do {Node_South[0]} {Node_South[1]} wynosi {Node_South[2]}  \n");
            }
            if (Node_West != null)
            {
                lista_do_wyplucia.Add($"koszt ruchu z {Node_1[0]},{Node_1[1]} do {Node_West[0]} {Node_West[1]} wynosi {Node_West[2]}  \n");
            }
            return lista_do_wyplucia;
                
                
            //return base.ToString(); 
        }

        public List<Node_koszt> Daj_Liste_Z_node(int[,] grid)
        {
            List<Node_koszt> lista_do_wyplucia = new();
            //Console.WriteLine("Node row " + Node_1[0] + " col: " + Node_1[1]);
            if (Node_North != null)
            {
                //Console.WriteLine("North row " + Node_North[0] + " col: " + Node_North[1]);
                if (grid[Node_North[0], Node_North[1]] != 3 && grid[Node_North[0], Node_North[1]] != 3)
                {
                    lista_do_wyplucia.Add(new Node_koszt {Row = Node_North[0], Col = Node_North[1], Koszt = Node_North[2], Kierunek = Helper_driectrion.UP });
                }              
            }
            if (Node_East != null)
            {
                //Console.WriteLine("East row "+Node_East[0] +" col: "+ Node_East[1]);
                if (grid[Node_East[0], Node_East[1]] != 3 && grid[Node_East[0], Node_East[1]] != 1)
                {
                    lista_do_wyplucia.Add(new Node_koszt { Row = Node_East[0], Col = Node_East[1], Koszt = Node_East[2], Kierunek = Helper_driectrion.RIGHT });
                }
            }
            if (Node_South != null)
            {
                //Console.WriteLine("South row " + Node_South[0] + " col: " + Node_South[1]);
                if (grid[Node_South[0], Node_South[1]] != 3 && grid[Node_South[0], Node_South[1]] != 1)
                {
                    lista_do_wyplucia.Add(new Node_koszt { Row = Node_South[0], Col = Node_South[1], Koszt = Node_South[2], Kierunek = Helper_driectrion.DOWN });
                }               
            }
            if (Node_West != null)
            {
                //Console.WriteLine("West row " + Node_West[0] + " col: " + Node_West[1]);
                if (grid[Node_West[0], Node_West[1]] != 3 && grid[Node_West[0], Node_West[1]] != 1)
                {
                    lista_do_wyplucia.Add(new Node_koszt { Row = Node_West[0], Col = Node_West[1], Koszt = Node_West[2], Kierunek = Helper_driectrion.LEFT });
                }             
            }
            return lista_do_wyplucia;

            //return base.ToString(); 
        }

    }
}
