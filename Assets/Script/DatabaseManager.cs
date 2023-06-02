using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        itemList.Add(new Item(10001, "빨간 포션", "설명", Item.ItemType.Use));
    }
}
