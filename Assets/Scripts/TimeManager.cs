using UnityEngine;
using UnityEngine.UI; //ƒ{ƒ^ƒ“‚âƒeƒLƒXƒg“™‚Ì•\¦‚Ég‚¤

public class TimeManager : MonoBehaviour
{
    public Button PauseButton;
    public Button PlayButton;//1”{‘¬
    public Button Speed2xButton;//2”{‘¬
    public Button Speed3xButton;//3”{‘¬

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ƒQ[ƒ€‚ªŠJn‚µ‚½‚Æ‚«‚Í“™”{‘¬‚É‚µ‚Ä‚¨‚­
        SetTimeScale(1f);
        PauseButton.onClick.AddListener(() => SetTimeScale(0f));   //PauseButton‚ª‰Ÿ‚³‚ê‚½‚ÉSetTimeScale‚ğ0‚É‚µ‚Ä’â~‚·‚é
        PlayButton.onClick.AddListener(() => SetTimeScale(1f));    //PlayButton‚ª‰Ÿ‚³‚ê‚½‚ÉSetTimeScale‚ğ1‚É‚µ‚Ä“™”{‘¬‚É‚·‚é
        Speed2xButton.onClick.AddListener(() => SetTimeScale(2f)); //Speed2xButton‚ª‰Ÿ‚³‚ê‚½‚ÉSetTimeScale‚ğ2‚É‚µ‚Ä2”{‘¬‚É‚·‚é
        Speed3xButton.onClick.AddListener(() => SetTimeScale(3f)); //Speed3xButton‚ª‰Ÿ‚³‚ê‚½‚ÉSetTimeScale‚ğ3‚É‚µ‚Ä3”{‘¬‚É‚·‚é
    }

    /// <summary>
    /// ŠÔ‚Ì”{‘¬İ’è‚ğˆø”‚Ì’l‚É‚æ‚Á‚Äs‚¤
    /// </summary>
    /// <param name="scale"></param>
    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Debug.Log($"TimeScale:{scale}");
        //F‚àİ’è
        SetButtonColer(scale);
    }

    private void SetButtonColer(float scale)
    {
        // ‰½”{‘¬‚ğ‰Ÿ‚µ‚½‚©
        switch (scale)
        {
            case 0f:
                PauseButton.image.color = Color.white;
                PlayButton.image.color = Color.gray5;
                Speed2xButton.image.color = Color.gray5;
                Speed3xButton.image.color = Color.gray5;
                break;

            case 1f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.white;
                Speed2xButton.image.color = Color.gray5;
                Speed3xButton.image.color = Color.gray5;
                break;

            case 2f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.gray5;
                Speed2xButton.image.color = Color.white;
                Speed3xButton.image.color = Color.gray5;
                break;

            case 3f:
                PauseButton.image.color = Color.gray5;
                PlayButton.image.color = Color.gray5;
                Speed2xButton.image.color = Color.gray5;
                Speed3xButton.image.color = Color.white;
                break;
        }
    }
}
