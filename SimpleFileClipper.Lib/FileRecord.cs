using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFileClipper.Lib;

public class FileRecord
{
     [Key]
     public string Path { get; set; } = "";
     public string Memo { get; set; } = "";

}