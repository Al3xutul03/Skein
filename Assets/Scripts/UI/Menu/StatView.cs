using System.Collections.Generic;
using UnityEngine;

public class StatView : MonoBehaviour
{
    private Dictionary<StatViewType, Stat> statMap;

    public int getStat(StatViewType type) { return statMap[type].Value; }

    public void SetStat(StatViewType type, int value) { statMap[type].Value = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform statBar = transform.GetChild(0);

        statMap = new Dictionary<StatViewType, Stat>
        {
            { StatViewType.Meele, statBar.GetChild(0).GetComponent<Stat>() },
            { StatViewType.Ranged, statBar.GetChild(1).GetComponent<Stat>() },
            { StatViewType.Casting, statBar.GetChild(2).GetComponent<Stat>() },
            { StatViewType.Armor, statBar.GetChild(3).GetComponent<Stat>() },
            { StatViewType.Fortitude, statBar.GetChild(4).GetComponent<Stat>() },
            { StatViewType.Reflex, statBar.GetChild(5).GetComponent<Stat>() },
            { StatViewType.Will, statBar.GetChild(6).GetComponent<Stat>() },
        };
    }
}

public enum StatViewType
{
    Meele, Ranged, Casting, Armor, Fortitude, Reflex, Will
}
