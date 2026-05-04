namespace ContactBook;

public sealed class Union<T>
    where T : notnull
{
    private readonly Dictionary<T, T> _parents = [];
    private readonly Dictionary<T, int> _ranks = [];

    public void MakeSet(T value)
    {
        if (_parents.ContainsKey(value))
        {
            return;
        }

        _parents[value] = value;
        _ranks[value] = 0;
    }

    public T Find(T value)
    {
        // Path compression keeps future Find calls close to constant time.
        MakeSet(value);

        if (!EqualityComparer<T>.Default.Equals(_parents[value], value))
        {
            _parents[value] = Find(_parents[value]);
        }

        return _parents[value];
    }

    public bool Join(T first, T second)
    {
        // Union by rank keeps the tree shallow when two sets are joined.
        var firstRoot = Find(first);
        var secondRoot = Find(second);

        if (EqualityComparer<T>.Default.Equals(firstRoot, secondRoot))
        {
            return false;
        }

        if (_ranks[firstRoot] < _ranks[secondRoot])
        {
            _parents[firstRoot] = secondRoot;
        }
        else if (_ranks[firstRoot] > _ranks[secondRoot])
        {
            _parents[secondRoot] = firstRoot;
        }
        else
        {
            _parents[secondRoot] = firstRoot;
            _ranks[firstRoot]++;
        }

        return true;
    }

    public bool Connected(T first, T second)
    {
        return EqualityComparer<T>.Default.Equals(Find(first), Find(second));
    }
}

