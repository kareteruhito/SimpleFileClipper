using System.IO;

// 検索文字列の読み書き用クラス
public class SearchWordService
{
    static string _SearchWordFile = @"SearchWord.txt";

    // 読み込み
    public static IEnumerable<SearchWordModel> Load()
    {
        string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _SearchWordFile);
        if (!File.Exists(file)) yield break;

        using StreamReader reader = new (file);

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            SearchWordModel swm = new()
            {
                SearchWord = line,
            };
            yield return swm;
        } 
    }

    // 書き込み
    public static void Save(IEnumerable<SearchWordModel> searchWordModels)
    {
        string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _SearchWordFile);
        using StreamWriter writer = new (file);

        foreach(var swm in searchWordModels)
        {
            writer.WriteLine(swm.SearchWord);
        }        
    }
}