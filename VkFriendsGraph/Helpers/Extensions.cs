using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using VkFriendsGraph.Pages;

namespace VkFriendsGraph.Helpers
{
    public static class Extensions
    {
        public static Point GetPoint(this FriendNode fn)
        {
            return new Point((int)fn.Margin.Left, (int)fn.Margin.Top);
        }
    }
}
