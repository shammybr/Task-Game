using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Money;
    ItemData.EItemID _equippedHat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public ItemData.EItemID EquippedHat
    {
        get => _equippedHat;
    }

    public void EquipHat(ItemData.EItemID HatID)  {


    }
}
