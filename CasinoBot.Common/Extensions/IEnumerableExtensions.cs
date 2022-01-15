namespace CasinoBot.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        // Keep this static to the class to avoid different sources obtaining the seed
        private static readonly Random r = new Random();
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                lock (r)
                {
                    var i = r.Next(list.Count + 1);
                    if (i == list.Count)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        var temp = list[i];
                        list[i] = item;
                        list.Add(temp);
                    }

                }
            }
            return list;
        }
    }

}
