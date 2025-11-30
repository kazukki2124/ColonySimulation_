using UnityEngine;
// UnityEngineのUIを使う宣言
using UnityEngine.UI;

public class ColonistHealthUI : MonoBehaviour
{
    /// <summary>
    /// 体力を参照するため
    /// </summary>
    public ColonistAI ColonistAI;

    /// <summary>
    /// 体力表示用のバー
    /// </summary>
    public Image HealthBar;

    /// <summary>
    /// ストレス値用のバー
    /// </summary>
    public Image StressBar;

    /// <summary>
    /// 空腹値用のバー
    /// </summary>
    public Image HungerBar;

    // Update is called once per frame
    void Update()
    {
        // healthBarに現在の体力/最大体力で出る割合を表示する
        HealthBar.fillAmount = ColonistAI.GetCurrentHealth / ColonistAI.MaxHealth;

        // stressBarに現在のストレス値/100で出る割合を表示する
        StressBar.fillAmount = ColonistAI.GetStress / 100;

        // stressBarに現在の空腹値/100で出る割合を表示する
        HungerBar.fillAmount = ColonistAI.GetHunger / 100;
    }
}
