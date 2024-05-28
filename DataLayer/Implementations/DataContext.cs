using DataLayer.API;

namespace DataLayer.Implementations;

internal class DataContext : IDataContext
{
    private readonly string _connectionString;

    public DataContext(string? connectionString = null)
    {
        if (connectionString is null)
        {
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string _DBRelativePath = @"DataLayer\Database\Database.mdf";
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            this._connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }
        else
        {
            this._connectionString = connectionString;
        }
    }

    #region Supplier CRUD

    public async Task AddSupplierAsync(ISupplier supplier)
    {
        using DatabaseDataContext _context = new DatabaseDataContext(_connectionString);
    }

    #endregion
}