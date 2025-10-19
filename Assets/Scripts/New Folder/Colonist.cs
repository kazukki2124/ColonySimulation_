using UnityEngine;

public class Colonist : MonoBehaviour
{
    public string Name = "taro";
    public float MoveSpeed = 2.0f;

    private Vector3 targetPosition = new Vector3(2, 0, 2);

    // ゲームの最初に一回だけ実行される
    void Start()
    {
        Debug.Log($"{Name}がコロニーに到着しました");
        SetRandomTarget();
    }
    // ゲームの実行中、常に実行される
    void Update()
    {
        // 移動したい。移動するのに必要なのは自分の位置と、行くべき位置
        //自分の位置をtargetPositionまで移動させる
        //.は接続詞と考えてもらったらOK "transform.position"は"transformのposition"
        transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        // if文はもし、小括弧内の条件だったら、中括弧内の処理を行う
        // 自分の位置と、ターゲットの位置が10cmより近くなったら
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 次のターゲットの位置を決める
            SetRandomTarget();
        }
    }

    //うろつく位置を決める処理を書く
    void SetRandomTarget()
    {
        targetPosition = new Vector3(
            Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
    }
}
