using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverUIReStart : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene(GameManager.instance.SceneIndex);
    }
}
