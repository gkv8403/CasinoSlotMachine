using UnityEngine;

/// <summary>
/// Manages player wallet and betting
/// Single Responsibility: Financial transactions
/// </summary>
public class WalletManager : MonoBehaviour
{
    

    private int currentWallet;
    private int winWallet;
    private int currentBet;

    void Start()
    {
        Initialize();
        SubscribeToEvents();
    }

    void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void Initialize()
    {
        currentWallet = SpinManager.instance.config.startingWallet;
        currentBet = SpinManager.instance.config.minBet;
        winWallet = 0;

        GameEvents.WalletChanged(currentWallet);
        GameEvents.WinWalletChanged(winWallet);
        GameEvents.BetChanged(currentBet);
    }

    private void SubscribeToEvents()
    {
        GameEvents.OnBetPlaced += HandleBetPlaced;
        GameEvents.OnWin += HandleWin;
    }

    private void UnsubscribeFromEvents()
    {
        GameEvents.OnBetPlaced -= HandleBetPlaced;
        GameEvents.OnWin -= HandleWin;
    }

    private void HandleBetPlaced(int amount)
    {
        currentWallet -= amount;
        GameEvents.WalletChanged(currentWallet);
    }

    private void HandleWin(int winAmount)
    {
        currentWallet += winAmount;
        winWallet += winAmount;

        GameEvents.WalletChanged(currentWallet);
        GameEvents.WinWalletChanged(winWallet);
    }

    public void IncreaseBet()
    {
        if (currentBet < SpinManager.instance.config.maxBet && currentBet < currentWallet)
        {
            currentBet = Mathf.Min(currentBet + SpinManager.instance.config.betIncrement, SpinManager.instance.config.maxBet, currentWallet);
            GameEvents.BetChanged(currentBet);
        }
    }

    public void DecreaseBet()
    {
        if (currentBet > SpinManager.instance.config.minBet)
        {
            currentBet = Mathf.Max(currentBet - SpinManager.instance.config.betIncrement, SpinManager.instance.config   .minBet);
            GameEvents.BetChanged(currentBet);
        }
    }

    public bool CanPlaceBet() => currentWallet >= currentBet;
    public int GetCurrentBet() => currentBet;
    public int GetCurrentWallet() => currentWallet;
}