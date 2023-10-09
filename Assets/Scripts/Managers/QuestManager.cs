using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId; //����Ʈ�� ������ �����ϴ� ���� 

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("ģ���� ������!!",new int[] {1000,2000}));
    }

    public int GetQuestTalkIndex(int id) //npcid�� �ް� ����Ʈ��ȣ�� ��ȯ�ϴ� �Լ� ����
    {
        return questId;
    }
}
