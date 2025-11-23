using UnityEngine;

public class ColonistAI : MonoBehaviour
{
    /// <summary>
    /// enum型で宣言したコロニストの状態
    /// </summary>
    public enum ColonistState
    {
        Idle,   // 待機
        Move,   // 移動
        Mine,   // 採掘
        Sleep,  // 就寝
        Carry,  // 運ぶ
        Rest,   // 休憩
        Eat,    // 食事
        Dead    // 死亡
    }

    public ColonistState State;

    public enum JobType
    {
        Invalid = -1,   // 定義されていない
        Miner,          // 採掘者
        Carrier         //運搬者
    }

    // 一旦全ての住人は採掘者とします
    public JobType Job = JobType.Miner;

    /// <summary>
    /// コロニストの状態を変更するためのタイマー
    /// [SerializeField]のようなものを属性(Attribute)という
    /// </summary>
    [SerializeField]
    private float timer = 2f;

    public float MoveSpeed = 2.0f;
    private Vector3 targetPosition = new Vector3(2, 1, 2);

    /// <summary>
    /// 採掘場所の位置
    /// </summary>
    public Vector3 MinePoint;

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

    /// <summary>
    /// 掘削スキルで高いほど速い
    /// </summary>
    [Range(0.5f, 3f)]
    public float MiningSkill = 1f;

    /// <summary>
    /// 採掘量
    /// </summary>
    public int MinedResource = 0;

    /// <summary>
    /// 空腹度
    /// </summary>
    [SerializeField]
    private float hunger = 100f;

    /// <summary>
    /// ストレス
    /// </summary>
    private float stress = 0f;

    /// <summary>
    /// 生きているかの判定
    /// </summary>
    public bool IsAlive
    {
        // boolは真偽の判定になるので、条件を作ることが出来ます
        // 今回は体力が合って、空腹度も飢えていない状態とします
        // ||は日本語でいうと、"か"とか"もしくは"です
        get { return currentHealth > 0 || hunger > 0; }
    }

    /// <summary>
    ///  倉庫
    /// </summary>
    public Transform Warehouse;

    /// <summary>
    ///  市場の位置
    /// </summary>
    public Transform MarketPosition;

    /// <summary>
    /// ベーカリー(食事をする場所)の位置
    /// </summary>
    public Transform BakeryPosition;

    /// <summary>
    /// ベーカリーの機能
    /// </summary>
    public Bakery Bakery;

    /// <summary>
    /// 採掘場の機能
    /// </summary>
    public MineSite MineSite;

    /// <summary>
    /// 運搬中の採掘資産
    /// </summary>
    private float carryingAmount = 0f;

