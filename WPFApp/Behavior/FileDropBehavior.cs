using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleFileClipper.WPFApp;
public class FileDropBehavior : Behavior<FrameworkElement>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(FileDropBehavior),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.AllowDrop = true;
        AssociatedObject.Drop += AssociatedObject_Drop;
        AssociatedObject.DragEnter += AssociatedObject_DragEnter;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.Drop -= AssociatedObject_Drop;
        AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
    }

    private void AssociatedObject_Drop(object? sender, DragEventArgs e)
    {
        if (Command != null && e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                Command.Execute(files);
                e.Handled = true; // イベントを処理済みに設定
            }
        }
    }
    private void AssociatedObject_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        } else {
            e.Effects = DragDropEffects.None;
            e.Handled = false;
        }
    }
}