using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobSwitchUI : MonoBehaviour
{
    /// <summary>
    /// ColonistAIに直接、Jobの変更を行うため
    /// </summary>
    public ColonistAI ColonistAI;

    public Button SwitchButton;

    /// <summary>
    /// Jobの名前を表示するための機能
    /// </summary>
    public TextMeshProUGUI JobLabel;

    /// <summary>
    /// ColonistUIManagerさんから呼ばれることを想定
    /// </summary>
    /// <param name="colonistAI"></param>
    public void SetSwitchUI(ColonistAI colonistAI)
    {
        this.ColonistAI = colonistAI;
        SwitchButton.onClick.AddListener(ToggleJob);
        UpdateLabel();
    }

    public void ToggleJob()
    {
        // コロニストのJobが採掘者(Miner)だったら
        if (ColonistAI.Job == ColonistAI.JobType.Miner)
        {
            // 運搬者(Carrier)に変更する
            ColonistAI.Job = ColonistAI.JobType.Carrier;
        }
        // コロニストのJobが採掘者(Miner)ではなく運搬者(Carrier)だったら
        else if (ColonistAI.Job == ColonistAI.JobType.Carrier)
        {
            // 建築作業員(Builder)に変更する
            ColonistAI.Job = ColonistAI.JobType.Builder;
        }
        else if (ColonistAI.Job == ColonistAI.JobType.Builder)
        {
            // 採掘者(Miner)に変更する
            ColonistAI.Job = ColonistAI.JobType.Miner;
        }

        UpdateLabel();
    }

    /// <summary>
    /// JobLabelにColonistAI.Jobの文字を表示する
    /// </summary>
    void UpdateLabel()
    {
        JobLabel.text = $"job:{ColonistAI.Job}";
    }
}
