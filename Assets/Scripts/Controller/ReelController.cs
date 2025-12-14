using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

/// <summary>
/// Controls a single reel with 3 visible symbols
/// Properly tracks middle symbol for win condition
/// </summary>
public class ReelController : MonoBehaviour
{
    [SerializeField] private Image[] symbolSlots = new Image[3]; // 0=top, 1=middle, 2=bottom
    [SerializeField] private int reelIndex;

    private bool isSpinning = false;
    private SlotSymbol middleSymbol; // Store middle symbol separately
    private Sequence spinSequence;

    void Start()
    {
        InitializeReel();
    }

    private void InitializeReel()
    {
        // Initialize with random symbols
        for (int i = 0; i < 3; i++)
        {
            SlotSymbol symbol = SpinManager.instance.config.GetRandomSymbol();
            symbolSlots[i].sprite = symbol.symbolSprite;

            // Store the middle symbol
            if (i == 1)
            {
                middleSymbol = symbol;
                Debug.Log($"[Reel {reelIndex}] Initialized middle symbol: {symbol.symbolName}");
            }
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
        Debug.Log($"[Reel {reelIndex}] Spin started");

        // Continuous spin animation
        spinSequence = DOTween.Sequence();
        spinSequence.AppendCallback(() => UpdateSymbols())
                    .AppendInterval(SpinManager.instance.config.spinSpeed)
                    .SetLoops(-1);
    }

    public void StopSpin()
    {
        if (spinSequence != null)
        {
            spinSequence.Kill();
        }

        // Set final symbols and properly track middle symbol
        for (int i = 0; i < 3; i++)
        {
            SlotSymbol symbol = SpinManager.instance.config.GetRandomSymbol();
            symbolSlots[i].sprite = symbol.symbolSprite;

            // Ensure middle symbol is properly stored
            if (i == 1)
            {
                middleSymbol = symbol;
                Debug.Log($"[Reel {reelIndex}] Stopped - Middle symbol set to: {symbol.symbolName}");
            }
        }

        isSpinning = false;
        GameEvents.ReelStopped(reelIndex);
    }

    private void UpdateSymbols()
    {
        if (!isSpinning) return;

        for (int i = 0; i < 3; i++)
        {
            SlotSymbol symbol = SpinManager.instance.config.GetRandomSymbol();
            symbolSlots[i].sprite = symbol.symbolSprite;
        }
    }

    public SlotSymbol GetMiddleSymbol()
    {
        Debug.Log($"[Reel {reelIndex}] GetMiddleSymbol called - returning: {middleSymbol.symbolName}");
        return middleSymbol;
    }

    public void PlayShakeAnimation()
    {
        Debug.Log($"[Reel {reelIndex}] Shake animation triggered");
        // Shake only the symbol images, not the entire reel
        foreach (var symbolSlot in symbolSlots)
        {
            symbolSlot.transform.DOShakePosition(
                SpinManager.instance.config.shakeDuration,
                SpinManager.instance.config.shakeStrength,
                10,
                90,
                false,
                true
            );
        }
    }

    void OnDestroy()
    {
        if (spinSequence != null)
        {
            spinSequence.Kill();
        }
    }
}