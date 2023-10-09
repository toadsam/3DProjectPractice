using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionManager1 : MonoBehaviour
{
    private GameObject _nearObject;
    private ItemObject _nearItemObject;
    public GameObject press;
    public TextMeshProUGUI promptText;
    public Camera QuestCamera;

    public GameObject QuestObject;
    public QuestManager questManager;  
    public TalkManager talkManager;
    public TextMeshProUGUI talkText;
    public int talkIndex;
    public bool isAction;
    //public GameObject nextBtn;
    public Button nextBtn;

    public void Awake()
    {
        isAction = false;

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            _nearObject = other.gameObject;
            _nearItemObject = _nearObject.GetComponent<ItemObject>();
            SetPromptText();
            
        }
        if(other.tag == "NPC")
        {
            _nearObject = other.gameObject;
            press.gameObject.SetActive(true);
            promptText.text = "대화하기";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            _nearObject = null;
            _nearItemObject = null;
            OffPromptText();
        }

        if (other.tag == "NPC")
        {
            press.gameObject.SetActive(false);
            promptText.text = "";
        }
    }

    public void SetPromptText()
    {
        press.gameObject.SetActive(true);
        promptText.text = _nearItemObject.item.displayName;
    }

    public void OffPromptText()
    {
        press.gameObject.SetActive(false);
    }

    public void OnInteractInput(InputAction.CallbackContext context)  //인터렉션부분을 오버라이드해서 구현하든 경우를 나눠서 구분하든 해보기
    {
        if (_nearObject != null && context.phase == InputActionPhase.Started)
        {
            _nearItemObject.OnInteract();
            Destroy(_nearObject);
            press.gameObject.SetActive(false);
        }
    }

    public void Talk(int id, bool isNPC)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id); //npcid를 통해 퀘스트 번호를 가져옴 
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);//퀘스트번호 + npcid = 퀘스트 대화 데이터 id
        if (talkData == null) //대화의 내용이 더이상 없다면
        {
            isAction = false; //대화가 끝났다.
            QuestObject.SetActive(isAction);
            QuestCamera.enabled = isAction;
            Debug.Log(isAction);
            talkIndex = 0;
           // questManager.CheckQuest();
            return; //VOID에서 return은 강제 종료역할
        }
        if(isNPC) 
        {
            talkText.text = talkData;      
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true; //대화중이다
        talkIndex++;
    }

    public void NextTalk()
    {
        //Talk(id,isNPC);
        
    }
    



    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (_nearObject != null)
            {
                if (_nearObject.tag == "NPC")  // 이런식으로 하면 코드가 지저분할 듯
                {
                    ObgData obgData = _nearObject.GetComponent<ObgData>();
                    Debug.Log("엔피씨가 맞아");
                    Talk(obgData.id, obgData.isNpc);
                   // nextBtn.onClick.AddListener(() => Talk(obgData.id, obgData.isNpc)); //버튼을 누르면 다음으로 넘어감.
                    QuestObject.SetActive(isAction);
                    QuestCamera.enabled = isAction; 
                }
                else
                {
                    _nearItemObject.OnInteract();
                    Destroy(_nearObject);
                    press.gameObject.SetActive(false);
                }
                
            }
           
        }
    }
}
