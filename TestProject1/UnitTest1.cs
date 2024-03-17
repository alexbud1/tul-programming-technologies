using ClassLibrary1;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Class1 class1 = new Class1();

        class1.Num = 5;
        Assert.AreEqual(class1.Num, 5);
        try
        {
            throw new NotImplementedException("1");
        } catch(NotImplementedException)
        {
            Console.WriteLine("caught exception");
        }
        
    }
}