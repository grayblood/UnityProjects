using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] List<ItemSlot> slots;

    public List<ItemSlot> Slots => slots;

    public static Inventario GetInventario()
    {
        return FindObjectOfType<ItemController>().GetComponent<Inventario>();
    }
}

[Serializable]
public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;


    public ItemBase Item => item;
    public int Count => count;
}