using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    public string BuildingName = "House";

    /// <summary>
    /// 作業が完了したときに生成するGameObject(HouseやBakeryなどの建物)
    /// </summary>
    public GameObject CompletedPrefab;

    /// <summary>
    /// コロニストに位置を知らせるためのTransformの変数
    /// </summary>
    public Transform BuildPoint;

    /// <summary>
    /// 工事が完了するのに必要な作業量
    /// </summary>
    public float RequiredWork = 100f;

    /// <summary>
    /// Workの仕事量に対して消費される資産
    /// </summary>
    public float ResourcePerWork = 1f;

    /// <summary>
    /// 資産の値を参照したいので、倉庫のコンポーネントを参照させる
    /// </summary>
    public Warehouse Warehouse;

    /// <summary>
    /// 今どれくらい作業しているか
    /// </summary>
    private float currentWork = 0f;

    /// <summary>
    /// 
    /// </summary>
    public bool IsCompleted
    {
        get { return currentWork >= RequiredWork; }
    }

    /// <summary>
    /// 現在の作業達成度(%)
    /// </summary>
    public float GetProgress
    {
        get { return currentWork / RequiredWork; }
    }

    public bool Build(float workAmount)
    {
        if (IsCompleted)
        {
            return true;
        }

        // 倉庫の資産を使いたいが、倉庫の参照がない場合、計算が出来ないのでfalseを返します
        if (Warehouse == null)
        {
            return false;
        }

        // 必要な資産を計算します
        float requiredResource = workAmount * ResourcePerWork;

        if (!Warehouse.HasEnough(requiredResource))
        {
            // 必要な資産がない場合はfalseを返します
            return false;
        }

        // 必要な資産を倉庫から抜き出します
        Warehouse.Withdrow((int)requiredResource);

        // ここから建築開始です。作業に作業量を追加します
        currentWork += workAmount;

        if (IsCompleted)
        {
            // 建築完了のメソッドを実行する
            ComoleteBuilding();
        }

        return true;
    }

    private void ComoleteBuilding()
    {
        Debug.Log($"{BuildingName}の建築が完了しました");
        // 完了時の建物が指定されていたら
        if (CompletedPrefab != null)
        {
            // CompletedPrefabをConstructionSiteの位置に生成します
            // 回転は0,0,0で生成→CompletedPrefabが元から持っている回転で生成します
            //Instantiate(何を生成するか,位置はどうするか,回転はどうするか);
            Instantiate(CompletedPrefab,this.transform.position,CompletedPrefab.transform.rotation);
        }
        // ConstructionSiteのObjectのアクティブを切ります
        this.gameObject.SetActive(false);
    }

    public Vector3 GetBuildPosition()
    {
        // buildPointが指定されていた場合はそれを返す
        if(BuildPoint != null)
        {
            return BuildPoint.position;
        }
        // そうじゃなかったら、ConstructionSiteの世界座標上の原点を返します
        return this.transform.position;
    }

}
