using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int id;
    private int tempHP;
    private int currentHealth;
    private int maxHealth;

    private int armor, fortitude, reflex, will;
    private int melee, ranged, casting;

    private TextMeshProUGUI idText;
    private TextMeshProUGUI tempHPText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI armorText, fortitudeText, reflexText, willText;
    private TextMeshProUGUI meleeText, rangedText, castingText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        tempHPText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        healthText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

        armorText = transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        fortitudeText = transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();
        reflexText = transform.GetChild(0).GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>();
        willText = transform.GetChild(0).GetChild(3).GetChild(5).GetComponent<TextMeshProUGUI>();

        meleeText = transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        rangedText = transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>();
        castingText = transform.GetChild(0).GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    public void SetText(Player player)
    {
        this.id = player.ID;
        this.tempHP = player.TempHP;
        this.currentHealth = player.CurrentHP;
        this.maxHealth = player.CurrentHP;

        this.armor = player.ArmorClass;
        this.fortitude = player.Fortitude;
        this.reflex = player.Reflex;
        this.will = player.Will;

        this.melee = player.Meele;
        this.ranged = player.Ranged;
        this.casting = player.Casting;

        idText.text = $"Player ID: {id}";
        tempHPText.text = $"Temp HP: {tempHP}";
        healthText.text = $"Current Health: {currentHealth} / {maxHealth}";

        armorText.text = $"Armor\n{armor}";
        fortitudeText.text = $"Fortitude\n{fortitude}";
        reflexText.text = $"Reflex\n{reflex}";
        willText.text = $"Will\n{will}";

        meleeText.text = $"Melee\n{melee}";
        rangedText.text = $"Ranged\n{ranged}";
        castingText.text = $"Casting\n{casting}";
    }

    public void Show(bool value) { transform.GetChild(0).gameObject.SetActive(value); }
}

