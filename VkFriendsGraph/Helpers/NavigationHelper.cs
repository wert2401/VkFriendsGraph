using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using VkFriendsGraph.Pages;

namespace VkFriendsGraph.ViewModels
{
    
    public static class NavigationHelper
    {
        private static Frame mainFrame = null;

        /// <summary>
        /// It can be initialized only once
        /// </summary>
        public static Frame MainFrame
        {
            get { return mainFrame; }
            set
            {
                if (mainFrame == null)
                {
                    mainFrame = value;
                }
            }
        }


        /// <summary>
        /// Helper for navigating to a different page
        /// </summary>
        /// <param name="page">Page to navigate</param>
        /// <param name="parameter">Additional params for pages (List<Person> for Friends page etc.)</param>
        public static void Navigate(Pages.Pages page, object parameter = null)
        {
            if (MainFrame == null) throw new NullReferenceException("Main frame is not defined");

            switch (page)
            {
                case Pages.Pages.SearchPage:
                    MainFrame.Content = new SearchPage();
                    break;
                case Pages.Pages.FriendsPage:
                    MainFrame.Content = new FriendsPage(parameter);
                    break;
                default:
                    break;
            }
        }
    }
}
