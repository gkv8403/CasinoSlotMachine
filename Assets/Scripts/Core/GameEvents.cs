using System;
using UnityEngine;

/// <summary>
/// Central event system for slot machine game
/// Implements Observer pattern for decoupled communication
/// </summary>
public static class GameEvents
{
    // Betting Events
    public static event Action<int> OnBetChanged;
    public static event Action<int> OnBetPlaced;

    // Spin Events
    public static event Action OnSpinStarted;
    public static event Action<int> OnReelStopped; // reelIndex
    public static event Action OnAllReelsStopped;

    // Result Events
    public static event Action<int> OnWin; // winAmount
    public static event Action OnLose;

    // Wallet Events
    public static event Action<int> OnWalletChanged;
    public static event Action<int> OnWinWalletChanged;

    // Animation Events
    public static event Action OnLeverPulled;
    public static event Action OnLeverReset;

    // Invoke Methods
    public static void BetChanged(int newBet) => OnBetChanged?.Invoke(newBet);
    public static void BetPlaced(int amount) => OnBetPlaced?.Invoke(amount);
    public static void SpinStarted() => OnSpinStarted?.Invoke();
    public static void ReelStopped(int index) => OnReelStopped?.Invoke(index);
    public static void AllReelsStopped() => OnAllReelsStopped?.Invoke();
    public static void Win(int amount) => OnWin?.Invoke(amount);
    public static void Lose() => OnLose?.Invoke();
    public static void WalletChanged(int newAmount) => OnWalletChanged?.Invoke(newAmount);
    public static void WinWalletChanged(int newAmount) => OnWinWalletChanged?.Invoke(newAmount);
    public static void LeverPulled() => OnLeverPulled?.Invoke();
    public static void LeverReset() => OnLeverReset?.Invoke();
}