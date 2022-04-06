using System.Threading.Tasks;

namespace SuperRider.Service.Interfaces
{
    public interface IWordCountService
    {
        Task SaveFilteredWordsAsync(string words);
    }
}
