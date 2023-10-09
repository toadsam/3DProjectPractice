using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<int, string[]> talkData;
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?? Ȥ�� �� �� ������ �� �־�?", "�� ģ�� �� �������" });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] {"�ȳ�.......","�ʰ� �� ������ �����̴�?","�� ģ���� �����־�....Ȥ�� ������ �� ������?","���� �����شٸ� �Ѱ����� ���� �ʿ��� �ֵ��� �Ұ�" });
        talkData.Add(11 + 2000, new string[] { "�����༭ ������!","�Ѱ� �並 �ʿ��� �ٰ�" });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public string GetTalk(int id, int talkIndex) //��ȭ ������ �������� ����
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];
    }
}