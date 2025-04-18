using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive.Disposables;
using Reactive.Bindings;

namespace SimpleFileClipper.Lib;

public class FileRecordModel : INotifyPropertyChanged, IDisposable
{
#region 
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    private CompositeDisposable Disposable { get; } = new ();
    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose() => this.Disposable.Dispose();
#endregion
     public string Path { get; set; } = "";
     public ReactiveProperty<string> Memo { get; set; } = new("");

     public string Name
     {
          get
          {
               return System.IO.Path.GetFileName(Path);
          }
     }
     public string Location
     {
          get
          {
               return System.IO.Path.GetDirectoryName(Path) ?? "";
          }
     }
     public bool IsFile { get; set; } = false;
     public string IconString
     {
          get
          {
               string dirString =  "M 10,80 L 10,240 L 240,240 L 240,80 L 100,80 L 80,60  L 30,60 Z";
               string fileString =  "M 200,60 L 180,60 L 180,30 Z M 80,20 L 80,240 L 210,240 L 210,50 L 180,20 Z";

               return IsFile ? fileString : dirString;
          }
     }
}