using System.Numerics;
using System.Security.Cryptography;

namespace IsPrime;

public static class MillerRabin
{
    // See https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test#Example
    // `n` Is The Number To Be Proven And `k` Is The Number Of Iterations To Run
    public static bool IsPrime(BigInteger n, int k)
    {
        if (n == 2 || n == 3)
            return true;
        if (n < 2 || n % 2 == 0)
            return false;
        
        // Calculate `d` And `j`
        (BigInteger d, BigInteger j) = FirstPart(n);
        // Check Whether Random `a` Is A Witness To The Compositeness Of `n`, Therefore Definitely Not Prime
        return SecondPart(n, d, j, k);
    }
    
    // Proven Working
    // This Performs The First Step Of The Primality Test Which Yields `d` and `j` (`s` In English-Speaking Countries)
    private static (BigInteger, BigInteger) FirstPart(BigInteger n)
    {
        // Do-While Loops will always run at least once, therefore we set j to -1
        BigInteger d = n - 1;
        BigInteger j = -1;
        
        // Divide n-1 By Two Until The Result Is Odd, Incrementing `j` Everytime And Storing The Result In `d`, So That `n - 1 = d * 2^j`
        do
        {
            j++;
        } while ((d /= 2).IsEven);
       
        return (d, j);
    }

    private static bool SecondPart(BigInteger n, BigInteger d, BigInteger j, int k)
    {
        // Test Whether `a` Is A Witness To The Compositeness Of `n` For `k` Iterations To Improve Accuracy.
        // `x` Very likely Composite If true
        return Witness(n, d, j, k);
    }

    private static bool Witness(BigInteger n, BigInteger d, BigInteger j, int k)
    {
        
        for (int i = 0; i < k; i++)
        {
            // If `a` Is A Witness To The Compositeness Of `n` Then `n` Is Definitely Composite.
            BigInteger a = Program.GetRandomBigIntInRange(n.GetByteCount(), 2, n - 2);
            
            BigInteger x = Program.ModPow(a, d, n);
            if (x == 1 || x == n - 1)
                continue;

            for (int y = 1; y < j; y++)
            {
                x = Program.ModPow(x, 2, n);
                if (x == 1)
                    return false;
                if (x == n - 1)
                    break;
            }

            if (x != n - 1)
                return false;

        }
        return true;
    }
}
