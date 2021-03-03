using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VkFriendsGraph.Helpers
{
    public enum MoveDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class MovingHelper
    {
        private static Grid grid = null;

        public static Grid Grid
        {
            get { return grid; }
            set
            {
                if (grid == null)
                {
                    grid = value;
                }
            }
        }

        public static void Move(MoveDirection direction)
        {
            if (grid == null)
            {
                return;
            }

            Thickness t = Grid.Margin;

            switch (direction)
            {
                case MoveDirection.Left:
                    Grid.Margin = new Thickness(t.Left + 10, t.Top, 0, 0);
                    break;
                case MoveDirection.Right:
                    Grid.Margin = new Thickness(t.Left - 10, t.Top, 0, 0);
                    break;
                case MoveDirection.Up:
                    Grid.Margin = new Thickness(t.Left, t.Top + 10, 0, 0);
                    break;
                case MoveDirection.Down:
                    Grid.Margin = new Thickness(t.Left, t.Top - 10, 0, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
