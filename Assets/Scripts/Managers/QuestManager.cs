using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId; //퀘스트의 종류를 구분하는 변수 
    public int questActionIndex; // npc의 인덱스번호를 표시하는 곳
    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("친구를 구해줘!!",new int[] {1000,2000}));
        questList.Add(20, new QuestData("한강 뷰 보러가기", new int[] { 5000, 2000 }));
    }

    public int GetQuestTalkIndex(int id) //npcid를 받고 퀘스트번호를 반환하는 함수 생성
    {
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)//대화 진행을 위해 퀘스트 대화순서를 올리는 함수 생성
    {
       if(id == questList[questId].npcId[questActionIndex])
        questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length);
        NextQuest();
    } 

    void NextQuest() //다음으로 가는 퀘스트 -> 10늘려주고 인덱스는 다시 0으로한다.
    {
        questId += 10;
        questActionIndex = 0;
    }
}
