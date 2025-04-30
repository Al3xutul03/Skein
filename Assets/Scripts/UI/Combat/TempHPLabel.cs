using TMPro;
using UnityEngine;

public class TempHPLabel : MonoBehaviour
{
    [SerializeField]
    private string defaultText;
    private TextMeshProUGUI hpText;
    private int tempHP;
    public int TempHP
    {
        get { return tempHP; }
        set
        {
            tempHP = value;
            hpText.text = defaultText + tempHP.ToString();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpText = transform.GetComponentInChildren<TextMeshProUGUI>();
        hpText.text = defaultText + "0";
    }
}
