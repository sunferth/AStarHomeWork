using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class Node
    {
        //fields
        String type;
        Texture2D tileText;
        Rectangle location;
        double distanceFromStart = 626;
        double HeuristicCost = 626;
        Node path;
        SpriteFont font;
        int x;
        int y;

        //Creates a new node, with font, x and y location on screen, texture, and type normal
        public Node(SpriteFont font, int x, int y, Texture2D text, String type = "Normal")
        {
            this.font = font;
            location = new Rectangle(x, y, 64, 64);
            this.x = (int)Math.Floor((double)x / 64);
            this.y = (int)Math.Floor((double)y / 64);
            tileText = text;
            this.type = type;
        }

        //Draw with type color, and left being start cost, right being h cost, and bot left being total cost
        public void Draw(SpriteBatch sb)
        {
            
            switch (type)
            {

                case "Start":
                    {
                        sb.Draw(tileText, location, Color.Green);
                        break;
                    }
                case "Goal":
                    {
                        sb.Draw(tileText, location, Color.Red);
                        break;
                    }
                case "Path":
                    {
                        sb.Draw(tileText, location, Color.Blue);
                        break;
                    }
                case "Checked":
                    {
                        sb.Draw(tileText, location, Color.LightSeaGreen);
                        break;
                    }
                case "Obstacle":
                    {
                        sb.Draw(tileText, location, Color.Gray);
                        break;
                    }
                case "Closed":
                    {
                        sb.Draw(tileText, location, Color.Brown);
                        break;
                    }
                default:
                    {
                        sb.Draw(tileText, location, Color.White);
                        break;
                    }
            }
            if(distanceFromStart != 626)
                sb.DrawString(font, Math.Round(distanceFromStart,2).ToString(), new Vector2(x * 64 + 5, y * 64 + 5), Color.Blue);
            if (HeuristicCost != 626)
                sb.DrawString(font, Math.Round(HeuristicCost,2).ToString(), new Vector2(x * 64 + 64 - 32, y * 64 + 5), Color.Purple);
            if (TotalCost != 1252 && TotalCost != 626)
                sb.DrawString(font, Math.Round(TotalCost,2).ToString(), new Vector2(x * 64 + 5, y * 64 + 64 - 24), Color.Brown);
        }
        //calc start cost
        public float CalcG(String Heuristic, Node prev, bool DiagonalsCostMore)
         {
            if (Heuristic == "Manhattan")
            {
                if(DiagonalsCostMore)
                    return (float)(Math.Sqrt(((prev.X - x)*(prev.X - x) + (prev.Y - y)*(prev.Y-y))) + prev.StartCost);
                else
                    return (float)(prev.StartCost)+1;
            }
            return 1000;
        }
        //calc h cost
        public float CalcH(String Heuristic, Node goal)
        {
            if (Heuristic == "Manhattan")
            {
                return Math.Abs(goal.X - x) + Math.Abs(goal.Y - y);
            }
            return 1000;
        }
        //set type
        public String Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        //return location
        public Rectangle Rect
        {
            get
            {
                return location;
            }
        }
        //return gcost + hcost
        public double TotalCost
        {
            get
            {
                return HeuristicCost + distanceFromStart;
            }
        }
        //gcost
        public double StartCost
        {
            get
            {
                return distanceFromStart;
            }
            set
            {
                distanceFromStart = value;
            }
        }
        //hcost
        public double HCost
        {
            get
            {
                return HeuristicCost;
            }
            set
            {
                HeuristicCost = value;
            }
        }
        //prev node
        public Node Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        //x
        public int X
        {
            get { return x; }
        }
        //y
        public int Y
        {
            get { return y; }
        }


    }
}
