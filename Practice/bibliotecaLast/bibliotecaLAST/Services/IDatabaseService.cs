using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApplication1.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<string>> GetNamesAsync();

    }
}
