using System.ComponentModel;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

using SimpleFileClipper.Lib;
using Microsoft.EntityFrameworkCore.Update.Internal;

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
    public ReactiveProperty<string> Title { get; private set; } = new ("FileClipper");

    FileRecordRepository _db = new();

    public ReactiveCommand<string[]> DropCommand { get; }
    public ReactiveCollection<FileRecord> FileRecords { get; set; } = [];
    public MainWindowViewModel()
    {
        Title.AddTo(this.Disposable);
        _db.AddTo(this.Disposable);

        DropCommand = new ReactiveCommand<string[]>().WithSubscribe(
            async files=>{
                string path = files[0];
                await _db.UpsertFileRecord(new FileRecord(){Path=path, Memo=""});
                Update();

            }).AddTo(this.Disposable);
        
        Update();
    }
    public async void Update()
    {
        var task = _db.FindRecord("");
        
        if (0 < FileRecords.Count)
        {
            var tcs = new TaskCompletionSource<bool>();
            var v = FileRecords.ObserveResetChanged<int>().Subscribe(x=>
            {
                tcs.SetResult(true);
            });
            FileRecords.ClearOnScheduler();
            await tcs.Task;
            v.Dispose();
        }
        var files = await task;

        foreach(var file in files)
        {
            FileRecords.AddOnScheduler(file);
        }
    }
}
