using NumCalc.Core;

namespace NumCalc.App.Services.Fakes;

public partial class MockCalculatorService : ICalculatorService
{
    public double Calculate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return 0;
        }

        return expression.Length == 5 ? 42.0 : new Random().Next(1, 100);
    }
}

