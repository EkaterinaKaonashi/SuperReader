using SuperRider.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperRider.DAL.Repositories.Interfaces
{
    public interface IWordCountRepository
    {
        Task UpdateTableAsync(List<WordCountDto> words);
    }
}
