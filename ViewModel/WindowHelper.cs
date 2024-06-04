using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModels;

public static class WindowHelper
{
    public static void ChangeWindow(Window currentWindow, Window newWindow)
    {
        try
        {
            // Close the current window
            currentWindow?.Close();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur while closing the window
            Console.WriteLine($"Error closing window: {ex.Message}");
        }

        try
        {
            // Show the new window
            newWindow.Show();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur while showing the new window
            Console.WriteLine($"Error showing new window: {ex.Message}");
        }
    }
}
