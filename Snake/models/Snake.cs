using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Snake.Annotations;

namespace Snake.models
{
    class Snake : INotifyPropertyChanged
    {
        private int _direction;
        public List<Point> BodyPoints;
        public TimeSpan Speed { get; }
        public Brush color { get; }
        public int BodySize { get; }
        //public Point CurrentPosition { get; set; }
        public Point CurrentPosition;
        public int Length { get; set; }
        public int PreviousDirection { get; private set; }
        public int Direction
        {
            get { return _direction; }
            set
            {
                PreviousDirection = _direction;
                _direction = value;
            }
        }

        /// <summary>
        /// Constructor. Sets initial values.
        /// </summary>
        public Snake()
        {
            Speed = new TimeSpan(10000);
            CurrentPosition = new Point(100,100);
            color = Brushes.Green;
            BodySize = 8;
            Length = 100;
            Direction = 0;
            PreviousDirection = 0;
            BodyPoints = new List<Point>();
        }

        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
