using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestData : MonoBehaviour
{
    public string questName;
    public int[] npcId;


    public QuestData(string name, int [] npc)  //퀘스트이 이름과, 퀘스트의 연계된 npc들의 이름
    {
        questName = name;
        npcId = npc; 

    }
}
