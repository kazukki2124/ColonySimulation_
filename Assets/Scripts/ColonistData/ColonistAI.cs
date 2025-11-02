using UnityEngine;

public class ColonistAI : MonoBehaviour
{
    /// <summary>
    /// enum型で宣言したコロニストの状態
    /// </summary>
    public enum ColonistState
    {
        Idle,   //待機
        Move,   //移動
        Mine,   //採掘
        Sleep   //就寝
    }

    public ColonistState State;

    /// <summary>
    /// コロニストの状態を変更するためのタイマー
    /// [SerializeField]のようなものを属性(Attribute)という
    /// </summary>
    [SerializeField]
    private float timer = 2f;

    public float MoveSpeed = 2.0f;
    private Vector3 targetPosition = new Vector3(2, 0, 2);

    /// <summary>
    /// 最大体力値
    /// </summary>
    public float MaxHealth = 100f;

    /// <summary>
    /// 現在の体力値
    /// </summary>
    [SerializeField]
    private float currentHealth;

    /// <summary>
    /// 外部から現在の体力を取得させるためのプロパティ
    /// </summary>
    public float GetCurrentHealth
    {
        get { return currentHealth; }
    }

    /// <summary>
    /// 疲労回復速度
    /// </summary>
    public float RecoveryRate = 1f;

    /// <summary>
    /// 疲れやすさ
    /// </summary>
    public float FatigueRate = 1f;

    /// <summary>
    /// コロニストの年齢
    /// </summary>
    public int ColonistAge = 20;

    /// <summary>
    /// 年齢によってコロニストの色を変更する
    /// </summary>
    public Material YoungMaterial;
    public Material NomalMaterial;
    public Material OldMaterial;

    /// <summary>
    /// Colonistの3Dモデル表示部分
    /// </summary>
    private MeshRenderer[] colonistMeshRenderers = new MeshRenderer[2];

    void Start()
    {
        //コロニストの状態をIdle(待機)から始める
        State = ColonistState.Idle;
        //現在の体力をMAXにする
        currentHealth = MaxHealth;

        //3D表示部分を取得
        colonistMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        //コロニストの年齢を決める
        ColonistAge = Random.Range(18, 70);
        //コロニストの年齢が20まで
        if (ColonistAge < 20)
        {
            RecoveryRate = 2f;
            FatigueRate = 0.5f;
            MoveSpeed = 5.0f;

            //foreach文は配列に対して、全ての要素に変更を加えたい時に使用
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = YoungMaterial;
            }
        }
        //上のifの条件を満たしていなかったら
        else if (ColonistAge < 40)
        {
            RecoveryRate = 1f;
            FatigueRate = 1f;
            MoveSpeed = 2.0f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = NomalMaterial;
            }
        }
        //上のif、else ifの条件を満たしていなかったら
        else
        {
            RecoveryRate = 0.5f;
            FatigueRate = 2f;
            MoveSpeed = 1.0f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = OldMaterial;
            }
        }
    }

    void Update()
    {
        //1フレームにかかった時間をtimerから減算していきます
        timer -= Time.deltaTime;

        //小括弧の中の値(変数)を使って処理を分岐させます
        switch (State)
        {
            case ColonistState.Idle:

                //現在の体力をじわじわっと回復させる
                currentHealth += RecoveryRate * 2f * Time.deltaTime;

                //caseとbreakの間に、caseの場合の処理を書く
                //もしtimerが0秒を下回ったら
                if (timer <= 0f)
                {
                    //コロニストの状態を"動く"という状態に変更
                    State = ColonistState.Move;
                    //次のターゲットポジションを決める
                    targetPosition = new Vector3(
                        Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    timer = 2f;
                }
                break;

            case ColonistState.Move:
                transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                //現在の体力値から1秒観で5ポイント体力を減らします
                currentHealth -= FatigueRate * 5f * Time.deltaTime;

                //現在の体力が20ポイントを下回ったら
                if (currentHealth <= 20f)
                {
                    //体力を回復するためにSleepにする
                    State = ColonistState.Sleep;
                }

                // if文はもし、小括弧内の条件だったら、中括弧内の処理を行う
                // 自分の位置と、ターゲットの位置が10cmより近くなったら
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    // 次の行動を行う
                    State = ColonistState.Mine;
                    //掘削時間が1～5秒の間でランダムになる。
                    timer = Random.Range(3f, 5f);
                }
                break;

            case ColonistState.Mine:
                //仮で採掘アニメーション再生の代わりにログを出力します
                Debug.Log("Colonist is mining!");

                //毎フレーム回転させ続ける
                transform.Rotate(Vector3.up * 30f * Time.deltaTime);

                //現在の体力を秒間10ポイント減少させる
                currentHealth -= FatigueRate * 10f * Time.deltaTime;

                //現在の体力が20ポイントより少なくなったら
                if (currentHealth <= 20f)
                {
                    //体力を回復させるためにSleepにする
                    State = ColonistState.Sleep;
                }

                if (timer <= 0f)
                {
                    //State = ColonistState.Idle;
                    //timer = 2f;
                    //StateをColonistState.Sleepに代入してください。
                    State = ColonistState.Idle;
                    //timerを10秒～15秒で設定してください。
                    timer = Random.Range(2f, 5f);
                }
                break;

            case ColonistState.Sleep:
                //体力を秒間8ポイント回復させる
                currentHealth += RecoveryRate * 8f * Time.deltaTime;

                //もし、コロニストの体力が完全に回復したら
                if (currentHealth >= MaxHealth)
                {
                    State = ColonistState.Idle;
                    //timerを1秒～5秒で設定してください。
                    timer = Random.Range(1f, 5f);
                }
                break;
        }
    }
}
