using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// Manages all UI elements and updates
/// Single Responsibility: UI presentation
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI walletText;
    [SerializeField] private TextMeshProUGUI winWalletText;
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private Button increaseBetBtn;
    [SerializeField] private Button decreaseBetBtn;
    [SerializeField] private Button spinBtn;
    [SerializeField] private TextMeshProUGUI spinBtnText;
    [SerializeField] private GameObject winPopup;
    [SerializeField] private TextMeshProUGUI winPopupText;
    [SerializeField] private GameObject insufficientFundsText;

    [Header("References")]
    [SerializeField] private WalletManager walletManager;
    [SerializeField] private SpinManager spinManager;

    void Start()
    {
        // Initialize all UI to default values
        InitializeUI();
        SubscribeToEvents();
        SetupButtons();
        winPopup.SetActive(false);
        insufficientFundsText.SetActive(false);
    }

    void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void InitializeUI()
    {
        // Update all UI elements with current wallet and bet values
        UpdateWalletUI(walletManager.GetCurrentWallet());
        UpdateWinWalletUI(0);
        UpdateBetUI(walletManager.GetCurrentBet());
        spinBtnText.text = "SPIN";
    }

    private void SubscribeToEvents()
    {
        GameEvents.OnWalletChanged += UpdateWalletUI;
        GameEvents.OnWinWalletChanged += UpdateWinWalletUI;
        GameEvents.OnBetChanged += UpdateBetUI;
        GameEvents.OnSpinStarted += HandleSpinStarted;
        GameEvents.OnAllReelsStopped += HandleSpinEnded;
        GameEvents.OnWin += HandleWin;
    }

    private void UnsubscribeFromEvents()
    {
        GameEvents.OnWalletChanged -= UpdateWalletUI;
        GameEvents.OnWinWalletChanged -= UpdateWinWalletUI;
        GameEvents.OnBetChanged -= UpdateBetUI;
        GameEvents.OnSpinStarted -= HandleSpinStarted;
        GameEvents.OnAllReelsStopped -= HandleSpinEnded;
        GameEvents.OnWin -= HandleWin;
    }

    private void SetupButtons()
    {
        increaseBetBtn.onClick.AddListener(() => walletManager.IncreaseBet());
        decreaseBetBtn.onClick.AddListener(() => walletManager.DecreaseBet());
        spinBtn.onClick.AddListener(() => spinManager.RequestSpin());
    }

    private void UpdateWalletUI(int amount)
    {
        walletText.text = "$" + amount;
        insufficientFundsText.SetActive(!walletManager.CanPlaceBet());
    }

    private void UpdateWinWalletUI(int amount)
    {
        winWalletText.text = "$" + amount;
    }

    private void UpdateBetUI(int amount)
    {
        betText.text = "$" + amount;
    }

    private void HandleSpinStarted()
    {
        spinBtnText.text = "SPINNING...";
        SetButtonsInteractable(false);
    }

    private void HandleSpinEnded()
    {
        spinBtnText.text = "SPIN";
        SetButtonsInteractable(true);
    }

    private void HandleWin(int winAmount)
    {
        winPopupText.text = $"YOU WIN\n${winAmount}!";
        winPopup.SetActive(true);

        // Simple popup animation - scale up and down
        winPopup.transform.localScale = Vector3.zero;

        Sequence popupSeq = DOTween.Sequence();
        popupSeq.Append(winPopup.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack))
                .AppendInterval(1.5f)
                .Append(winPopup.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack))
                .OnComplete(() => winPopup.SetActive(false));
    }

    private void SetButtonsInteractable(bool interactable)
    {
        increaseBetBtn.interactable = interactable;
        decreaseBetBtn.interactable = interactable;
        spinBtn.interactable = interactable;
    }
}