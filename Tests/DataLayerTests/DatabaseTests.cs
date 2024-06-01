using DataLayer.API;
using Microsoft.Data.SqlClient;
namespace Tests.DataLayerTests;

[TestClass]
[DeploymentItem("TestDatabase.mdf")]
public class DatabaseTests
{
    private static string connectionString;

    private IDataRepository _dataRepository;

    [ClassInitialize]
    public static void ClassInitializeMethod(TestContext context)
    {
        string _DBRelativePath = "TestDatabase.mdf";
        string _DBPath = Path.Combine(Directory.GetCurrentDirectory(), _DBRelativePath);
        FileInfo _databaseFile = new FileInfo(_DBPath);
        if (!_databaseFile.Exists)
        {
            throw new FileNotFoundException($"Database file {_DBPath} not found in current directory {Environment.CurrentDirectory}");
        }
        connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
    }

    [TestInitialize]
    public void TestInitialize()
    {
        IDataContext dataContext = IDataContext.CreateContext(connectionString);
        _dataRepository = IDataRepository.CreateDatabase(dataContext);
    }

    [TestMethod]
    public void TestDatabaseConnection()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Assert.IsTrue(connection.State == System.Data.ConnectionState.Open, "Connection to the database failed.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred while connecting to the database: {ex.Message}");
            }
        }
    }
}