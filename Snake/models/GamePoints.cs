using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Snake.models
{
    class GamePoints
    {
        public List<Point> PointPositions;
        private Random Rand;
        public int Score { get; set; }
        public int Size { get; }
        public Brush Color { get; }
        
        public GamePoints()
        {
            PointPositions = new List<Point>();
            Rand = new Random();
            Score = 0;
            Size = 8;
            Color = Brushes.Red;

            GenerateInitialPoints();
        }

        private void GenerateInitialPoints()
        {
            for (int i = 0; i < 10; i++)
            {
                Point bonusPoint = new Point(Rand.Next(5, 520), Rand.Next(5, 345));
                PointPositions.Insert(i, bonusPoint);
            }
        }
    }
}
