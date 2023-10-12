# 개인과제 자유게임 만들기

## 프로젝트 소개

- 퀘스트를 받아 분노한 친구를 막는 게임입니다.(캐릭터와 몬스터 간의 상호작용, 인벤토리의 장착 기능, 퀘스트이 ui적인 부분, 캐릭터의 스킬과 유한스테이트 머신 부분을 조금 더 구현해보고 싶었지만, 지금까지 작성한 코드 중 이해하지 못하는 부분이 많아서 그부분을 이해하는데 더 많은 집중을 하기로해서 구현하지 못했습니다…..시간이 된다면 꼭 구현해보겠습니다. 감사합니다. )

## 개발기간

- 2023/10/04 ~ 2023/10/11

## ❤맴버 구성❤

팀원 : 정재훈
  

## **⚙️ 개발 환경**

- Visual Studio - C#

## 🌐주요 기능

- 🎮게임 시작 화면
    - 게임 시작 버튼을 누르면 게임화면으로 이동합니다.

- 💾상호작용
    - InteractionManager1를 통해서 태그에 따라 반응하도록 구현하였습니다.
    
    ```csharp
    public class InteractionManager1 : MonoBehaviour
    {
        //생략
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Interactable") //만약 상호작용가능한 아이템이라면 아이템의 정보를 가져옵니다
            {
                _nearObject = other.gameObject;
                _nearItemObject = _nearObject.GetComponent<ItemObject>();
                SetPromptText();
                
            }
            if(other.tag == "NPC")  //만약 npc라면 대화하기 창이 뜹니다.
            {
                _nearObject = other.gameObject;
                press.gameObject.SetActive(true);
                promptText.text = "대화하기";
            }
        }
    
        public void OnTriggerExit(Collider other) //나가면 전부다 초기화 시킵니다.
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
                if (questManager.GetQuestTalkIndex(id) == 10) //퀘스트 아이디가 10인데 마지막이라면 보스를 소환합니다.
                {
                    Boss.SetActive(true);
                }
                isAction = false; //대화가 끝났다.
                QuestObject.SetActive(isAction);
                QuestCamera.enabled = isAction;
                Debug.Log(isAction);
                talkIndex = 0;
                questManager.CheckQuest(id);  //다음 퀘스트로 이동합니다.
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
    
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (_nearObject != null)
                {
                    if (_nearObject.tag == "NPC")  
                    {
                        ObgData obgData = _nearObject.GetComponent<ObgData>();            
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
    ```
    
    - TalkManager를 통해서 대화들의 내용을 관리했습니다.
    
    ```csharp
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
    ```
    
    - QuestManager를 통해서 퀘스트에 대한 내용들을 관리했습니다.
    
    ```csharp
    public class QuestManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public int questId; //퀘스트의 종류를 구분하는 변수 
        public int questActionIndex; // npc의 인덱스번호를 표시하는 곳
        Dictionary<int, QuestData> questList;
        public GameObject questState1;
        public TextMeshProUGUI questStateText1;
        public GameObject Boss;
    
        private void Awake()
        {
            questList = new Dictionary<int, QuestData>();  //퀘스트id와 퀘스트 이름과 npc의 id를 담는 딕셔너리 생성
            GenerateData();
        }
    
        private void GenerateData()
        {
            questList.Add(10, new QuestData("친구를 구해줘!!", new int[] { 1000, 2000 }));
            questList.Add(20, new QuestData("한강 뷰 보러가기", new int[] { 5000, 2000 }));
        }
    
        public int GetQuestTalkIndex(int id) //npcid를 받고 퀘스트번호를 반환하는 함수 생성
        {
            
            return questId + questActionIndex;
        }
    
        public void CheckQuest(int id)//대화 진행을 위해 퀘스트 대화순서를 올리는 함수 생성
        {
            if (id == questList[questId].npcId[questActionIndex])  //만약 npc아이디의 순서가 다 끝났다면 배열에 있는 다음 npc로 넘어가기
                questActionIndex++;
    
            if (questActionIndex == questList[questId].npcId.Length) //npc와 전부다 얘기를 했다면 다음 퀘스트로 넘어가기
                NextQuest();
        }
    
        void NextQuest() //다음으로 가는 퀘스트 ->퀘스트id를 10늘려주고 인덱스는 다시 0으로한다.
        {
            questId += 10; 
            questActionIndex = 0;
        }
    }
    ```
    

