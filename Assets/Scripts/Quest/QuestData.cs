using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestData : MonoBehaviour
{
    public string questName;
    public int[] npcId;


    public QuestData(string name, int [] npc)  //����Ʈ�� �̸���, ����Ʈ�� ����� npc���� �̸�
    {
        questName = name;
        npcId = npc; 

    }
}
