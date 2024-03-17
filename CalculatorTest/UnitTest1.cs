using CalculatorNamespace;
namespace CalculatorTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        CalculatorNamespace.Calculator calculator = new CalculatorNamespace.Calculator();
        int num1 = calculator.add(1, 2);

        Assert.AreEqual(3, num1);

        int num2 = calculator.divide(4 , 2);
        Assert.AreEqual(2, num2);
        
        try
        {
            int num3 = calculator.divide(2, 0);
        }
        catch (DivideByZeroException)
        {
            Console.Error.WriteLine("Division by zero!");
        }
    }
}