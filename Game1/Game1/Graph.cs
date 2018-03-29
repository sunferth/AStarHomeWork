using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Graph
    {
        Node[,] mainGraph;
        int[] start;
        int[] goal;
        public Graph(int width, int height, Texture2D text)
        {
            start = new int[2];
            goal = new int[2];
            mainGraph = new Node[width, height];
            for(int i = 0; i<width; i++)
            {
                for(int j = 0; j<height; j++)
                {
                    mainGraph[i, j] = new Node(i * 24, j * 24, text);
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach(Node n in mainGraph)
            {
                n.Draw(sb);
            }
        }
        public int[] Start
        {
            get { return start; }
            set
            {
                start = value;
                mainGraph[start[0], start[1]].Type = "Start";
            }
        }
        public int[] Goal
        {
            get { return goal; }
            set
            {
                goal = value;
                mainGraph[goal[0], goal[1]].Type = "Goal";
            }
        }

       // public Tile[] Neighbors(int[] position, bool DiagonalsAllowed)
       // {
       //     if(DiagonalsAllowed)
       //     {
//
       //     }
      //  }
    }
}