- ⚔️인벤토리
    - Inventory를 통해 인벤토리 관련을 전체적으로 관리했습니다 (인벤토리 매너저 역할을 했습니다)
    
    ```csharp
    public class ItemSlot //슬룻 아이템이 담고있는 정보입니다.
    {
        public ItemData item;
        public int quantity;
    }
    
    public class Inventory : MonoBehaviour
    {
        public ItemSlotUI[] uiSlots;  //슬룻의 ui부분을 담당합니다. -> 우리의 눈에 보이는 부분입니다.
        public ItemSlot[] slots;      // 슬룻의 실질적인 부분입니다. 이 슬룻과 슬룻ui를 매체시켜야합니다.
    
        
        [Header("Selected Item")]
        //생략
       
       // private PlayerController controller;
        //private PlayerConditions condition;
    
        [Header("Events")]
        public UnityEvent onOpenInventory;  //이벤트들
        public UnityEvent onCloseInventory;
    
        public static Inventory instance;
     
        private void Start() //스타트 부분에서 슬룻을 초기화 시킵니다.
        {
            inventoryWindow.SetActive(false);  //인벤토리 창 끄기
            slots = new ItemSlot[uiSlots.Length]; // 슬롯을 담는 배열 생성
    
            for (int i = 0; i < slots.Length; i++)  //각 슬롯에 번호를 붙여주고 
            {
                slots[i] = new ItemSlot();
                uiSlots[i].index = i;
                uiSlots[i].Clear();  //슬롯 클리어
            }
    
            ClearSeletecItemWindow();
        }
        
        public void Toggle() //인벤토리 창을 닫고 열 때 정보를 최신화 합니다.
        {      
        }
    
        public bool IsOpen()
        {
            return inventoryWindow.activeInHierarchy;
        }
    
        public void AddItem(ItemData item) //아이템을 추가 시켜주는 메서드입니다
        {
            if (item.canStack) //만약 스택이가능하다면
            {
                ItemSlot slotToStackTo = GetItemStack(item); //이 아이템이 있는 슬룻이 있는지 찾아보고
                if (slotToStackTo != null)  //있다면
                {
                    slotToStackTo.quantity++; // 수를 올립니다.
                    UpdateUI(); //최신화 합니다.
                    return;
                }
            }
    
            ItemSlot emptySlot = GetEmptySlot(); //배열을 돌리며 비워져있는 슬롯부분을 찾습니다.
    
            if (emptySlot != null) //만약 비워있는 슬룻이 있다면
            {
                emptySlot.item = item; //아이템을 추가합니다
                emptySlot.quantity = 1; 
                UpdateUI(); //최신화 합니다.
                return;
            }
    
            ThrowItem(item); //아무 조건도 해당하지 않는다면 버립니다.
        }
    
        void ThrowItem(ItemData item)
        {
            Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
        }
    
        void UpdateUI() //업로드 시키는 역학을 합니다. -> 실질적인 슬롯정보가 담겨있는 슬롯을 ui슬롯에 전달하는 역할을 합니다
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                    uiSlots[i].Set(slots[i]);
                else
                    uiSlots[i].Clear();
            }
        }
    
        ItemSlot GetItemStack(ItemData item) //해당 아이템이 있는 슬롯을 찾고 반환하는 역할을 합니다.
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                    return slots[i];
            }
    
            return null;
        }
    
        ItemSlot GetEmptySlot()  //빈공간의 슬룻을 찾고 반환합니다.
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                    return slots[i];
            }
    
            return null;
        }
    
        public void SelectItem(int index) //선택한 아이템의 정보를 가져오는 역할을 합니다. 각 슬룻을 누르면 작동하는 이벤트에 넣습니다. 
        {
            if (slots[index].item == null) //빈값이면 그냥 나갑니다.
                return;
            inventoryInfo.SetActive(true); 
            selectedItem = slots[index];
            selectedItemIndex = index;
    
            selectedItemName.text = selectedItem.item.displayName;
            selectedItemDescription.text = selectedItem.item.description;
    
            selectedItemStatNames.text = string.Empty;
            selectedItemStatValues.text = string.Empty;
    
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
                selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
            }
            useButton.SetActive(true);
          //  useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
          //  equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipped);
          // unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipped);
            dropButton.SetActive(true);
        }
    
        private void ClearSeletecItemWindow()
        {
            selectedItem = null;
            selectedItemName.text = string.Empty;
            selectedItemDescription.text = string.Empty;
    
            selectedItemStatNames.text = string.Empty;
            selectedItemStatValues.text = string.Empty;
    
            useButton.SetActive(false);
           // equipButton.SetActive(false);
           // unEquipButton.SetActive(false);
            dropButton.SetActive(false);
        }
    
        public void OnUseButton()
        {
            if (selectedItem.item.type == ItemType.Consumable)
            {
                for (int i = 0; i < selectedItem.item.consumables.Length; i++)
                {
                    switch (selectedItem.item.consumables[i].type)
                    {
                      //  case ConsumableType.Health:
                        //    condition.Heal(selectedItem.item.consumables[i].value); break;
                       // case ConsumableType.Hunger:
                         //   condition.Eat(selectedItem.item.consumables[i].value); break;
                    }
                }
            }
            RemoveSelectedItem();
        }
    
        public void OnEquipButton()
        {
    
        }
    
        void UnEquip(int index)
        {
    
        }
    
        public void OnUnEquipButton()
        {
    
        }
    
        public void OnDropButton()
        {
            ThrowItem(selectedItem.item);
            RemoveSelectedItem();
        }
    
        private void RemoveSelectedItem()
        {
            selectedItem.quantity--;
    
            if (selectedItem.quantity <= 0)
            {
                if (uiSlots[selectedItemIndex].equipped)
                {
                    UnEquip(selectedItemIndex);
                }
    
                selectedItem.item = null;
                ClearSeletecItemWindow();
            }
    
            UpdateUI();
        }
    
        public void RemoveItem(ItemData item)
        {
    
        }
    
        public bool HasItems(ItemData item, int quantity)
        {
            return false;
        }
        
        
    }
    ```
    

