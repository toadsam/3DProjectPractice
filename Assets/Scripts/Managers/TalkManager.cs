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
