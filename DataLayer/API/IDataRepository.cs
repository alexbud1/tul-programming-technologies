namespace DataLayer.API;

public interface IDataRepository
{
    // TODO : Implement constructor with dependency injection
    IDataRepository Create(IDataContext dataContext);
}