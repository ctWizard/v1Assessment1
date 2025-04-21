using System.Collections.Generic;
using testThurs.Models;

namespace testThurs.Services
{
    public interface IExportService
    {
        void ExportBooks(IEnumerable<Movie> movies, string filePath);
    }
}
