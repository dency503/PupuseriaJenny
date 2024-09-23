using System.Windows.Controls;

namespace InventiSys.CLS
{
    public static class NavigationManager
    {
        private static Stack<UserControl> _history = new Stack<UserControl>();

        public static void Navigate(UserControl newControl, ContentControl parent)
        {
            if (parent.Content != null)
            {
                _history.Push(parent.Content as UserControl);
            }
            parent.Content = newControl;
        }

        public static void GoBack(ContentControl parent)
        {
            if (_history.Count > 0)
            {
                parent.Content = _history.Pop();
            }
        }
    }

}
