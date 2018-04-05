using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PriorityQueue
    {
        private List<Node> mainList;
        //create a new priority queue with an empty list
        public PriorityQueue()
        {
            mainList = new List<Node>();
        }
        //add a new elememt with data at the end and sort it correctly
        public void Enqueue(Node data)
        {
            int i = 0;
            while (true)
            {
                if (mainList.Count <= i)
                {
                    mainList.Add(data);
                    return;
                }
                if (mainList[i].TotalCost <= data.TotalCost)
                {
                    i++;
                }
                else
                {
                    mainList.Insert(i, data);
                    return;
                }
            }
            
        }
        //remove the first element and resort
        public Node Dequeue()
        {
           if(mainList.Count == 0)
            {
                return null;
            }
            Node temp = mainList[0];
            mainList = mainList.GetRange(1, mainList.Count - 1);
            return temp;
        }
        //return the first piece
        public Node Peek()
        {
            if(mainList.Count >= 1)
                return mainList[0];
            return null;
        }
        public bool InList(Node node)
        {
            for(int i  = 0; i<mainList.Count; i++)
            {
                if(mainList[i] == node)
                {
                    return true;
                }
            }
            return false;
        }
        public void Remove(Node node)
        {
            
        }
        //return if there is data in the list
        public bool IsEmpty()
        {
            return !(mainList.Count > 0);
        }
    }
}
