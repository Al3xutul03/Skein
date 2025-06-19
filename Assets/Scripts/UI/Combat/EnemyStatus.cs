using TMPro;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private int id;
    private int currentHealth;
    private int maxHealth;

    private TextMeshProUGUI idText;
    private TextMeshProUGUI healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        healthText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetText(int id, int currentHealth, int maxHealth)
    {
        this.id = id;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;

        string status;
        float currentPercent = (float)currentHealth / (float)maxHealth * 100.0f;

        if (currentPercent >= 100.0f) { status = "Unharmed"; }
        else if (currentPercent > 50.0f && currentPercent < 100.0f) { status = "Wounded"; }
        else if (currentPercent > 0.0f && currentPercent <= 50.0f) { status = "Dying"; }
        else { status = "Dead"; }

        idText.text = $"Enemy ID: {id}";
        healthText.text = $"Current Health:\n{status}";
    }

    public void Show(bool value) { transform.GetChild(0).gameObject.SetActive(value); }
}
