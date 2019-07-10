using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class BitUtil
{

	public static int ALL_ZERO = 0;
    public static int ALL_ONE = ~0;
	
    public static int SetBit(this int A, int k, bool val)
    {
        if(k > 32)
            throw new Exception("The k can't more than 32");

        return val ? A | (1 << k) : A & ~(1 << k);
    }

    public static int SetBit(this int A, int k)
    {
        if (k > 32)
            throw new Exception("The k can't more than 32");

        return A | (1 << k);
    }

    public static int ClearBit(this int A, int k)
    {
        if (k > 32)
            throw new Exception("The k can't more than 32");

        return A & ~(1 << k);
    }

    public static int ToggleBit(this int A, int k)
    {
        if (k > 32)
            throw new Exception("The k can't more than 32");

        return A ^ (1 << k);
    }

    public static bool GetBit(this int A, int k)
    {
        if (k > 32)
            throw new Exception("The k can't more than 32");

        return (A & (1 << k)) != 0;
    }

    public static string ToBinaryString(this int A, char sep = ',')
    {
        StringBuilder sb = new StringBuilder();

        int k = 0;
        while (k < 32)
        {
            sb.Append(GetBit(A, k++) ? "1" : "0");
            if (k % 8 == 0 && k < 32)
                sb.Append(sep);
        }

        char[] c = sb.ToString().ToCharArray();
        Array.Reverse(c);
        return new string(c);
    }

}
