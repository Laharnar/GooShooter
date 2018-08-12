using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject replay;
    public void ShowReplayScreen()
    {
        replay.SetActive(true);
    }
}
