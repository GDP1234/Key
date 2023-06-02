using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    string cameraPivot;

    public void CameraPosMove()
    {
        cameraPivot = GameManager.instance.currentMapName;
        GameObject go = GameObject.Find(cameraPivot);
        if(go != null)
        {
            transform.position = go.transform.position;
            return;
        }

        Debug.Log("go가 NULL 입니다.");
    }
}
