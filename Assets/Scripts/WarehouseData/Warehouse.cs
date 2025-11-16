using UnityEngine;

public class Warehouse : MonoBehaviour
{
    /// <summary>
    /// 倉庫が保持する資源料
    /// </summary>
    public int StoredResources = 0;

    /// <summary>
    /// 保管出来る在庫の最大数
    /// </summary>
    private int maxStockAmount = 1000;

    /// <summary>
    /// 外部から倉庫が保管出来る在庫の最大値を取得する
    /// </summary>
    public int GetMaxStockAmount
    {
        get { return maxStockAmount; }
    }

    /// <summary>
    /// 引数の数、倉庫が保持する資源量を増やします
    /// </summary>
    /// <param name="amount"></param>
    public void Store(int amount)
    {
        StoredResources += amount;
        Debug.Log($"倉庫に{amount}納品" +
            $"(合計:{StoredResources})");
    }
}
