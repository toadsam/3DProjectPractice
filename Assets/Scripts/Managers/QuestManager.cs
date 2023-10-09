using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId; //퀘스트의 종류를 구분하는 변수 

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("친구를 구해줘!!",new int[] {1000,2000}));
    }

    public int GetQuestTalkIndex(int id) //npcid를 받고 퀘스트번호를 반환하는 함수 생성
    {
        return questId;
    }
}
