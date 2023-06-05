using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        itemList.Add(new Item(10001, "폭탄", "사용할 수 있을 거 같다", Item.ItemType.Use));
        itemList.Add(new Item(20001, "누군가의 편지", "앞으로 가....", Item.ItemType.Equip));
        itemList.Add(new Item(30001, "이상한 열쇠", "어떤 곳에 쓰는 열쇠지?", Item.ItemType.ETC));
    }
}
