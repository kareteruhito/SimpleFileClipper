using System.Diagnostics;
using System;
using System.ComponentModel;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace SimpleFileClipper.WPFApp;

public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
{
#region
    // INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    // IDisposable
    private CompositeDisposable Disposable { get; } = [];
    public void Dispose() => Disposable.Dispose();
#endregion
    public ReactiveProperty<string> Title { get; private set; }
    public MainWindowViewModel()
    {
        PropertyChanged += (o, e) => {};
        string className = SimpleFileClipper.Lib.Class1.CLASS_NAME;
        Title = new ReactiveProperty<string>(className).AddTo(this.Disposable);
    }
}
