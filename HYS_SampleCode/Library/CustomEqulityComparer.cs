using HYS.EnumType;
using System.Collections.Generic;

public sealed class ItemTypeComparer : IEqualityComparer<ItemType>
{
    public static readonly ItemTypeComparer Comparer = new ItemTypeComparer();

    public bool Equals(ItemType x, ItemType y)
    {
        return x == y;
    }

    public int GetHashCode(ItemType obj)
    {
        return ((int)obj).GetHashCode();
    }
}

public sealed class MaterialTypeComparer : IEqualityComparer<MaterialType>
{
    public static readonly MaterialTypeComparer Comparer = new MaterialTypeComparer();

    public bool Equals(MaterialType x, MaterialType y)
    {
        return x == y;
    }

    public int GetHashCode(MaterialType obj)
    {
        return ((int)obj).GetHashCode();
    }
}