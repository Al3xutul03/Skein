using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo Instance;

    private List<Character> characters;
    public List<Character> Characters {  get { return characters; } }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCharacters(List<Character> characters) { this.characters = characters; }
    public List<Character> GetCharacters() { return characters; }
}
