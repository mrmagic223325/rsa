using System.Numerics;
using System.Runtime.InteropServices;

namespace IsPrime;

public abstract class Program
{
    // x^n mod m
    // This is faster than BigInteger.ModPow [citation needed]
    // Derived From https://en.wikipedia.org/wiki/Modular_exponentiation and https://github.com/RJT529/Algos/blob/master/Modular%20Exponentiation%5BRight-to-left%20binary%20method%5D
    public static BigInteger ModPow(BigInteger x, BigInteger n, BigInteger m)
    {
        if (m == 1)
            return 0;
        
        BigInteger res = 1;
        x %= m;

        while (n > 0)
        {
            if (n % 2 == 1)
                res = (res * x) % m;
            n >>= 1;
            x = (x * x) % m;
        }

        return res;
    }
    
    [DllImport("libc")]
    static extern unsafe int getrandom(void* buf, int buflen, int flags);
    
    public static unsafe BigInteger GetRandomBigIntInRange(int size, BigInteger lo, BigInteger hi)
    {
        byte[] buf = new byte[size];
        fixed (byte* bufPtr = buf)
        {
            getrandom(bufPtr, size, 0);
        }

        BigInteger result = new BigInteger(0);

        for (int i = buf.Length - 1; i >= 0; i--)
        {
            result <<= 8;
            result |= buf[i];
        }

        if (result < lo)
            return (lo - result) + result;
        if (result > hi)
            return (result - hi) - hi;
        
        return result;
    }
    
    static unsafe BigInteger GetRandomBigInt(int size)
    {
        byte[] buf = new byte[size];
        fixed (byte* bufPtr = buf)
        {
            getrandom(bufPtr, size, 0);
        }

        BigInteger result = new BigInteger(0);

        for (int i = buf.Length - 1; i >= 0; i--)
        {
            result <<= 8;
            result |= buf[i];
        }

        return result;
    }
    
    public static void Main()
    {
        // BigInteger = 2 Random 256 Byte Numbers That Pass MRT and Fermat
        BigInteger p = 0, q = 0;

        var t1 = new Task(() =>
        {
            Restart:
            
            // Generate Random number of 2048 bits / 256 Bytes
            p = GetRandomBigInt(256);
            
            // All primes >2 are odd
            p |= 1 << 0;
                
            Failed:

            // Trial Division State
            bool failed = false;
            
            // Divide by small primes
            Parallel.ForEach(Primes.List, (prime, state) =>
            {
                if (p % prime == 0)
                {
                    p += 2;
                    failed = true;
                    state.Break();
                }
            });

            if (failed)
                goto Failed;
            
            if (!Fermat(p, 1))
                
                goto Restart;

            if (MillerRabin.IsPrime(p, 5))
                return;
            
            goto Restart;
        });
            
        var t2 = new Task(() =>
        {
            Restart:
            
            // Generate Random number of 2048 bits / 256 Bytes
            q = GetRandomBigInt(256);
            
            // All primes >2 are odd
            q |= 1 << 0;
                
            Failed:
            
            // Trial Division State
            bool failed = false;
            
            // Divide by small primes (first 5000 primes)
            Parallel.ForEach(Primes.List, (prime, state) =>
            {
                if (q % prime == 0)
                {
                    q += 2;
                    failed = true;
                    state.Break();
                }
            });

            if (failed)
                goto Failed;

            if (!Fermat(q, 1))
                goto Restart;

            if (MillerRabin.IsPrime(q, 5))
                return;
            
            goto Restart;
        });

        t1.Start();
        t2.Start();

        while (!(t1.IsCompleted && t2.IsCompleted)) {}
        
        BigInteger n = p * q;
        Console.WriteLine($"{p} {q}\n");
        Console.WriteLine($"Mod n = {n}");
    } 
    private static bool Fermat(BigInteger n, int k)
    {
        if (n == 3 || n == 2)
            return true;
        
        for (int _ = 0; _ < k; _++)
        {
            BigInteger a = GetRandomBigIntInRange( n.GetByteCount(),2,  n - 2);
            // Calculate `a^(n-1) mod n`
            BigInteger res = ModPow(a, n - 1, n);

            if (res != 1)
            {
                return false;
            }
        }
        return true;
    }
}
