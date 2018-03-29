using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AStar
    {
        bool diagonalsAllowed;
        bool diagonalsCostMore;
        String heuristic;
        Node current;
        Graph graph;
        PriorityQueue OpenQueue;
        List<Node> ClosedList;
        
        public AStar(Graph graph, String Heuristic)
        {
            this.graph = graph;
            diagonalsAllowed = true;
            diagonalsCostMore = false;
            current = graph.Start;
            current.StartCost = 0;
            OpenQueue = new PriorityQueue();
            OpenQueue.Enqueue(current);

        }

        public bool OneIteration()
        {
            if(OpenQueue.Peek() != graph.Goal)
            {
                current = OpenQueue.Dequeue();
                ClosedList.Add(current);
                List<Node> temp = graph.GetNeighbors(current, diagonalsAllowed);
                foreach(Node n in temp)
                {
                    double cost;
                    if (diagonalsAllowed && diagonalsCostMore)
                    {
                        if (n.X == current.X || n.Y == current.Y)
                        {
                            cost = current.StartCost + 1;
                        }
                        else
                        {
                            cost = current.StartCost + Math.Sqrt(2);
                        }
                    }
                    else
                    {
                        cost = current.StartCost + 1;
                    }

                    if(OpenQueue.InList(n) && cost<n.StartCost)
                    {
                        OpenQueue.Remove(n);
                    }
                    if(ClosedList.Contains(n) && cost < n.StartCost)
                    {
                        ClosedList.Remove(n);
                    }
                    if (!OpenQueue.InList(n) && !ClosedList.Contains(n))
                    {
                        n.StartCost = cost;
                        OpenQueue.Enqueue(n);
                        n.Path = current;
                    }
                }

                return false;
            }
            else
            {
                return true;
            }

        }
        public List<Node> GetPath()
        {
            List<Node> thePath = new List<Node>();
            Node temp;
            temp = graph.Goal;
            if(temp.Path != null)
            {
                while(temp.Path != graph.Start)
                {
                    thePath.Add(temp.Path);
                    temp = temp.Path;
                }
            }
            return null;
        }

        public void Run()
        {
            while(graph.Goal.Path == null)
            {
                OneIteration();
            }
        }
    }
}
