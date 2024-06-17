using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IStateCRUD
    {
        static IStateCRUD CreateStateCRUD(IDataRepository? dataRepository = null)
        {
            return new StateCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddStateAsync(string id, string bookid, bool availability);

        Task<IStateDTO> GetStateAsync(string id);

        Task UpdateStateAsync(string id, string bookid, bool availability);

        Task DeleteStateAsync(string id);

        Task<Dictionary<string, IStateDTO>> GetAllStatesAsync();

        Task<string> GetStatesCountAsync();


    }
}
