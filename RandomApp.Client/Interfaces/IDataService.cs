using System.Threading.Tasks;

namespace RandomApp.Client.Interfaces
{
    public interface IDataService
    {
        Task<int> GetRandomNumber();
    }
}
