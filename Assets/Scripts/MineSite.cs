using UnityEngine;

public class MineSite : MonoBehaviour
{
    /// <summary>
    /// 採掘場に貯める共有資産
    /// </summary>
    public float SharedMinedResource = 0f;

    /// <summary>
    /// コロニストがリソースを追加する為の処理
    /// </summary>
    /// <param name="amount"></param>
    public void AddResouce(float amount)
    {
        SharedMinedResource += amount;
        //Mathf.Clamp(変数,最小値,最大値)で最小値から最大値までの範囲で変数を制限してくれます
        SharedMinedResource = Mathf.Clamp(SharedMinedResource,0,9999);
    }

    /// <summary>
    /// コロニストが採掘場からamount分撮っていく処理
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public float TakeResource(float amount)
    {
        // Mathf.Min(変数A,変数B)はどちらが少ないかを計算してくれます
        float taken = Mathf.Min(amount, SharedMinedResource);
        SharedMinedResource -= taken;
        // 採掘場から取得出来る資産の数を返す
        return taken;
    }
}