- 🤡캐릭터 애니메이션 커스텀
    - 3d사이트에 들어가서 obg파일을 찾은 후 자동으로 리깅해주는기능을 찾아 리깅을 하여 다양한 애니메이션을 사용하였습니다.

- 🤺배틀(몬스터)
    - ai를 이용하여 플레이어를 따라다리면서 공격하도록 구현하였습니다. → 플레이어와의 공격상호작용을 만들어보고 싶었지만 시간 부족으로 하지 못했습니다.
    
    ```csharp
    public class Boss : MonoBehaviour
    {
        public enum AIState  //ai상태
        {
            Idle,
            Wandering,
            Attacking,
            Fleeing
        }
       
         public Player player;
        public GameObject BossNpc;
       
    
        [Header("Stats")]
        public int health;
        public float walkSpeed;
        public float runSpeed;
       
    
        [Header("AI")]
        private AIState aiState;
        public float detectDistance;  //탐지거리
        public float safeDistance;   //안전거리
    
        [Header("Wandering")]
        public float minWanderDistance;  //방황 최소거리 
        public float maxWanderDistance;  // 방황 최대거리
        public float minWanderWaitTime;
        public float maxWanderWaitTime;
    
        [Header("Combat")]
        public int damage;
        public float attackRate;
        private float lastAttackTime;
        public float attackDistance;
    
        private float playerDistance;
    
        public float fieldOfView = 120f;
    
        private NavMeshAgent agent;
        private Animator animator;
        //public Collider collider;
        private SkinnedMeshRenderer[] meshRenderers;
    
        private void Awake()
        {
         
            //player = GetComponent<Player>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();// meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                                                                           //  collider = GetComponentInChildren<Collider>();
        }
    
        private void Start()
        {
            SetState(AIState.Wandering);  //처음에 방황으로 시작합니다. 
        }
    
        private void Update()
        {
           
          
            // 시아각 해결 문제 없음 본체랑 꼬리랑 꺼꾸로 되있었다.  Debug.Log(IsPlaterInFireldOfView());
            playerDistance = Vector3.Distance(transform.position, player.transform.position); //플레이어와 자신사이의 거리
            // 여기는 문젱없음 Debug.Log(playerDistance);
            animator.SetBool("Moving", aiState != AIState.Idle);//가만히 있는것이 아니면 움직이기
    
            switch (aiState)
            {
                case AIState.Idle: PassiveUpdate(); break;
                case AIState.Wandering: PassiveUpdate(); break;
                case AIState.Attacking: AttackingUpdate(); break;
                case AIState.Fleeing: FleeingUpdate(); break;
            }
            // Debug.Log(playerDistance);
        }
    
        private void FleeingUpdate()
        {
            if (agent.remainingDistance < 0.1f)  //이동거리가 가까우면
            {
                agent.SetDestination(GetFleeLocation()); //목적지를 찾기
            }
            else
            {
                SetState(AIState.Wandering);
            }
        }
    
        private void AttackingUpdate()
        {
            if (playerDistance > attackDistance || !IsPlaterInFireldOfView())
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(player.transform.position, path)) //경로를 새로검색한다.
                {
                    agent.SetDestination(player.transform.position); //목적기를 찾기
                }
                else
                {
                    SetState(AIState.Fleeing);
                }
            }
            else
            {
               
                agent.isStopped = true;  //데미지를 입하는 부분 
                if (Time.time - lastAttackTime > attackRate)
                {
                    animator.SetTrigger("Attack");
                   
                    lastAttackTime = Time.time;
                    
                    animator.speed = 1;                
                            
                    fieldOfView = 120f;
                }
            }
        }
    
        private void PassiveUpdate()
        {
            if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f) //방황하는 중이고, 남은거리가 0.1보다 작다
            {
               
                SetState(AIState.Idle);
                Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime)); //새로운 로케이션하는 것이 지연시키는 것
            }
            
    
            if (playerDistance < detectDistance)  //거리안에 들오았다면
            {
                
                SetState(AIState.Attacking);
            }
        }
    
        bool IsPlaterInFireldOfView() //시아각에 들어오는지
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;//거리구하기
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            return angle < fieldOfView * 0.5f;
        }
    
        private void SetState(AIState newState) //곰돌이 상태에 따라 변화 구하기
        {
            aiState = newState;
            switch (aiState)
            {
                case AIState.Idle:
                    {
                        agent.speed = walkSpeed;
                        agent.isStopped = true;
                    }
                    break;
                case AIState.Wandering:
                    {
                        agent.speed = walkSpeed;
                        agent.isStopped = false;
                    }
                    break;
    
                case AIState.Attacking:
                    {
                        agent.speed = runSpeed;
                        agent.isStopped = false;
                    }
                    break;
                case AIState.Fleeing:
                    {
                        agent.speed = runSpeed;
                        agent.isStopped = false;
                    }
                    break;
            }
    
            animator.speed = agent.speed / walkSpeed;
        }
    
        void WanderToNewLocation()  //새로운 거리를 구하는 방법
        {
            if (aiState != AIState.Idle)
            {
                return;
            }
            SetState(AIState.Wandering);
            agent.SetDestination(GetWanderLocation());
        }
    
        Vector3 GetWanderLocation() //
        {
            NavMeshHit hit;
    
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
    
            int i = 0;
            while (Vector3.Distance(transform.position, hit.position) < detectDistance) //hit와 해당위치의 거리가 탐지거리보다 작다면
            {
                NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
                i++;
                if (i == 30)
                    break;
            }
    
            return hit.position;
        }
    
        Vector3 GetFleeLocation()
        {
            NavMeshHit hit;
    
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
    
            int i = 0;
            while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
            {
    
                NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
                i++;
                if (i == 30)
                    break;
            }
    
            return hit.position;
        }
    
        float GetDestinationAngle(Vector3 targetPos)
        {
            return Vector3.Angle(transform.position - player.transform.position, transform.position + targetPos);
        }
    
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                health -= 2;
                Debug.Log("몬스터 체력 : " + health);
                StartCoroutine(DamageFlash());
                
            }
            if (other.tag == "Melee")
            {            
                health -= 2;
                Debug.Log("몬스터 체력 : " + health);
                StartCoroutine(DamageFlash());
            }
    
            if (other.tag == "Bullet")
            {
              
            }
            if (health <= 0)
            {
                attackRate = 20;       
    
                StartCoroutine(DieAni());            
            }
    
        }   
    
        
    
        IEnumerator DamageFlash() //깜빡이는 것임.
        {
            for (int x = 0; x < meshRenderers.Length; x++)
                meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);
    
            yield return new WaitForSeconds(0.1f);
            for (int x = 0; x < meshRenderers.Length; x++)
                meshRenderers[x].material.color = Color.white;
        }
    
        IEnumerator DieAni() //깜빡이는 것임.
        {
            //agent.speed = 0;
            animator.SetTrigger("Die");
            //animator.SetBool("Die", true);
            yield return new WaitForSeconds(8f);
            // DropItem();
            BossNpc.SetActive(true);
            Destroy(gameObject);
        }
       
    }
    ```
    

전체적인 클래스의 관계도
[연습프로젝트 코드 관계도](https://www.figma.com/file/SCSRfnzC0bkV2VSYpc48SU/%EC%97%B0%EC%8A%B5%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%BD%94%EB%93%9C-%EA%B4%80%EA%B3%84%EB%8F%84?type=design&node-id=0-1&mode=design&t=Cscr5cUMGAeCiIre-0)
