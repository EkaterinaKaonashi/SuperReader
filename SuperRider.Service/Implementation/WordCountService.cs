using SuperRider.DAL.Models;
using SuperRider.DAL.Repositories.Interfaces;
using SuperRider.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperRider.Service.Implementation
{
    public class WordCountService : IWordCountService
    {
        private readonly IWordCountRepository _wordCountRepository;

        public WordCountService(IWordCountRepository wordCountRepository)
        {
            _wordCountRepository = wordCountRepository;
        }

        public async Task SaveFilteredWordsAsync(string path)
        {
            var list = new List<string>();

            using (var sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = await sr.ReadLineAsync();
                    if(string.IsNullOrEmpty(line)) break;

                    var textArray = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var filteredWords = textArray.Where(word => word.Length > 3 && word.Length < 20);

                    list.AddRange(filteredWords);
                }
            }

            var result = list.GroupBy(x => x).Select(word => new { Word = word.Key, Count = word.Count() })
                .Where(count => count.Count >= 4)
                .Select(item => new WordCountDto { Word = item.Word, Count = item.Count }).ToList();

            await _wordCountRepository.UpdateTableAsync(result);
        }
    }
}
