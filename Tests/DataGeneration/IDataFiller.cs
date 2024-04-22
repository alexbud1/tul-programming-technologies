using DataLayer.API;

namespace Tests.DataGeneration;

public interface IDataFiller
{
    void FillData(IDataContext dataContext, IDataRepository dataRepository);
}