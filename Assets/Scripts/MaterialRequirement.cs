using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

[System.Serializable]
public class MaterialRequirement
{
    public int wood;
    public int scrapMetal;
    public int refinedMetal;

    public static ItemPrototype woodPrototype;
    private static ItemPrototype scrapMetalPrototype;
    private static ItemPrototype refinedMetalPrototype;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Material: \n");
        sb.AppendLine("Wood x " + wood);
        sb.AppendLine("Scrap Metal x " + scrapMetal);
        sb.AppendLine("Refined Metal x " + refinedMetal);

        return sb.ToString();
    }

    private void AcquirePrototypes()
    {
        if (!woodPrototype)
        {
            foreach (ItemPrototype prototype in Resources.FindObjectsOfTypeAll<ItemPrototype>())
            {
                if (prototype.name == "Wood")
                {
                    woodPrototype = prototype;
                }
                else if (prototype.name == "Scrap Metal")
                {
                    scrapMetalPrototype = prototype;
                }
                else if (prototype.name == "Refined Metal")
                {
                    refinedMetalPrototype = prototype;
                }
            }
        }

        Debug.Assert(woodPrototype && scrapMetalPrototype && refinedMetalPrototype);
    }

    public bool CheckRequirement()
    {
        AcquirePrototypes();
        return Backpack.Instance.HasEnoughItem(woodPrototype, wood)
            && Backpack.Instance.HasEnoughItem(scrapMetalPrototype, scrapMetal) 
            && Backpack.Instance.HasEnoughItem(refinedMetalPrototype, refinedMetal);
    }

    public bool TryConsumeRequirement()
    {
        if (!CheckRequirement())
        {
            return false;
        }
        return Backpack.Instance.TryUseItem(woodPrototype, wood, false)
            && Backpack.Instance.TryUseItem(scrapMetalPrototype, scrapMetal, false)
            && Backpack.Instance.TryUseItem(refinedMetalPrototype, refinedMetal, false);
    }
}
