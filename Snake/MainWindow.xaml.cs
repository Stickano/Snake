using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Snake.Annotations;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        public enum Directions
        {
            Up = 8,
            Down = 2,
            Left = 4,
            Right = 6
        }

        private models.Snake snake;
        
        public MainWindow()
        {
            InitializeComponent();

            snake = new models.Snake();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTIck;
            timer.Interval = snake.Speed;
            timer.Start();

            KeyDown += OnButtonKeyDown;
            PrintSnake();
        }

        /// <summary>
        /// Prints the snake to the canvas
        /// </summary>
        private void PrintSnake()
        {

            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Green;
            newEllipse.Width = 8;
            newEllipse.Height = 8;

            Canvas.SetTop(newEllipse, snake.CurrentPosition.Y);
            Canvas.SetLeft(newEllipse, snake.CurrentPosition.X);

            int count = PaintCanvas.Children.Count;
            PaintCanvas.Children.Add(newEllipse);
            snake.BodyPoints.Add(snake.CurrentPosition);

            //Canvas.SetTop(snake.PaintSnake(), snake.CurrentPosition.Y);
            //Canvas.SetLeft(snake.PaintSnake(), snake.CurrentPosition.X);

            //int count = PaintCanvas.Children.Count;
            //PaintCanvas.Children.Add(snake.PaintSnake());
            //snake.BodyPoints.Add(snake.CurrentPosition);

            // Place/Remove snake body from canvas
            if (count > snake.Length)
            {
                PaintCanvas.Children.RemoveAt(count - snake.Length + 9);
                snake.BodyPoints.RemoveAt(count - snake.Length);
            }
        }

        /// <summary>
        /// The Tick of the DispatchTimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTIck(object sender, EventArgs e)
        {
            switch (snake.Direction)
            {
                case (int)Directions.Up:
                    snake.CurrentPosition.Y -= 1;
                    PrintSnake();
                    break;
                case (int)Directions.Down:
                    snake.CurrentPosition.Y += 1;
                    PrintSnake();
                    break;
                case (int)Directions.Left:
                    snake.CurrentPosition.X -= 1;
                    PrintSnake();
                    break;
                case (int)Directions.Right:
                    snake.CurrentPosition.X += 1;
                    PrintSnake();
                    break;
            }

            // Add Points to the snake body (list)
            for (int i = 0; i < (snake.BodyPoints.Count - 8*2); i++)
            {
                Point point = new Point();
            }
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (snake.Direction != (int)Directions.Up)
                        snake.Direction = (int)Directions.Down;
                    break;
                case Key.Up:
                    if (snake.Direction != (int)Directions.Down)
                        snake.Direction = (int)Directions.Up;
                    break;
                case Key.Left:
                    if (snake.Direction != (int)Directions.Right)
                        snake.Direction = (int)Directions.Left;
                    break;
                case Key.Right:
                    if (snake.Direction != (int)Directions.Left)
                        snake.Direction = (int)Directions.Right;
                    break;
            }
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
