using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistAnimationcontroller : MonoBehaviour
{
    /// <summary>
    /// 住人のアニメーター
    /// </summary>
    public Animator CollonistAnimator;

    // Update is called once per frame
    void Update()
    {
        //待機アニメーションデバッグ用
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            PlayIdloAnimation();
        }

        //歩くアニメーションデバッグ用
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            PlayWalkingAnimation();
        }

        //採掘アニメーションデバッグ用
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            PlayMineAnimation();
        }

        //採掘アニメーションデバッグ用
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            PlaySleepingAnimation();
        }

        //休憩アニメーションデバッグ用
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            PlayRestAnimation();
        }

        //死亡アニメーションデバッグ用
        if (Keyboard.current.digit1Key.wasPressedThisFrame
            && Keyboard.current.minusKey.wasPressedThisFrame)
        {
            PlayDeathAnimation();
        }
    }

    /// <summary>
    /// 待機アニメーションを再生
    /// </summary>
    public void PlayIdloAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", 0);
    }

    /// <summary>
    /// 歩くアニメーションを再生
    /// </summary>
    public void PlayWalkingAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", 1);
    }

    /// <summary>
    /// 採掘アニメーションを再生
    /// </summary>
    public void PlayMineAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", 2);
    }

    /// <summary>
    /// 眠るアニメーションを再生
    /// </summary>
    public void PlaySleepingAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", 3);
    }

    /// <summary>
    /// 休憩アニメーションを再生
    /// </summary>
    public void PlayRestAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", 5);
    }

    /// <summary>
    /// 死亡時アニメーションを再生
    /// </summary>
    public void PlayDeathAnimation()
    {
        CollonistAnimator.SetInteger("AnimationState", -1);
        CollonistAnimator.SetTrigger("DeathTrigger");
    }

}
