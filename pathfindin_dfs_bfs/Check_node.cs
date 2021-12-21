using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pathfindin_dfs_bfs
{
    public class Check_node
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Visted_node { get; set; }
        public string Direction { get; set; }
        public int Koszt { get; set; }


    }

    public static class Helper_driectrion
    {
        public const string UP = "UP";
        public const string DOWN = "DOWN";
        public const string LEFT = "LEFT";
        public const string RIGHT = "RIGHT";
    }
}
