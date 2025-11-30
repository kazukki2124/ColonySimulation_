using UnityEngine;
using UnityEngine.UI; //ボタンやテキスト等の表示に使う

public class TimeManager : MonoBehaviour
{
    public Button PauseButton;
    public Button PlayButton;//1倍速
    public Button Speed2xButton;//2倍速
    public Button Speed3xButton;//3倍速

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲームが開始したときは等倍速にしておく
        SetTimeScale(1f);
        PauseButton.onClick.AddListener(() => SetTimeScale(0f));   //PauseButtonが押された時にSetTimeScaleを0にして停止する
        PlayButton.onClick.AddListener(() => SetTimeScale(1f));    //PlayButtonが押された時にSetTimeScaleを1にして等倍速にする
        Speed2xButton.onClick.AddListener(() => SetTimeScale(2f)); //Speed2xButtonが押された時にSetTimeScaleを2にして2倍速にする
        Speed3xButton.onClick.AddListener(() => SetTimeScale(3f)); //Speed3xButtonが押された時にSetTimeScaleを3にして3倍速にする
    }

    /// <summary>
    /// 時間の倍速設定を引数の値によって行う
    /// </summary>
    /// <param name="scale"></param>
    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Debug.Log($"TimeScale:{scale}");
    }
}
