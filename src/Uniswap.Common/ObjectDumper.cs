using Newtonsoft.Json;

namespace Uniswap.Common
{
    public static class ObjectDumper
    {
        public static string Dump(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}