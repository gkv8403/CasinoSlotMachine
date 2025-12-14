using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controls lever animation with standing and pull GameObjects
/// Enhanced with more impactful animations
/// </summary>
public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject standingLever;  // Standing position GameObject
    [SerializeField] private GameObject pullLever;      // Pull position GameObject
    [SerializeField] private RectTransform standingRect;
    [SerializeField] private RectTransform pullRect;

    [Header("Pull Animation Settings")]
    [SerializeField] private float pullDownDuration = 0.3f;  // Fast downward motion
    [SerializeField] private float pullHoldDuration = 0.4f;  // Hold at pulled position
    [SerializeField] private float resetDuration = 0.4f;     // Spring back

    [SerializeField] private float squashAmount = 0.4f;      // How much to squash (0.4 = 40%)
    [SerializeField] private float pullDownDistance = 50f;   // How far to pull down

    private Vector3 standingOriginalScale;
    private Vector3 standingOriginalPos;

    void Start()
    {
        standingOriginalPos = standingRect.localPosition;
        standingOriginalScale = standingRect.localScale;

        // Set initial state - standing lever visible, pull lever hidden
        standingLever.SetActive(true);
        pullLever.SetActive(false);

        // Ensure pivot is at top for proper squashing
        standingRect.pivot = new Vector2(0.5f, 1f); // Top center

        GameEvents.OnLeverPulled += PullLever;
        GameEvents.OnLeverReset += ResetLever;
    }

    void OnDestroy()
    {
        GameEvents.OnLeverPulled -= PullLever;
        GameEvents.OnLeverReset -= ResetLever;
    }

    private void PullLever()
    {
        Sequence pullSeq = DOTween.Sequence();

        // Phase 1: Quick pull down with squash (creates impact)
        pullSeq.Append(standingRect.DOScaleY(squashAmount, pullDownDuration)
                      .SetEase(Ease.InQuad))
               .Join(standingRect.DOLocalMoveY(standingOriginalPos.y - pullDownDistance, pullDownDuration)
                      .SetEase(Ease.InQuad))

               // Phase 2: Brief bounce effect at bottom
               .Append(standingRect.DOLocalMoveY(standingOriginalPos.y - pullDownDistance + 5f, 0.08f)
                      .SetEase(Ease.OutQuad))
               .Append(standingRect.DOLocalMoveY(standingOriginalPos.y - pullDownDistance, 0.08f)
                      .SetEase(Ease.InQuad))

               // Phase 3: Smooth transition to pull lever image
               .AppendCallback(() =>
               {
                   standingLever.SetActive(false);
                   pullLever.SetActive(true);
               })

               // Phase 4: Hold at pulled position
               .AppendInterval(pullHoldDuration);
    }

    private void ResetLever()
    {
        Sequence resetSeq = DOTween.Sequence();

        // Phase 1: Transition back to standing lever
        resetSeq.AppendCallback(() =>
        {
            pullLever.SetActive(false);
            standingLever.SetActive(true);
        })

        // Phase 2: Spring back with overshoot for satisfying feel
        .Append(standingRect.DOLocalMove(standingOriginalPos, resetDuration)
                .SetEase(Ease.OutElastic))
        .Join(standingRect.DOScaleY(standingOriginalScale.y, resetDuration)
                .SetEase(Ease.OutElastic));
    }
}