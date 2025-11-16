using UnityEngine;
using TMPro;

public class WarehouseUI : MonoBehaviour
{
    public Warehouse Warehouse;
    public TextMeshProUGUI ResourcesText;

    private void Update()
    {
        // ‘qŒÉ“à‚ÌŽ‘Œ¹—Ê‚ð•\Ž¦
        ResourcesText.text = $"{Warehouse.StoredResources}/{Warehouse.GetMaxStockAmount}";
    }
}
