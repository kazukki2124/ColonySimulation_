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
    public Image healthBar;

    // Update is called once per frame
    void Update()
    {
        // healthBarに現在の体力/最大体力で出る割合を表示する
        healthBar.fillAmount = ColonistAI.GetCurrentHealth / ColonistAI.MaxHealth;
    }
}
