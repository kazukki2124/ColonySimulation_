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

    void Start()
    {
        //コロニストの状態をIdle(待機)から始める
        State = ColonistState.Idle;
        //現在の体力をMAXにする
        currentHealth = MaxHealth;
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
                currentHealth += 2f * Time.deltaTime;

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
                currentHealth -= 5f * Time.deltaTime;

                //現在の体力が20ポイントを下回ったら
                if (currentHealth <= 20f)
                {
                    State = ColonistState.Sleep;
                }

                // if文はもし、小括弧内の条件だったら、中括弧内の処理を行う
                // 自分の位置と、ターゲットの位置が10cmより近くなったら
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    // 次の行動を行う
                    State = ColonistState.Mine;
                    //掘削時間が1～5秒の間でランダムになる。
                    timer = Random.Range(3f,5f);
                }
                break;

            case ColonistState.Mine:
                //仮で採掘アニメーション再生の代わりにログを出力します
                Debug.Log("Colonist is mining!");

                //毎フレーム回転させ続ける
                transform.Rotate(Vector3.up * 30f * Time.deltaTime);

                //現在の体力を秒間10ポイント減少させる
                currentHealth -= 10f * Time.deltaTime;

                //現在の体力が20ポイントより少なくなったら
                if (currentHealth <= 20f)
                {
                    //体力を回復させるためにSleepにする
                    State = ColonistState.Sleep;
                }

                if(timer <= 0f)
                {
                    //State = ColonistState.Idle;
                    //timer = 2f;
                    //StateをColonistState.Sleepに代入してください。
                    State = ColonistState.Idle;
                    //timerを10秒～15秒で設定してください。
                    timer = Random.Range(10f,15f);
                }
                break;

            case ColonistState.Sleep:
                //体力を秒間8ポイント回復させる
                currentHealth += 8f * Time.deltaTime;

                //もし、コロニストの体力が完全に回復したら
                if (currentHealth >= MaxHealth)
                {
                    State = ColonistState.Idle;
                    //timerを1秒～5秒で設定してください。
                    timer = Random.Range(1f,5f);
                }
                break;
        }
    }
}
