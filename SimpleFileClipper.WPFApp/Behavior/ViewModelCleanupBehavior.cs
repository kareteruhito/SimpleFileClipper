
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace SimpleFileClipper.WPFApp;

public class ViewModelCleanupBehavior : Behavior<Window>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Closed += this.WindowClosed;
    }

    private void WindowClosed(object? sender, EventArgs e)
    {
        (this.AssociatedObject.DataContext as IDisposable)?.Dispose();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.Closed -= this.WindowClosed;
    }
}
