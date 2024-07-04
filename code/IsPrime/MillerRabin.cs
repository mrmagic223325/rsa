using System.Numerics;

namespace IsPrime;

public static class MillerRabin
{
    
    // Proven Working
    // This Performs The First Step Of The Primality Test Which Yields `d` and `j` (`s` In English-Speaking Countries)
    private static (BigInteger, BigInteger) FirstPart(BigInteger n)
    {
        // nMinusOne
        BigInteger nMo = n - 1;
        BigInteger j = 0;
        BigInteger d = nMo;
        
        // Divide n-1 By Two Until The Result Is Odd, Incrementing `j` Everytime And Storing The Result In `d`, So That `n - 1 = d * 2^j`
        while (d % 2 == 0)
        {
            d /= 2;
            j++;
        }

        return (d, j);
    }

    private static bool SecondPart(BigInteger n, BigInteger d, BigInteger j, int k)
    {
        // Test Whether `a` Is A Witness To The Compositeness Of `n` For `k` Iterations To Improve Accuracy.
        bool isComposite = false;
        Parallel.For(0, k, (i, state) =>
        {
            BigInteger a = 2 + (BigInteger)((BigInteger)new Random().NextDouble() * (n - 3));
            // `x` Definitely Composite If true, Therefore Not A Prime
            if (Witness(n, j, d, a))
            {
                isComposite = true;
                state.Stop();
            };
        });
        return !isComposite;
    }

    private static bool Witness(BigInteger n, BigInteger j, BigInteger d, BigInteger a)
    {
        // Calculate `x = a^d mod n`
        // If `a` Is A Witness To The Compositeness Of `n` Then `n` Is Definitely Composite.
        BigInteger x = BigInteger.ModPow(a, d, n);
        // `a` Is Not A Witness To The Compositeness Of `n`
        if (x == 1 || x == n - 1)
            return false;

        for (int i = 0; i < j - 1; i++)
        {
            x = BigInteger.ModPow(x, 2, n);
            // `a` Is A Witness To The Compositeness Of `n`
            if (x == 1)
                return true;
            if (x == n - 1)
                return false;
        }

        return true;
    }

    // See https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test#Example
    // `n` Is The Number To Be Proven And `i` Is The Number Of Iterations To Run
    public static bool IsPrime(BigInteger n, int i)
    {
        if (n == 2 || n == 3)
            return true;
        if (n < 2 || n % 2 == 0)
            return false;
        
        // Calculate `d` And `j`
        (BigInteger d, BigInteger j) = FirstPart(n);
        // Check Whether Random `a` Is A Witness To The Compositeness Of `n`, Therefore Definitely Not Prime
        return SecondPart(n, d, j, i);
    }
}
