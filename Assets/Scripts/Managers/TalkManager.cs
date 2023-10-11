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
        talkData.Add(2000, new string[] { "���� �ٺ���....�Ф�", "�� ģ�� �� �������" });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "�ȳ�.......", "�ʰ� �� ������ �����̴�?", "�� ģ���� �г븦 ��ü���� ���ϰ� �־�....", "���� �����شٸ� �Ѱ����� ���� �ʿ��� �ֵ��� �Ұ�" });
        talkData.Add(11 + 2000, new string[] { "�����༭ ����!", "���� �Ѱ��� �ִ� �� �����" });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetTalk(int id, int talkIndex) //��ȭ ������ �������� ����
    {
        if (!talkData.ContainsKey(id)) //��ųʸ��� kety�� �����ϴ��� �˻�
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
                //�ش� ����Ʈ ���� ���� ��簡 ���� ��.
                //����Ʈ �� ó�� ��縦 ������ �´�.
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
