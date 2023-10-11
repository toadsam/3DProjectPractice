using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int questId; //����Ʈ�� ������ �����ϴ� ���� 
    public int questActionIndex; // npc�� �ε�����ȣ�� ǥ���ϴ� ��
    Dictionary<int, QuestData> questList;
    public GameObject questState1;
    public TextMeshProUGUI questStateText1;
    public GameObject Boss;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        questList.Add(10, new QuestData("ģ���� ������!!", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("�Ѱ� �� ��������", new int[] { 5000, 2000 }));
    }

    public int GetQuestTalkIndex(int id) //npcid�� �ް� ����Ʈ��ȣ�� ��ȯ�ϴ� �Լ� ����
    {
        
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)//��ȭ ������ ���� ����Ʈ ��ȭ������ �ø��� �Լ� ����
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
    }

    void NextQuest() //�������� ���� ����Ʈ -> 10�÷��ְ� �ε����� �ٽ� 0�����Ѵ�.
    {
        questId += 10;
        questActionIndex = 0;
    }
}
