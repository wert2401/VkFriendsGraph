using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VkFriendsGraph.Helpers
{
    public enum MoveDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum Zooming
    {
        ZoomIn,
        ZoomOut
    }

    public static class MovingHelper
    {
        private static Canvas canvas = null;
        private static ScaleTransform scale = null;
        private static int movingStep = 30;
        private static double scaleStep = 0.1;

        public static Canvas Canvas
        {
            get { return canvas; }
            set
            {
                if (canvas == null)
                {
                    canvas = value;
                }
            }
        }

        public static void Move(MoveDirection direction)
        {
            if (canvas == null)
            {
                return;
            }

            Thickness t = Canvas.Margin;

            switch (direction)
            {
                case MoveDirection.Left:
                    canvas.Margin = new Thickness(t.Left + movingStep, t.Top, t.Right - movingStep, t.Bottom);
                    canvas.Width += movingStep;
                    break;
                case MoveDirection.Right:
                    canvas.Margin = new Thickness(t.Left - movingStep, t.Top, t.Right + movingStep, t.Bottom);
                    canvas.Width -= movingStep;
                    break;
                case MoveDirection.Up:
                    canvas.Margin = new Thickness(t.Left, t.Top + movingStep, t.Right, t.Bottom - movingStep);
                    canvas.Height += movingStep;
                    break;
                case MoveDirection.Down:
                    canvas.Margin = new Thickness(t.Left, t.Top - movingStep, t.Right, t.Bottom + movingStep);
                    canvas.Height -= movingStep;
                    break;
                default:
                    break;
            }
        }

        public static void Zoom(Zooming zooming)
        {
            if (canvas == null)
            {
                return;
            }

            if (scale == null)
            {
                scale = new ScaleTransform();
                scale.ScaleX = 1;
                scale.ScaleY = 1;
            }

            scale.CenterX = canvas.ActualWidth / 2;
            scale.CenterY = canvas.ActualHeight / 2;

            switch (zooming)
            {
                case Zooming.ZoomIn:
                    scale.ScaleX += scaleStep;
                    scale.ScaleY += scaleStep;
                    break;
                case Zooming.ZoomOut:
                    scale.ScaleX -= scaleStep;
                    scale.ScaleY -= scaleStep;
                    break;
                default:
                    break;
            }

            canvas.RenderTransform = scale;
        }

    }
}
