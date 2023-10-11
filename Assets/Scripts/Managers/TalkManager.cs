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
        talkData.Add(2000, new string[] { "나는 바보야....ㅠㅠ", "내 친구 좀 구해줘라" });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "안녕.......", "너가 그 유명한 재훈이니?", "내 친구가 분노를 주체하지 못하고 있어....", "만약 도와준다면 한강뷰의 집을 너에게 주도록 할게" });
        talkData.Add(11 + 2000, new string[] { "도와줘서 고마워!", "여기 한강에 있는 집 열쇠야" });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetTalk(int id, int talkIndex) //대화 내용을 가져오는 과정
    {
        if (!talkData.ContainsKey(id)) //딕셔너리에 kety가 존재하는지 검사
        {
            if (!talkData.ContainsKey(id - id % 10))
            {

                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else
            {
                //해당 퀘스트 진행 순서 대사가 없을 때.
                //퀘스트 맨 처음 대사를 가지고 온다.
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
