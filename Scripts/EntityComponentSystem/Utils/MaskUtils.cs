using System.Collections.Generic;
using System.Numerics;

public static class MaskUtils {
    public const int BlockSize = 64;
    public const int BlockCount = 4;

    public static void Set(ulong[] mask, int id) {
        int block = id / BlockSize;
        int bit = id % BlockSize;
        mask[block] |= (1UL << bit);
    }

    public static void Clear(ulong[] mask, int id) {
        int block = id / BlockSize;
        int bit = id % BlockSize;
        mask[block] &= ~(1UL << bit);
    }

    public static bool Has(ulong[] mask, int id) {
        int block = id / BlockSize;
        int bit = id % BlockSize;
        return (mask[block] & (1UL << bit)) != 0;
    }

    public static bool HasAll(ulong[] mask, ulong[] required) {
        for (int i = 0; i < BlockCount; i++) {
            if ((mask[i] & required[i]) != required[i]) {
                return false;
            }
        }

        return true;
    }

    public static List<int> ToComponentIdList(ulong[] mask) {
        List<int> result = [];

        for (int block = 0; block < mask.Length; block++) {
            ulong bits = mask[block];

            while (bits != 0) {
                int bit = BitOperations.TrailingZeroCount(bits);
                int componentId = (block * 64) + bit;
                result.Add(componentId);
                bits &= bits - 1;
            }
        }

        return result;
    }
}