    /// <summary>
    /// コロニストが持てる採掘資産の最大値
    /// </summary>
    private float carryingCapacity = 10f;

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
        if (ColonistAge < 30)
        {
            RecoveryRate = 2f;
            FatigueRate = 0.5f;
            MoveSpeed = 5.0f;
            MiningSkill = 3f;
            //foreach文は配列に対して、全ての要素に変更を加えたい時に使用
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = YoungMaterial;
            }
        }
        //上のifの条件を満たしていなかったら
        else if (ColonistAge < 50)
        {
            RecoveryRate = 1f;
            FatigueRate = 1f;
            MoveSpeed = 2.0f;
            MiningSkill = 2f;
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
            MiningSkill = 1f;
            foreach (var renderer in colonistMeshRenderers)
            {
                renderer.material = OldMaterial;
            }
        }
    }

    void Update()
    {
        // !は否定の意味です。!(bool型の変数)で、
        // bool型の変数の反対の判定をします
        // 生存していなかったら
        if (!IsAlive)
        {
            State = ColonistState.Dead;
            Debug.Log($"{name}は死亡しました");
            return;
        }
        //1フレームにかかった時間をtimerから減算していきます
        timer -= Time.deltaTime;

        // 1秒間に2ポイントずつ、空腹になっていきます
        hunger -= 2f * Time.deltaTime;

        // 1秒に1ポイントずつ、ストレスがかかっていきます
        stress += 1f * Time.deltaTime;

        // ストレスが限界(100)を越えたら勝手に休憩に入る
        if (stress >= 100f)
        {
            Debug.Log($"{name}はストレスが限界です!休憩に入ります！");
            State = ColonistState.Rest;
        }
        // 空腹度が限界(30)を下回ったら勝手に休憩に入る
        else if (hunger <= 30f)
        {
            Debug.Log($"{name}は空腹です!休憩に入ります！");
            State = ColonistState.Eat;
        }

        //小括弧の中の値(変数)を使って処理を分岐させます
        switch (State)
        {
            case ColonistState.Idle:
                HandleIdle();
                break;

            case ColonistState.Move:
                HandleMove();
                break;

            case ColonistState.Mine:
                HandleMine();
                break;

            case ColonistState.Carry:
                HandleCarry();
                break;

            case ColonistState.Rest:
                HandleRest();
                break;

            case ColonistState.Eat:
                HandleEat();
                break;

            case ColonistState.Sleep:
                HandleSleep();
                break;
        }

        // Mathf.Clamp(固定する値,最小値,最大値)で最小値から最大値までの値に
        // 制限してくれます
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);
    }

    /// <summary>
    /// 待機中の行動
    /// </summary>
    private void HandleIdle()
    {
        //現在の体力をじわじわっと回復させる
        currentHealth += RecoveryRate * 2f * Time.deltaTime;

        //caseとbreakの間に、caseの場合の処理を書く
        //もしtimerが0秒を下回ったら
        if (timer <= 0f)
        {
            //コロニストの状態を"動く"という状態に変更
            State = ColonistState.Move;
            //移動場所を採掘場へ指定する
            targetPosition = MinePoint;
            timer = 2f;
        }
    }

    /// <summary>
    /// 移動中の行動
    /// </summary>
    private void HandleMove()
    {
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
    }

    /// <summary>
    /// 採掘中の行動
    /// </summary>
    private void HandleMine()
    {
        // もしジョブが運搬車だったら
        if (Job == JobType.Carrier)
        {
            // 採掘場の共有資産が自分が持てるキャパシティに到達しているか
            if (MineSite.SharedMinedResource >= carryingCapacity)
            {
                // 自分が持てるキャパシティ分を採掘場から取得してくる
                carryingAmount = MineSite.TakeResource(carryingCapacity);
                Debug.Log($"{name}が{carryingAmount}分、資源を回収しました");
                // 取得出来たら運ぶという状態にします
                State = ColonistState.Carry;
                // 移動先を倉庫の位置にする
                targetPosition = Warehouse.position;
                // ここから下の処理を行わない
                return;
            }
        }
        //仮で採掘アニメーション再生の代わりにログを出力します
        Debug.Log("Colonist is mining!");

        //毎フレーム回転させ続ける
        //1秒間にMiningSkillが3の人は360°回転できる
        transform.Rotate(Vector3.up * 120f * MiningSkill * Time.deltaTime);

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
            int mined = Mathf.RoundToInt(10 * MiningSkill);
            // MinedResource += mined;
            // Debug.Log($"採掘完了{mined}(合計{MinedResource})");
            MineSite.AddResouce(mined);
            Debug.Log($"採掘完了{mined}" +
                $"(合計{MineSite.SharedMinedResource})");
            MinedResource = 0;

            //timerを10秒～15秒で設定してください。
            timer = Random.Range(1f, 5f);
            // もしJobが採掘者だったら
            if (Job == JobType.Miner)
            {
                // 掘り終わったらもう一度採掘します
                State = ColonistState.Mine;
            }
            else if (Job == JobType.Carrier)
            {
                // 掘り終わったらもう一度採掘します
                State = ColonistState.Carry;
                //移動先を倉庫の位置にする
                targetPosition = Warehouse.position;

                // 採掘場の共有資産が自分が持てるキャパシティに到達しているか
                if (MineSite.SharedMinedResource >= carryingCapacity)
                {
                    // 自分が持てるキャパシティ分を採掘場から取得してくる
                    carryingAmount = MineSite.TakeResource(carryingCapacity);
                    Debug.Log($"{name}が{carryingAmount}分、資源を回収しました");
                }
                else
                {
                    // 採掘場の共有資産が自分のキャパシティに到達していなかったら、
                    // 自分も採掘を行う
                    State = ColonistState.Mine;
                }
            }
        }
    }

    /// <summary>
    /// 運搬中の行動
    /// </summary>
    private void HandleCarry()
    {
        transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        //体力が回復するまで休ませるか。
        //体力があったらもう一回Moveにして採掘場に向かわせるか
        //休憩場に行って、休憩する

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 倉庫に資源を置く
            // まず倉庫のコンポーネント(機能)を取得する
            Warehouse warehouse = Warehouse.GetComponentInChildren<Warehouse>();
            // もし、倉庫のコンポーネントが見つかったら
            if (warehouse != null)
            {
                // 倉庫に採掘した量を追加する
                // carringAmountはfloatなのでint型でキャストします
                // キャストとは(型)変数で変数を型に変換します
                // 今回はfloat(小数点付きの値)をint(変数)に変換しました
                warehouse.Store((int)carryingAmount);
                // 倉庫に置いたので、運搬中の採掘量を0にする
                carryingAmount = 0;
            }

            targetPosition = MarketPosition.position;
            // 体力があった場合
            if (currentHealth > 50)
            {
                targetPosition = MinePoint;
                State = ColonistState.Move;
            }
            // 体力が危ない場合
            else
            {
                // 次の行動を行う(休憩)
                State = ColonistState.Rest;
            }
            timer = Random.Range(3f, 5f);
        }
    }

    /// <summary>
    /// 休憩中の行動
    /// </summary>
    private void HandleRest()
    {
        transform.position = Vector3.MoveTowards(
        transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ターゲットポジションが市場じゃなかったら市場に変更する
            if (targetPosition != MarketPosition.position)
            {
                targetPosition = MarketPosition.position;
            }
            //ストレスも1秒間に5ポイント緩和
            stress -= 5f * Time.deltaTime;
            //現在の体力をじわじわっと回復させる
            currentHealth += RecoveryRate * 2f * Time.deltaTime;

            //体力と空腹度が80より回復したら
            if (currentHealth > 80f && stress < 0f)
            {
                stress = 0f;
                timer = 1f;
                //状態を待機状態に戻す
                State = ColonistState.Idle;
            }
        }
    }

    /// <summary>
    /// 睡眠中の行動
    /// </summary>
    private void HandleSleep()
    {
        // 体力を秒間8ポイント回復させる
        currentHealth += hunger * 8f * Time.deltaTime;

        // ストレスも1秒間に5ポイントずつ減少していく
        stress -= 5f * Time.deltaTime;

        // もし、コロニストの体力が完全に回復したら
        if (currentHealth >= MaxHealth)
        {
            State = ColonistState.Idle;
            //timerを1秒～5秒で設定してください。
            timer = Random.Range(1f, 5f);
        }
    }

    /// <summary>
    /// 食事中の行動
    /// </summary>
    private void HandleEat()
    {
        if (targetPosition != BakeryPosition.position)
        {
            targetPosition = BakeryPosition.position;
        }

        transform.position = Vector3.MoveTowards(
        transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ベーカリーで食事が出来た場合
            if (Bakery.CanEat())
            {
                // 食事を行いFoodStockを減少させる
                Bakery.Eat();

                // 食事の場所に行ったら
                hunger += 20f * Time.deltaTime;

                // ストレスも1秒間に5ポイントずつ減少していく
                stress -= 5f * Time.deltaTime;

                // 体力も回復させる
                currentHealth += 2f * RecoveryRate * Time.deltaTime;


            }
            else // 食料がない・・・
            {
                // 体力が回復できない
                currentHealth += 2f * hunger * Time.deltaTime;
            }

            if (hunger >= 100f)
            {
                hunger = 100;
                Debug.Log($"{name}は満腹になりました");
                State = ColonistState.Idle;
            }
        }
    }
}
