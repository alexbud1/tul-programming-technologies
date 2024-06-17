using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class StateModelOperation
    {
        public static StateModelOperation CreateModelOperation(IStateCRUD? stateCrud = null)
        {
            return new StateModelOperation(stateCrud ?? IStateCRUD.CreateStateCRUD());
        }


        private IStateCRUD stateCRUD;

        public StateModelOperation(IStateCRUD? stateCrud = null)
        {
            this.stateCRUD = stateCrud ?? IStateCRUD.CreateStateCRUD();
        }

        private StateModel Map(IStateDTO state)
        {
            return new StateModel(state.Id, state.BookId, state.Date, state.Availability);
        }

        public async Task AddAsync(string id, string bookid, bool availability)
        {
            await this.stateCRUD.AddStateAsync(id, bookid, availability);
        }

        public async Task<StateModel> GetAsync(string id)
        {
            return this.Map(await this.stateCRUD.GetStateAsync(id));
        }

        public async Task UpdateAsync(string id, string bookid, bool availability)
        {
            await this.stateCRUD.UpdateStateAsync(id, bookid, availability);
        }

        public async Task DeleteAsync(string id)
        {
            await this.stateCRUD.DeleteStateAsync(id);
        }

        public async Task<Dictionary<string, StateModel>> GetAllAsync()
        {
            Dictionary<string, StateModel> result = new Dictionary<string, StateModel>();

            foreach (IStateDTO state in (await this.stateCRUD.GetAllStatesAsync()).Values)
            {
                result.Add(state.Id, this.Map(state));
            }

            return result;
        }

        public async Task<string> GetCountAsync()
        {
            return await this.stateCRUD.GetStatesCountAsync();
        }
    }
}
