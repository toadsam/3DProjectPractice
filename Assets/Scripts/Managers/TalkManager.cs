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
        talkData.Add(1000, new string[] { "안녕?? 혹시 나 좀 도와줄 수 있어?", "내 친구 좀 구해줘라" });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public string GetTalk(int id, int talkIndex) //대화 내용을 가져오는 과정
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];
    }
}
