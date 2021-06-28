using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace CSV
{
    public class CsvService
    {
        public async Task<T?> Find<T>(Predicate<T> keySelector)
            where T : class
        {
            await using var fs = new FileStream(
                $"{Path.Combine(AppContext.BaseDirectory, typeof(T).Name)}.csv",
                FileMode.OpenOrCreate, FileAccess.Read);
            using var reader = new StreamReader(fs);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            });

            var records = csvReader.GetRecordsAsync<T>();

            await foreach (var record in records)
            {
                if (keySelector(record))
                {
                    return record;
                }
            }
            
            return null;
        }
        
        public async Task Write<T>(T obj)
        {
            await using var fs = new FileStream(
                $"{Path.Combine(AppContext.BaseDirectory, typeof(T).Name)}.csv",
                FileMode.Append, FileAccess.Write);
            await using var writer = new StreamWriter(fs);
            await using var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            });
            
            await csvWriter.WriteRecordsAsync(new List<T>{obj});
        }
    }
}