using System.Numerics;
using CommunityToolkit.Diagnostics;

namespace IsPrime;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine($"{UInt128.MinValue} {UInt128.MaxValue}");
        
        int[] primes = [ 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83,
            89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179,	181, 191,
            193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307,
            311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431,
            433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541 ];

        BigInteger bigAssNumber = BigInteger.Pow(2, 4096)-1;
        
        Console.WriteLine(bigAssNumber.GetBitLength());
        foreach (var @byte in bigAssNumber.ToByteArray())
        {
            Console.WriteLine(@byte.ToHexString());
        }
        

        /*
        foreach (int x in primes)
        {
            Console.WriteLine(Fermat(x, 1000));
            Console.WriteLine(MillerRabin.IsPrime(x, 1000));
        }
        */

        //Console.WriteLine(Fermat(8321, 1000));
        //Console.WriteLine(MillerRabin.IsPrime(8321, 1000));
    }

    private static bool Fermat(long n, int k)
    {
        if (n is 3 or 2)
            return true;
        
        for (int _ = 0; _ < k; _++)
        {
            long a = Random.Shared.NextInt64(2, n - 2);
            // Calculate `a^(n-1) mod n`
            BigInteger res = BigInteger.ModPow(a, n - 1, n);

            if (res != 1)
            {
                return false;
            }
        }
        return true;
    }
}
