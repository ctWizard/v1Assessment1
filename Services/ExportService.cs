using System.Collections.Generic;
using System.IO;
using System.Text;
using testThurs.Models;

namespace testThurs.Services
{
    public class ExportService : IExportService
    {
        public void ExportBooks(IEnumerable<Movie> movies, string filePath)
        {
            var sb = new StringBuilder();
            foreach (var mov in movies)
            {
                sb.AppendLine($"\"{mov.Title}\",\"{mov.Director}\",{mov.ReleaseYear}");
            }
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
