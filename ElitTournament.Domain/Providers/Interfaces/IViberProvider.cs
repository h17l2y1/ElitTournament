using ElitTournament.Domain.Views;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
    public interface IViberProvider
    {
        Task SetWebHook();
        Task Remove();
        Task Update(RootObject res);
    }
}
