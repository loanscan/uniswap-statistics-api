using System;
using System.Numerics;
using System.Text;

namespace Uniswap.Fetchers.Core.Utils
{
    public static class TypeExtensions
    {
        public static decimal ToDecimal(this BigInteger amount, BigInteger decimals)
        {
            var decimalsBigInteger = BigInteger.Pow(10, (int) decimals);
            var result = BigInteger.DivRem(amount, decimalsBigInteger, out var remainder);

            decimal resultDecimal;
            try
            {
                resultDecimal = (decimal) result + (decimal) remainder / (decimal) decimalsBigInteger;
            }
            catch (Exception)
            {
                try
                {
                    resultDecimal = (decimal) result + (decimal) ((double) remainder / (double) decimalsBigInteger);
                }
                catch (Exception e)
                {
                    return -1;
                }
            }

            return resultDecimal;
        }
        
        public static string ToStringWithTrimming(this byte[] bytes)
        {
            return bytes == null ? string.Empty : Encoding.UTF8.GetString(bytes).Trim(new[] { '\0' }).Trim();
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}