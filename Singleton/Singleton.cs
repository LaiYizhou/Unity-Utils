public class Singleton<T> where T : class, new()
{
    private static T instance;
    private static readonly object syncObject = new object();
    public static T Instance
    {
        get
        {
            lock (syncObject)
            {
                if (null == instance)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
