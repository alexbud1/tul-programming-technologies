using DataLayer.API;

namespace DataLayer.DataGeneration;

public interface IDataFiller
{
    void FillData(IDataContext dataContext, IDataRepository dataRepository);
}