using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistManager : MonoBehaviour
{
    /// <summary>
    /// []は配列と言い、一つの変数の中で複数のColonistAIを管理できます。
    /// </summary>
    public ColonistAI[] Colonists;

    // Update is called once per frame
    void Update()
    {
        //キーボードのスペースキーが押されたら
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //for文は(初期値,初期値が指定の値になるまで,初期値を増減させる)
            //初期値が指定の値になるまでの回数処理を行う
            for (int i = 0;i<Colonists.Length;i++)
            {
                Colonists[i].State = ColonistAI.ColonistState.Mine;
            }
        }
    }
}
