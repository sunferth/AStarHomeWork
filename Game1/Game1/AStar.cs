using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AStar
    {
        //fields
        bool diagonalsAllowed;
        bool diagonalsCostMore;
        String heuristic;
        Node current;
        Graph graph;
        PriorityQueue OpenQueue;
        List<Node> ClosedList;
        //create a new pathfinding with a graph and heuristic
        public AStar(Graph graph, String Heuristic)
        {
            this.graph = graph;
            diagonalsAllowed = true;
            diagonalsCostMore = true;
            current = graph.Start;
            current.StartCost = 0;
            current.CalcH(Heuristic,graph.Goal);
            OpenQueue = new PriorityQueue();
            OpenQueue.Enqueue(current);
            ClosedList = new List<Node>();
            heuristic = Heuristic;

        }
        //run one iteration return true if no path found or goal found
        public bool OneIteration()
        {
            if(OpenQueue.InList(graph.Goal))
            {
                return true;
            }
            current = OpenQueue.Peek();
            ClosedList.Add(OpenQueue.Dequeue());
            current.Type = "Closed";
            List<Node> temp = graph.GetNeighbors(current, diagonalsAllowed);
            foreach(Node n in temp)
            {
                if(n.Type == "Obstacle")
                {

                }
                else if(OpenQueue.InList(n))
                {
                    if (n.StartCost > n.CalcG(heuristic, current, diagonalsCostMore))
                    {
                        n.CalcG(heuristic, current, diagonalsAllowed);
                        n.Path = current;
                        
                    }
                }
                else if (ClosedList.Contains(n) && n.CalcG("Manhattan",current,diagonalsCostMore)<n.StartCost)
                {
                    ClosedList.Remove(n);
                }
                else if(!ClosedList.Contains(n) && !OpenQueue.InList(n))
                {
                    n.StartCost = n.CalcG(heuristic, current, diagonalsCostMore);
                    n.HCost = n.CalcH(heuristic, graph.Goal);
                    n.Type = "Checked";
                    n.Path = current;
                    OpenQueue.Enqueue(n);
                }
                
            }
            if(OpenQueue.IsEmpty())
            {
                return true;
            }
            return false;

        }
       //return all nodes on the path
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
        //run all iterations until finished
        public void Run()
        {
            while(!OneIteration())
            {
                
            }
        }
    }
}
