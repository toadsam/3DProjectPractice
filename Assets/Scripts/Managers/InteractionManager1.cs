using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager1 : MonoBehaviour
{
    private GameObject _nearObject;
    private ItemObject _nearItemObject;
    public GameObject press;
    public TextMeshProUGUI promptText;
    public Camera QuestCamera;

    public GameObject QuestObject;


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
        Debug.Log(1);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_nearObject != null)
            {
                if (_nearObject.tag == "NPC")  // �̷������� �ϸ� �ڵ尡 �������� ��
                {
                    QuestObject.SetActive(true);
                    QuestCamera.enabled = true;
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
