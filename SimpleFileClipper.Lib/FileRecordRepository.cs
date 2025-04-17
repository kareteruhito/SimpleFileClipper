using Microsoft.EntityFrameworkCore;

namespace SimpleFileClipper.Lib;

public class FileRecordRepository : DbContext
{
    public DbSet<FileRecord> FileRecords => Set<FileRecord>();
    public string DbPath { get; set; } = "";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    // コンストラクタ
    public FileRecordRepository(string dbFile = "")
    {
        if (dbFile == "")
        {
            dbFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"mydatabase.db");
        }
        DbPath = dbFile;
        // データベースの作成 (まだ存在しない場合)
        this.Database.EnsureCreated();
    }
    // 追加更新
    public async Task UpsertFileRecord(FileRecord record)
    {
        var existingRecord = await this.FileRecords.FindAsync(record.Path);

        if (existingRecord != null)
        {
            // 既に存在するので更新
            this.Entry(existingRecord).CurrentValues.SetValues(record);
        }
        else
        {
            // 存在しないので追加
            this.FileRecords.Add(record);
        }

        await this.SaveChangesAsync();
    }
    // 削除
    public async Task<int> DeleteFileRecord(string path)
    {
        var deleteRecord = await this.FileRecords.FindAsync(path);
        if (deleteRecord == null) return 0;

        // 見つかったレコードを削除
        this.FileRecords.Remove(deleteRecord);

        // 変更をデータベースに保存
        int deletedCount = await this.SaveChangesAsync();

        return deletedCount;
    }
    // 検索
    public async Task<FileRecord[]> FindRecord(string keyword)
    {
        // Memo または Path にキーワードを含む FileRecord を検索
        var results = await this.FileRecords
            .Where(r => r.Memo.Contains(keyword) || r.Path.Contains(keyword))
            .ToArrayAsync();

        return results;
    }
}