using Reactive.Bindings;
using SimpleFileClipper.Lib;

namespace SimpleFileClipper;

class Program
{
    public static async Task Main()
    {
        using var db = new FileRecordRepository();

        string path = @"C:\Users\karet\git\SimpleFileClipper\SimpleFileClipper.Lib\SimpleFileClipper.Lib.csproj";
        string path2 = @"C:\Users\karet\git\SimpleFileClipper\SimpleFileClipper.Lib\FileRecord.cs";

        await db.UpsertFileRecord(new FileRecord()
        {
            Path = path,
            Memo = "メモ"
        });
        await db.UpsertFileRecord(new FileRecord()
        {
            Path = path2,
            Memo = "メモる"
        });
        foreach(var f in await db.FindRecord("Clipper"))
        {
            Console.WriteLine($"Path:{f.Path} Memo:{f.Memo}");
        }
        int result = await db.DeleteFileRecord(path);
        if (result > 0)
        {
            Console.WriteLine($"{path}の削除に成功しました。");
        }
    }
}
