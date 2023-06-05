using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        itemList.Add(new Item(10001, "��ź", "����� �� ���� �� ����", Item.ItemType.Use));
        itemList.Add(new Item(20001, "�������� ����", "������ ��....", Item.ItemType.Equip));
        itemList.Add(new Item(30001, "�̻��� ����", "� ���� ���� ������?", Item.ItemType.ETC));
    }
}
