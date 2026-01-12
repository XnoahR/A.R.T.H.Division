using UnityEngine;
using System.Collections.Generic;

public static class UpgradeRandomizer
{
    public static List<UpgradeData> Generate(List<UpgradeData> pool, int count)
    {
        List<UpgradeData> result = new();
        HashSet<UPGRADE_CATEGORY> usedCategory = new();
        HashSet<string> usedStat = new();

        List<UpgradeData> shuffled = new(pool);
        Shuffle(shuffled);

        foreach (var upgrade in shuffled)
        {
            if (result.Count >= count)
            {
                break;
            }
            if (upgrade.category == UPGRADE_CATEGORY.STATS || upgrade.category == UPGRADE_CATEGORY.WEAPON)
            {
                if (usedStat.Contains(upgrade.upgradeName)) continue;

                usedStat.Add(upgrade.upgradeName);
            }
            else
            {
                if (usedCategory.Contains(upgrade.category)) continue;
                usedCategory.Add(upgrade.category);

            }

            result.Add(upgrade);
        }
        return result;
    }

    static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
}