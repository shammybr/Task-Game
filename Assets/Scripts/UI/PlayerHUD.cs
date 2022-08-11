using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public void UpdateMoney(int Money)
    {
        MoneyText.text = Money.ToString();
    }
}
