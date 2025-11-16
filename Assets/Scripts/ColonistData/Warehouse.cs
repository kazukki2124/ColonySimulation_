using UnityEngine;

public class Warehouse : MonoBehaviour
{
    /// <summary>
    /// 倉庫が保持する資源料
    /// </summary>
    public int StoredResources = 0;

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
