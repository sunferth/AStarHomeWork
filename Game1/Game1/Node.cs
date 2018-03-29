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
        int x;
        int y;
        double distanceFromStart;
        double HeuristicCost;
        Node path;


        public Node(int x, int y, Texture2D text, String type = "Normal")
        {
            
            location = new Rectangle(x, y, 24, 24);
            x = (int)Math.Floor((double)x / 24);
            y = (int)Math.Floor((double)y / 24);
            tileText = text;
            this.type = type;
        }


        public void Draw(SpriteBatch sb)
        {
            switch(type)
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
                default:
                    {
                        sb.Draw(tileText, location, Color.White);
                        break;
                    }
            }
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

        public Rectangle rect
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
