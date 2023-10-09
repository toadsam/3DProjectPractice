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
            promptText.text = "��ȭ�ϱ�";
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

    public void OnInteractInput(InputAction.CallbackContext context)  //���ͷ��Ǻκ��� �������̵��ؼ� �����ϵ� ��츦 ������ �����ϵ� �غ���
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
        int questTalkIndex = questManager.GetQuestTalkIndex(id); //npcid�� ���� ����Ʈ ��ȣ�� ������ 
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);//����Ʈ��ȣ + npcid = ����Ʈ ��ȭ ������ id
        if (talkData == null) //��ȭ�� ������ ���̻� ���ٸ�
        {
            isAction = false; //��ȭ�� ������.
            QuestObject.SetActive(isAction);
            QuestCamera.enabled = isAction;
            Debug.Log(isAction);
            talkIndex = 0;
           // questManager.CheckQuest();
            return; //VOID���� return�� ���� ���Ὺ��
        }
        if(isNPC) 
        {
            talkText.text = talkData;      
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true; //��ȭ���̴�
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
                if (_nearObject.tag == "NPC")  // �̷������� �ϸ� �ڵ尡 �������� ��
                {
                    ObgData obgData = _nearObject.GetComponent<ObgData>();
                    Debug.Log("���Ǿ��� �¾�");
                    Talk(obgData.id, obgData.isNpc);
                   // nextBtn.onClick.AddListener(() => Talk(obgData.id, obgData.isNpc)); //��ư�� ������ �������� �Ѿ.
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
