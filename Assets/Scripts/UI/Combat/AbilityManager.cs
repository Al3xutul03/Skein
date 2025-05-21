using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private Dictionary<Player, AbilityList> abilityListDictionary = new Dictionary<Player, AbilityList>();

    public void Initialize(IEnumerable<Player> players)
    {
        GameObject abilityListPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/AbilityList");

        foreach (Player player in players)
        {
            GameObject newPrefab = Instantiate(abilityListPrefab, this.transform);
            AbilityList newList = newPrefab.GetComponent<AbilityList>();

            newList.Initialize(player, transform.parent.GetComponentInChildren<AbilityDescription>());
            abilityListDictionary.Add(player, newList);
        }

        foreach (var list in abilityListDictionary.Values)
        {
            list.gameObject.SetActive(false);
        }
    }

    public void ChangeList(Player player)
    {
        foreach (var list in abilityListDictionary.Values)
        {
            list.gameObject.SetActive(false);
        }

        abilityListDictionary[player].gameObject.SetActive(true);
    }
}
