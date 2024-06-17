using Data.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    internal class StateCRUD : IStateCRUD
    {
        private IDataRepository dataRepository;

        public StateCRUD(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        private IStateDTO Map(IState state)
        {
            return new StateDTO(state.Id, state.BookId, state.Availability);
        }

        public async Task AddStateAsync(string id, string bookid, bool availability)
        {
            await this.dataRepository.AddStateAsync(id, bookid, availability);
        }

        public async Task<IStateDTO> GetStateAsync(string id)
        {
            return this.Map(await this.dataRepository.GetStateAsync(id));
        }

        public async Task UpdateStateAsync(string id, string bookid, bool availability)
        {
            await this.dataRepository.UpdateStateAsync(id, bookid, availability);
        }

        public async Task DeleteStateAsync(string id)
        {
            await this.dataRepository.DeleteStateAsync(id);
        }

        public async Task<Dictionary<string, IStateDTO>> GetAllStatesAsync()
        {
            Dictionary<string, IStateDTO> result = new Dictionary<string, IStateDTO>();

            foreach (IState state in (await this.dataRepository.GetAllStatesAsync()).Values)
            {
                result.Add(state.Id, this.Map(state));
            }

            return result;
        }

        public async Task<string> GetStatesCountAsync()
        {
            return await this.dataRepository.GetStatesCountAsync();
        }

    }
}
