using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace KUtility.Utilities
{
    public static class WPFUtility
    {

        /// <summary>
        /// set height and width of a window for wpf apps
        /// </summary>
        /// <param name="window"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SetWindowSize(this Window window, double width, double height)
        {
            if (window == null) return;
            window.Height = height;
            window.Width = width;
        }

        /// <summary>
        /// Set window's size based on a percentage of height and a percentage of width to the screen size.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="widthRatio"></param>
        /// <param name="heightRatio"></param>
        public static void SetWindowSizeBasedOnScreen(this Window window, double widthRatio, double heightRatio)
        {
            var width = SystemParameters.PrimaryScreenWidth * widthRatio;
            var height = SystemParameters.PrimaryScreenHeight * heightRatio;
            window.SetWindowSize(width, height);
        }

        public static Point GetMousePosition()
        {
            System.Drawing.Point point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }

        /// <summary>
        /// Add items in a list to the observableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="colletion"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ObservableCollection<T> colletion, List<T> items)
        {
            foreach (var item in items)
            {
                colletion.Add(item);
            }
        }
    }
}
