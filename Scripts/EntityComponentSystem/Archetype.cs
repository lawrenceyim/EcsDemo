using System.Linq;

namespace EntityComponentSystem;

public class Archetype {
    public ulong[] Mask { get; }
    public int Hash { get; }

    public Archetype(ulong[] mask) {
        Mask = mask;
        Hash = 17;
        foreach (ulong val in Mask) {
            Hash = Hash * 31 + val.GetHashCode();
        }
    }

    public override int GetHashCode() => Hash;

    public override bool Equals(object obj) {
        if (obj is null || obj.GetType() != typeof(Archetype)) {
            return false;
        }

        Archetype other = (Archetype)obj;
        return other.Mask.SequenceEqual(Mask);
    }
}