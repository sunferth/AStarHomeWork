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
        public Graph(int width, int height, Texture2D text)
        {
            this.width = width;
            this.height = height;
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
        public Node Start
        {
            get { return start; }
            set
            {
                start = value;
                start.Type = "Start";
            }
        }
        public Node Goal
        {
            get { return goal; }
            set
            {
                goal = value;
                goal.Type = "Goal";
            }
        }

        public Node[,] allTiles
        {
            get
            {
                return mainGraph;
            }
        }

        public List<Node> GetNeighbors(Node current, bool diagonalsAllowed)
        {
            List<Node> allNeighbors = new List<Node>();
            if(diagonalsAllowed)
            {
                if(0<= current.X-1 && current.X-1 <= width && 0<= current.Y && current.Y <= height)
                    allNeighbors.Add(mainGraph[current.X - 1, current.Y]);
                if (0 <= current.X+1 && current.X+1 <= width && 0 <= current.Y && current.Y <= height)
                    allNeighbors.Add(mainGraph[current.X + 1, current.Y]);
                if (0 <= current.X && current.X <= width && 0 <= current.Y+1 && current.Y+1 <= height)
                    allNeighbors.Add(mainGraph[current.X, current.Y + 1]);
                if (0 <= current.X && current.X <= width && 0 <= current.Y-1 && current.Y-1 <= height)
                    allNeighbors.Add(mainGraph[current.X, current.Y - 1]);
                if (0 <= current.X-1 && current.X-1 <= width && 0 <= current.Y-1 && current.Y-1 <= height)
                    allNeighbors.Add(mainGraph[current.X - 1, current.Y -1]);
                if (0 <= current.X+1 && current.X+1 <= width && 0 <= current.Y+1 && current.Y+1 <= height)
                    allNeighbors.Add(mainGraph[current.X + 1, current.Y + 1]);
                if (0 <= current.X-1 && current.X-1 <= width && 0 <= current.Y+1 && current.Y+1 <= height)
                    allNeighbors.Add(mainGraph[current.X - 1, current.Y + 1]);
                if (0 <= current.X+1 && current.X+1 <= width && 0 <= current.Y-1 && current.Y-1 <= height)
                    allNeighbors.Add(mainGraph[current.X + 1, current.Y - 1]);
            }
            else
            {
                if (0 <= current.X - 1 && current.X - 1 <= width && 0 <= current.Y && current.Y <= height)
                    allNeighbors.Add(mainGraph[current.X - 1, current.Y]);
                if (0 <= current.X + 1 && current.X + 1 <= width && 0 <= current.Y && current.Y <= height)
                    allNeighbors.Add(mainGraph[current.X + 1, current.Y]);
                if (0 <= current.X && current.X <= width && 0 <= current.Y + 1 && current.Y + 1 <= height)
                    allNeighbors.Add(mainGraph[current.X, current.Y + 1]);
                if (0 <= current.X && current.X <= width && 0 <= current.Y - 1 && current.Y - 1 <= height)
                    allNeighbors.Add(mainGraph[current.X, current.Y - 1]);
            }
            
            return allNeighbors;
        }
    }
}
