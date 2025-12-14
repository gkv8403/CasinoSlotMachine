using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the spin sequence and win/lose logic
/// Single Responsibility: Spin orchestration
/// </summary>
public class SpinManager : MonoBehaviour
{
    public static SpinManager instance;
    public SlotConfig config;
    [SerializeField] private WalletManager walletManager;
    [SerializeField] private ReelController[] reels;

    private bool isSpinning = false;
    private int stoppedReelCount = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        GameEvents.OnReelStopped += HandleReelStopped;
    }

    void OnDestroy()
    {
        GameEvents.OnReelStopped -= HandleReelStopped;
    }

    public void RequestSpin()
    {
        if (isSpinning || !walletManager.CanPlaceBet())
        {
            Debug.Log("[SpinManager] Spin request rejected - already spinning or cannot place bet");
            return;
        }

        Debug.Log("[SpinManager] Spin request accepted");
        StartCoroutine(SpinSequence());
    }

    private IEnumerator SpinSequence()
    {
        isSpinning = true;
        stoppedReelCount = 0;

        Debug.Log("[SpinManager] === SPIN SEQUENCE STARTED ===");

        // Place bet
        int betAmount = walletManager.GetCurrentBet();
        GameEvents.BetPlaced(betAmount);
        Debug.Log($"[SpinManager] Bet placed: ${betAmount}");

        // Pull lever
        GameEvents.LeverPulled();
        yield return new WaitForSeconds(config.leverPullDuration);
        GameEvents.LeverReset();
        Debug.Log("[SpinManager] Lever animation complete");

        // Start spinning all reels
        GameEvents.SpinStarted();
        Debug.Log("[SpinManager] All reels spinning...");
        foreach (var reel in reels)
        {
            reel.StartSpin();
        }

        // Stop reels one by one
        yield return new WaitForSeconds(config.reel1StopDelay);
        Debug.Log("[SpinManager] Stopping Reel 1");
        reels[0].StopSpin();

        yield return new WaitForSeconds(config.reel2StopDelay - config.reel1StopDelay);
        Debug.Log("[SpinManager] Stopping Reel 2");
        reels[1].StopSpin();

        yield return new WaitForSeconds(config.reel3StopDelay - config.reel2StopDelay);
        Debug.Log("[SpinManager] Stopping Reel 3");
        reels[2].StopSpin();
    }

    private void HandleReelStopped(int reelIndex)
    {
        stoppedReelCount++;
        Debug.Log($"[SpinManager] Reel {reelIndex} stopped. Total stopped: {stoppedReelCount}/{reels.Length}");

        if (stoppedReelCount >= reels.Length)
        {
            GameEvents.AllReelsStopped();
            Debug.Log("[SpinManager] All reels stopped. Checking win condition...");
            CheckWinCondition();
            isSpinning = false;
        }
    }

    private void CheckWinCondition()
    {
        SlotSymbol symbol1 = reels[0].GetMiddleSymbol();
        SlotSymbol symbol2 = reels[1].GetMiddleSymbol();
        SlotSymbol symbol3 = reels[2].GetMiddleSymbol();

        Debug.Log($"[SpinManager] === WIN CHECK ===");
        Debug.Log($"[SpinManager] Reel 1 middle: {symbol1.symbolName}");
        Debug.Log($"[SpinManager] Reel 2 middle: {symbol2.symbolName}");
        Debug.Log($"[SpinManager] Reel 3 middle: {symbol3.symbolName}");

        if (symbol1 == symbol2 && symbol2 == symbol3)
        {
            // Win!
            int winAmount = walletManager.GetCurrentBet() * symbol1.payoutMultiplier;
            Debug.Log($"[SpinManager] ✓ WIN! Symbols match: {symbol1.symbolName} x{symbol1.payoutMultiplier} = ${winAmount}");
            GameEvents.Win(winAmount);
        }
        else
        {
            // Lose
            Debug.Log($"[SpinManager] ✗ LOSE! Symbols don't match");
            GameEvents.Lose();
            foreach (var reel in reels)
            {
                reel.PlayShakeAnimation();
            }
        }
    }

    public bool IsSpinning() => isSpinning;
}