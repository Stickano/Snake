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
using Snake.models;

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
        private GamePoints points;

        public MainWindow()
        {
            InitializeComponent();

            snake = new models.Snake();
            points = new GamePoints();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTIck;
            timer.Interval = snake.Speed;
            timer.Start();

            KeyDown += OnButtonKeyDown;
            PrintSnake();
            PrintPoints();
        }

        /// <summary>
        /// Prints the snake to the canvas
        /// </summary>
        private void PrintSnake()
        {
            // Create the snake body (Ellipse) and give intial values from snake obj
            Ellipse snakeBody = new Ellipse();
            snakeBody.Fill = snake.color;
            snakeBody.Width = snake.BodySize;
            snakeBody.Height = snake.BodySize;

            // Place the obj on canvas
            Canvas.SetTop(snakeBody, snake.CurrentPosition.Y);
            Canvas.SetLeft(snakeBody, snake.CurrentPosition.X);

            int count = PaintCanvas.Children.Count;
            PaintCanvas.Children.Add(snakeBody);
            snake.BodyPoints.Add(snake.CurrentPosition);

            // Remove snake body from canvas and the snake body (list)
            if (count > snake.Length)
            {
                PaintCanvas.Children.RemoveAt(count - snake.Length -1);
                snake.BodyPoints.RemoveAt(count - snake.Length);
            }
        }

        private void PrintPoints()
        {
            int br = 0;
            foreach (Point point in points.PointPositions)
            {
                Ellipse bonusPoint = new Ellipse();
                bonusPoint.Fill = points.Color;
                bonusPoint.Height = points.Size;
                bonusPoint.Width = points.Size;

                Canvas.SetTop(bonusPoint, point.Y);
                Canvas.SetLeft(bonusPoint, point.X);

                PaintCanvas.Children.Insert(br, bonusPoint);
                br++;
            }
        }

        /// <summary>
        /// The Tick of the DispatchTimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTIck(object sender, EventArgs e)
        {
            // Checks which direction we are going, and changes the current position accordingly (move one point)
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

            // Check if collision with body
            for (int i = 0; i < (snake.BodyPoints.Count - snake.BodySize*2); i++)
            {
                Point point = new Point(snake.BodyPoints[i].X, snake.BodyPoints[i].Y);
                if (Math.Abs(point.X - snake.CurrentPosition.X) < snake.BodySize &&
                    Math.Abs(point.Y - snake.CurrentPosition.Y) < snake.BodySize)
                {
                    EndGame();
                    break;
                }
            }
        }

        /// <summary>
        /// Will change the direction of the snake w/ keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Method to quit the game / Game over
        /// </summary>
        private void EndGame()
        {
            MessageBox.Show("The game has ended. Your score: ","Game Over!",MessageBoxButton.OK);
            Close();
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
