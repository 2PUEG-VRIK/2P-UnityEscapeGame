using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager3 : MonoBehaviour
{
    public talkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction=false;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        if (isAction)
            isAction = false;
        else
        {
            isAction = true;
            scanObject = scanObj;
            objectData objData = scanObject.GetComponent<objectData>();
            Talk(objData.id, objData.isNpc);
        }
        talkPanel.SetActive(true);
    }   

    void Talk(int id, bool isNpc)
    {
        talkManager.GetTalk(id, talkIndex);
    }
}
