using UnityEngine;
using TMPro;

public class FoodStockUI : MonoBehaviour
{
    public Bakery Bakery;
    public TextMeshProUGUI FoodStockText;

    private void Update()
    {
        // ‘qŒÉ“à‚ÌŽ‘Œ¹—Ê‚ð•\Ž¦
        FoodStockText.text = $"FoodStock : {Bakery.FoodStock}";
    }
}
