using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public int _count;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        // {
            //�κ��丮 �߰�
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        //}
    }
}
