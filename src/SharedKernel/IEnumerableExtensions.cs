namespace SharedKernel;

public static class IEnumerableExtensions
{
    public static void Do<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items) 
            action(item);
    } 
}