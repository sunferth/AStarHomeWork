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
        Node start;
        Node goal;
        int width;
        int height;
        public Graph(SpriteFont font,int width, int height, Texture2D text)
        {
            this.width = width;
            this.height = height;
            mainGraph = new Node[width, height];
            for(int i = 0; i<width; i++)
            {
                for(int j = 0; j<height; j++)
                {
                    mainGraph[i, j] = new Node(font,i * 64, j * 64, text);
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
        public Node Start
        {
            get { return start; }
            set
            {
                if (start != null)
                {
                    start.Type = "Normal";
                    start.StartCost = 626;
                }
                start = value;
                start.Type = "Start";
                start.StartCost = 0;
            }
        }
        public Node Goal
        {
            get { return goal; }
            set
            {
                if(goal != null)
                    goal.Type = "Normal";
                goal = value;
                goal.Type = "Goal";
            }
        }

        public Node[,] AllTiles
        {
            get
            {
                return mainGraph;
            }
        }

        public List<Node> GetNeighbors(Node current, bool diagonalsAllowed)
        {
            List<Node> allNeighbors = new List<Node>();
            for(int i = current.X-1; i<=current.X+1; i++)
            {
                for(int j = current.Y-1; j<=current.Y+1; j++)
                {
                    if(i>= 0 && i<mainGraph.GetLength(0) && j>= 0 && j<mainGraph.GetLength(1) && (mainGraph[i,j] != current))
                    {
                        if(diagonalsAllowed)
                        {
                            allNeighbors.Add(mainGraph[i, j]);
                        }
                        else if(i==current.X || j == current.Y)
                        {
                            allNeighbors.Add(mainGraph[i, j]);
                        }
                    }
                }
            }
            
            
            
            return allNeighbors;
        }
    }
}
