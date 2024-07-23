using TMPro;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Damage Displayed")]
    public TextMeshProUGUI damageText; // Reference to the TextMeshPro component
    public float maxDamageDisplay = 300f; // Max display damage number

    [Header("Actual Damage Values")]
    public float damageTaken = 0f; // Track the accumulated damage
    public float maxDamage = 20f; // Max damage before scaling

    [Header("Color Gradient")]
    public Color minDamageColor; // Min Damage Color
    public Color maxDamageColor; // Max Damage Color

    [Header("Percentage of Max Damage to reach Max Color")]
    public float colorThreshold = 0.75f; // Percentage of max damage to reach max color

    // Method to update damage and display it
    public void UpdateDamageDisplay()
    {
        // Calculate the scaled damage to display
        float damageToDisplay = (damageTaken / maxDamage) * maxDamageDisplay;

        // Round to the nearest whole number
        int roundedDamage = Mathf.RoundToInt(damageToDisplay);

        // Format the text with the rounded damage and a % symbol
        string displayText = $"{roundedDamage}%";

        // Calculate the color based on the damage
        float colorLerpFactor = Mathf.Clamp01(damageTaken / (maxDamage * colorThreshold));
        Color currentColor = Color.Lerp(minDamageColor, maxDamageColor, colorLerpFactor);

        // Update the TextMeshPro component with the formatted text and color
        if (damageText != null)
        {
            damageText.text = displayText;
            damageText.color = currentColor;
        }
    }

    // Call this method to apply damage
    public void IncreaseDamage(float amount)
    {
        damageTaken = Mathf.Min(damageTaken + amount, maxDamage);
        UpdateDamageDisplay(); // Update the display whenever damage is applied
    }

    // Method to get the current damage (useful if you need to reference it elsewhere)
    public float GetDamage()
    {
        return damageTaken;
    }

    // Method to reset damage to 0
    public void ResetDamage()
    {
        damageTaken = 0f;
        UpdateDamageDisplay(); // Update the display whenever damage is reset
    }
}
