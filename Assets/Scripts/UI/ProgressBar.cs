using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private float maxValue = 100.0f; // Backing field for maxValue

    public float MaxValue
    {
        get { return maxValue; }
        set
        {
            maxValue = value;
            UpdateProgress();
        }
    }

    [SerializeField]
    private float currentValue = 0.0f; // Backing field for currentValue

    public float CurrentValue
    {
        get { return currentValue; }
        set
        {
            float finalValue = toggleBounded ? Mathf.Clamp(value, 0.0f, maxValue) : value;
            currentValue = finalValue;
            UpdateProgress();
        }
    }


    [SerializeField]
    private bool toggleText = true; // Toggle for the visibility of the text
    [SerializeField]
    private bool toggleBounded = true; // Toggle for bounding current values within [0, maxValue]

    private RectTransform background;
    private RectTransform movingBar;
    private TextMeshProUGUI progressText;

    public float GetPercentage()
    {
        return currentValue / maxValue * 100.0f;
    }

    public void SetPercentage(float percentage)
    {
        currentValue = percentage / 100.0f * maxValue;
    }

    public void AddPercentage(float percentage)
    {
        float newPercentage = GetPercentage() + percentage;
        SetPercentage(newPercentage);
    }

    public void ToggleText(bool value)
    {
        toggleText = value;
        progressText.enabled = value;
    }

    public void ToggleBounded(bool value)
    {
        toggleBounded = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        background = this.transform.GetChild(1).GetComponent<RectTransform>();
        movingBar = this.transform.GetChild(2).GetComponent<RectTransform>();
        progressText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        UpdateProgress();
        progressText.enabled = toggleText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateProgress()
    {
        float normalizedPercentage = Mathf.Clamp(GetPercentage(), 0.0f, 100.0f) / 100.0f;
        float movingBarWidth = normalizedPercentage * background.rect.width;

        movingBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, movingBarWidth);
        progressText.text = currentValue.ToString() + " / " + maxValue.ToString();
    }
}
