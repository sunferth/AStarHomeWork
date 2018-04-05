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
        String type;
        Texture2D tileText;
        Rectangle location;
        double distanceFromStart = 626;
        double HeuristicCost = 626;
        Node path;
        SpriteFont font;
        int x;
        int y;


        public Node(SpriteFont font, int x, int y, Texture2D text, String type = "Normal")
        {
            this.font = font;
            location = new Rectangle(x, y, 64, 64);
            this.x = (int)Math.Floor((double)x / 64);
            this.y = (int)Math.Floor((double)y / 64);
            tileText = text;
            this.type = type;
        }


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
                sb.DrawString(font, distanceFromStart.ToString(), new Vector2(x * 64 + 5, y * 64 + 5), Color.Blue);
            if (HeuristicCost != 626)
                sb.DrawString(font, HeuristicCost.ToString(), new Vector2(x * 64 + 64 - 32, y * 64 + 5), Color.Purple);
            if (TotalCost != 1252 && TotalCost != 626)
                sb.DrawString(font, TotalCost.ToString(), new Vector2(x * 64 + 5, y * 64 + 64 - 24), Color.Brown);
        }
        public float CalcG(String Heuristic, Node prev, bool DiagonalsCostMore)
         {
            if (Heuristic == "Manhattan")
            {
                if(DiagonalsCostMore)
                    return (float)Math.Sqrt(((prev.X - x + prev.Y - y)^2) + prev.StartCost);
                else
                    return (float)(prev.StartCost)+1;
            }
            return 1000;
        }
        public float CalcH(String Heuristic, Node goal)
        {
            if (Heuristic == "Manhattan")
            {
                return Math.Abs(goal.X - x) + Math.Abs(goal.Y - y);
            }
            return 1000;
        }

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

        public Rectangle Rect
        {
            get
            {
                return location;
            }
        }

        public double TotalCost
        {
            get
            {
                return HeuristicCost + distanceFromStart;
            }
        }
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

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }


    }
}
