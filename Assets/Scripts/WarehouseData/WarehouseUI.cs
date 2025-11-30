using UnityEngine;
using TMPro;

public class WarehouseUI : MonoBehaviour
{
    public Warehouse Warehouse;
    public TextMeshProUGUI MinedResourcesText;

    private void Update()
    {
        // ‘qŒÉ“à‚ÌŽ‘Œ¹—Ê‚ð•\Ž¦
        MinedResourcesText.text = $"Mined resource : {Warehouse.StoredResources}/{Warehouse.GetMaxStockAmount}";
    }
}
