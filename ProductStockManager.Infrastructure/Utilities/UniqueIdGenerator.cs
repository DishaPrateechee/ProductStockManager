namespace ProductStockManager.Core.Utilities
{
    public class UniqueIdGenerator
    {
        private static readonly Random _random = new();
        private static readonly HashSet<string> _generated = new();

        public static string Generate()
        {
            string id;
            do
            {
                id = _random.Next(100000, 999999).ToString();
            } while (_generated.Contains(id));

            _generated.Add(id);
            return id;
        }
    }
}
