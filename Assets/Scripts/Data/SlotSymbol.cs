using UnityEngine;

/// <summary>
/// Defines slot machine symbols and their properties
/// Single Responsibility: Symbol configuration
/// </summary>
[CreateAssetMenu(fileName = "SlotSymbol", menuName = "Slot/Symbol")]
public class SlotSymbol : ScriptableObject
{
    public string symbolName;
    public Sprite symbolSprite;
    public int payoutMultiplier;
    public Color symbolColor = Color.white;
}