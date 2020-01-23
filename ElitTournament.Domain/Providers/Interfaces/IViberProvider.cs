using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
    public interface IViberProvider
    {
        Task SetWebHook();
        Task Remove();
    }
}
