using UnityEngine;

/// <summary>
/// Central configuration for slot machine
/// Single Responsibility: Game settings
/// </summary>
[CreateAssetMenu(fileName = "SlotConfig", menuName = "Slot/Config")]
public class SlotConfig : ScriptableObject
{
    [Header("Game Settings")]
    public int startingWallet = 100;
    public int minBet = 10;
    public int maxBet = 50;
    public int betIncrement = 10;

    [Header("Spin Settings")]
    public float spinSpeed = 0.1f;
    public float reel1StopDelay = 1.5f;
    public float reel2StopDelay = 2.5f;
    public float reel3StopDelay = 3.5f;

    [Header("Animation Settings")]
    public float leverPullDuration = 0.5f;
    public float winPopupDuration = 2f;
    public float shakeDuration = 0.5f;
    public float shakeStrength = 10f;

    [Header("Symbols")]
    public SlotSymbol[] symbols;

    public SlotSymbol GetRandomSymbol()
    {
        return symbols[Random.Range(0, symbols.Length)];
    }

    public int GetSymbolIndex(SlotSymbol symbol)
    {
        return System.Array.IndexOf(symbols, symbol);
    }
}