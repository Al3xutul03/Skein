using UnityEngine;

public class CharacterFetcher : MonoBehaviour
{
    [SerializeField]
    private FetchType fetchType;

    public void FetchCharacter(FetchType type)
    {
        this.gameObject.AddComponent(typeof(Player));
        Player newPlayer = this.gameObject.GetComponent<Player>();

        Character newCharacter = CharacterInfo.Instance.GetCharacters()[(int) type];
        newPlayer.Initialize(newCharacter);
        newCharacter.Initialize(newPlayer);
        Debug.Log("Player Created");
    }

    private void Awake()
    {
        FetchCharacter(fetchType);
    }
}

public enum FetchType
{
    Fighter, Rogue, Cleric
}
