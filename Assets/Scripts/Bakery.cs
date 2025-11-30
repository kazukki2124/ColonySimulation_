using UnityEngine;

public class Bakery : MonoBehaviour
{
    public float FoodStock = 100f;

    /// <summary>
    /// 倉庫資源10 → 食料1に変えるレート
    /// </summary>
    public float ExchangeRate = 10f;

    /// <summary>
    /// 食料の生産速度(毎秒)
    /// </summary>
    public float ProduceRate = 20f;

    /// <summary>
    /// 倉庫の中身を見たいので参照する
    /// </summary>
    public Warehouse Warehouse;

    /// <summary>
    /// 時間を測るのに必要なタイマー
    /// </summary>
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (ProduceRate <= timer)
        {
            ExchangeWithWarehouse();
            //timerをリセットする
            timer = 0f;
        }
    }

    public void ExchangeWithWarehouse()
    {
        if (Warehouse == null)
        {
            //ログの説明
            //参照されていなかったりすると困るのでWarningで注意喚起する
            Debug.LogWarning("WarehouseがUnityで設定されていません");
            //LogErrorにすると、ゲームがストップします
            //Debug.LogError("WarehouseがUnityで設定されていません");
            return;
        }
        //倉庫に十分な在庫があった時
        if (Warehouse.HasEnough(ExchangeRate))
        {
            //毎秒倉庫から交換を行う
            Warehouse.Withdrow((int)ExchangeRate);
            //毎秒、FoodStockをProduceRateに合わせて加算していく
            FoodStock += ProduceRate;
        }
    }

    /// <summary>
    /// ベーカリーで食事ができるかどうか
    /// </summary>
    /// <returns></returns>
    public bool CanEat()
    {
        return FoodStock > 0;
    }

    public void Eat()
    {
        FoodStock -= Time.deltaTime;
    }
}
