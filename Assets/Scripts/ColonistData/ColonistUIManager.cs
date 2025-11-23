using TMPro;
using UnityEngine;

public class ColonistUIManager : MonoBehaviour
{
    private ColonistHealthUI colonistHealthUI;

    private ColonistStatusUI colonistStatusUI;

    private JobSwitchUI switchUI;

    public TextMeshProUGUI NameTest;

    /// <summary>
    /// Awake()はStart()を実行される前に、実行される初期化用のメソッドです
    /// </summary>
    void Awake()
    {
        //GetComponentInChildrenはHierarchyWindowの
        //このコンポーネントが追加されたgameObjectの階層下から取得する
        colonistHealthUI = GetComponentInChildren<ColonistHealthUI>();
        colonistStatusUI = GetComponentInChildren<ColonistStatusUI>();
        switchUI = GetComponentInChildren<JobSwitchUI>();
    }

    //ColonistUIManager君が持っている2つのコンポーネントにColonistAIを渡してあげたい
    //小括弧の中身は引数と言って、引数に渡されたものは、この処理の中で使うことが出来る
    public void SetColonistAI(ColonistAI colonistAI)
    {
        colonistHealthUI.ColonistAI = colonistAI;
        colonistStatusUI.ColonistAI = colonistAI;

        // JobSwitchUIにcolonistAIを割り当てる
        switchUI.SetSwitchUI(colonistAI);

        //名前の表示をおこなう
        NameTest.text = colonistAI.gameObject.name;
    }
}
