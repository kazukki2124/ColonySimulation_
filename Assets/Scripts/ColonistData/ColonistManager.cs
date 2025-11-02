using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistManager : MonoBehaviour
{
    /// <summary>
    /// []は配列と言い、一つの変数の中で複数のColonistAIを管理できます。
    /// </summary>
    public ColonistAI[] Colonists;

    /// <summary>
    /// コロニストのPrefab
    /// </summary>
    public GameObject ColonistPrefab;

    /// <summary>
    /// コロニストの数
    /// </summary>
    public int ColonistCount = 3;

    /// <summary>
    /// UnityEditorからColonistUIManager達を参照
    /// /// </summary>
    public ColonistUIManager[] ColonistUIManagers;

    private void Start()
    {
        //コロニストの数分のColositAIを準備
        Colonists = new ColonistAI[ColonistCount];
        //コロニストの数分の処理を実行する
        for (int i = 0; i < ColonistCount; i++)
        {
            //登場する位置を決める(1体目は原点、2体目はX軸補正の方向に2m...)
            Vector3 position = new Vector3(i * 2, 0, 0);
            //GameOjbjectをScene内に生成します
            GameObject instantiateObject = Instantiate(ColonistPrefab, position, Quaternion.identity);
            //一斉命令用のColonistAIを生成したGameObjectから取得
            Colonists[i] = instantiateObject.GetComponent<ColonistAI>();

            //コロニストのUI表示用のマネージャに生成されたColonistAIをセット
            ColonistUIManagers[i].SetColonistAI(Colonists[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //キーボードのスペースキーが押されたら
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //for文は(初期値,初期値が指定の値になるまで,初期値を増減させる)
            //初期値が指定の値になるまでの回数処理を行う
            for (int i = 0; i < Colonists.Length; i++)
            {
                Colonists[i].State = ColonistAI.ColonistState.Mine;
            }
        }
    }
}
