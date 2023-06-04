using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private DatabaseManager theDatabase;
    private OrderManager theOrder;
    private InventorySlot[] slots;

    private List<Item> inventoryItemList; //�÷��̾ ������ ������ ����Ʈ
    private List<Item> inventoryTabList; //������ �ǿ� ������ ����Ʈ

    public TextMeshProUGUI Description_Text;
    public string[] tabDescription;

    public Transform tf; //slot �θ�ü

    public GameObject go; //�κ��丮 Ȱ��ȭ ��Ȱ��ȭ
    public GameObject[] selectedTabImages;

    private int selectedItem; //���õ� ������
    private int selectedTab; //���õ� ��

    private bool activated; //�κ��丮 Ȱ��ȭ�� true
    private bool tabActivated; //�� Ȱ��ȭ�� true
    private bool itemActivated; //������ Ȱ��ȭ�� true
    private bool stopKeyInput; //Ű �Է� ����
    private bool preventExec; //�ߺ����� ����.

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theDatabase = FindObjectOfType<DatabaseManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();

    }
    public void GetAnItem(int _itemID, int _Count = 1)
    {
        for(int i=0; i < theDatabase.itemList.Count; i++) // �����ͺ��̽� ������ �˻�
        {
            if (_itemID == theDatabase.itemList[i].itemID) //�����ͺ��̽��� ������ �߰�
            {
                for(int j=0; j < inventoryItemList.Count; j++) //����ǰ�� ���� �������� �ִ��� �˻�
                {
                    if (inventoryItemList[j].itemID == _itemID) //����ǰ�� ���� �������� �ִ�-> ������ ����������
                    {
                        if(inventoryItemList[j].itemType == Item.ItemType.Use)
                        {
                            inventoryItemList[j].itemType += _Count;
                        }
                        else
                        {
                            inventoryItemList.Add(theDatabase.itemList[i]);
                        }
                        inventoryItemList[j].itemCount += _Count;
                        return;
                    }
                }
                inventoryItemList.Add(theDatabase.itemList[i]); // �����㿡 �ش� ������ �߰�.
                return;
            }
        }
        Debug.LogError("�����ͺ��̽��� �ش� ID���� ���� �������� �������� �ʽ��ϴ�."); //�����ͺ��̽��� ItemID����
    }
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } //�κ��丮 ���� �ʱ�ȭ
    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //�� Ȱ��ȭ
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    } //���õ� ���� �����ϰ� �ٸ� ��� ���� �÷� ���İ� 0���� ����
    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    } //���õ� �� ��¦�� ȿ��
    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab) //�ǿ� ���� ������ �з�.
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        }
        for (int i = 0; i < inventoryItemList.Count; i++) //�κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        }
        SelectedItem();
    } //������ Ȱ��ȭ(inventoryTabList�� ���ǿ� �´� �����۵鸸 �־��ְ�, �κ��丮 ���Կ� ���
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "�ش� Ÿ���� �������� �����ϰ� ���� �ʽ��ϴ�.";
    } //���õ� �������� �����ϰ�, �ٸ� ��� ���� �÷� ���İ��� 0���� ����
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    } //���õ� ������ ��¦�� ȿ��


    // Update is called once per frame
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;
                if (activated)
                {
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;

                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;

                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }

                } // �� Ȱ��ȭ�� Ű�Է� ó��.
                
                else if (itemActivated)
                {
                    if (inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 3)
                                selectedItem += 3;
                            else
                                selectedItem %= 2;
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 3;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            SelectedItem();

                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            SelectedItem();

                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedTab == 0) //�Ҹ�ǰ
                            {
                                stopKeyInput = true;
                                //������ ���ǰų�? ���� ������ ȣ��
                            }
                            else if (selectedTab == 1)
                            {
                                //
                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                } //������ Ȱ��ȭ�� Ű�Է� ó��.

                if (Input.GetKeyUp(KeyCode.Z)) //�ߺ� ���� ����
                    preventExec = false;
            }
        }
    }
}