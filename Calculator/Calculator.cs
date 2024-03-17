namespace CalculatorNamespace;

public class Calculator
{
    public int add(int v1, int v2)
    {
        return v1 + v2;
        //throw new NotImplementedException()
    }

    public int divide(int v1, int v2)
    {
        if (v2 == 0)
            throw new DivideByZeroException();
        else
            return v1 / v2;
    }
}