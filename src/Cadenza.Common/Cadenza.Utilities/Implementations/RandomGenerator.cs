using Cadenza.Utilities.Interfaces;

namespace Cadenza.Utilities.Implementations;

public class RandomGenerator : IRandomGenerator
{
    private readonly IDateTime _dateTime;
    private readonly Lazy<Random> _random;

    public RandomGenerator(IDateTime dateTime)
    {
        _dateTime = dateTime;
        _random = new Lazy<Random>(GetRandom);
    }

    public int Next()
    {
        return _random.Value.Next();
    }

    private Random GetRandom()
    {
        return new Random(_dateTime.Now.Second * _dateTime.Now.Millisecond);
    }
}