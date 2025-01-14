using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TalkInteractable : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject upPopup; // 가까이 가면 나오는 설명창 UI
    [SerializeField] private GameObject dialogueUI; // 대화창 UI
    [SerializeField] private TextMeshProUGUI dialogueText; // 대사 출력 텍스트
    [SerializeField] private string[] dialogues; // 대사 목록
    [SerializeField] private float typingSpeed = 0.05f; // 타이핑 속도

    [SerializeField] PlayerInput playerInput; // Player Input 컴포넌트
    private bool isPlayerNearby = false; // 플레이어 근처에 있는지 확인
    private bool isTyping = false; // 현재 타이핑 중인지 확인
    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isItemSpon = false; // 아이템 한번만 소환
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public GameObject sponPoint;

    private void Start()
    {
        playerInput = InputKey.PlayerInput;
        if (playerInput == null)
        {
            Debug.Log("PlayerInput 안들어감");
        }
    }
    // 충돌했을때 안내ui 출력
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) && !isItemSpon)
        {
            ShowPopup();
            isPlayerNearby = true;
        }
    }
    // 나왔을때 안내 ui 비활성화
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            HidePopup();
            isPlayerNearby = false;
        }
    }
    // gameplay맵에서 Interaction액션이 실행될때
    private void Update()
    {
        if(playerInput.actions["Interaction"].WasPressedThisFrame() || playerInput.actions["Choice"].WasPressedThisFrame())
        {
            if (isPlayerNearby)
            {
                if (itemPrefab != null && sponPoint != null && !isItemSpon)
                {
                    if (dialogueUI.activeSelf)
                    {
                        // 대화창이 열려있으면 대화 스킵
                        HandleDialogueProgress();
                    }
                    else
                    {
                        // 대화창이 닫혀있으면 대화창으로 열고 대화 시작
                        ShowDialogueUI();
                        StartDialogue();
                    }
                }
                else if (itemPrefab == null && sponPoint == null)
                {
                    if (dialogueUI.activeSelf)
                    {
                        // 대화창이 열려있으면 대화 스킵
                        HandleDialogueProgress();
                    }
                    else
                    {
                        // 대화창이 닫혀있으면 대화창으로 열고 대화 시작
                        ShowDialogueUI();
                        StartDialogue();
                    }
                }               
            }
        }        
    }

    private void ShowPopup()
    {
        upPopup.SetActive(true);
    }

    private void HidePopup()
    {
        upPopup.SetActive(false);
    }

    private void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
        playerInput.SwitchCurrentActionMap(ActionMap.UI); // 플레이어 조작 비활성화
        Debug.Log(playerInput.currentActionMap);
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay); // 플레이어 조작 활성화
        Debug.Log(playerInput.currentActionMap);
        if (itemPrefab != null && sponPoint != null)
        {
            Instantiate(itemPrefab, sponPoint.transform.position, Quaternion.identity);
            isItemSpon = true;
        }
    }

    private void StartDialogue()
    {
        currentDialogueIndex = 0;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        string currentDialogue = dialogues[currentDialogueIndex];
        dialogueText.text = ""; // 대사 초기화

        foreach (char c in currentDialogue)
        {
            dialogueText.text += c; // 하나씩 출력
            yield return new WaitForSeconds(typingSpeed);

            // 타이핑 중에 'Choice' 액션이 트리거되면 즉시 출력
            if (playerInput.actions["Choice"].triggered)
            {
                dialogueText.text = currentDialogue; // 전체 대사 출력
                break;
            }
        }

        isTyping = false;
    }

    private void HandleDialogueProgress()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[currentDialogueIndex];
            isTyping = false;
        }
        else
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                HideDialogueUI();
            }
        }
    }
}
