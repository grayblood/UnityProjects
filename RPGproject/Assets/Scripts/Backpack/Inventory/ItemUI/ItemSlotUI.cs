using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{

    [SerializeField] Text nameText;
    [SerializeField] Text countText;

    public void setData(ItemSlot itemSlot)
    {
        nameText.text = itemSlot.Item.Name;
        countText.text = $"X{itemSlot.Count}";

    }


    




}
