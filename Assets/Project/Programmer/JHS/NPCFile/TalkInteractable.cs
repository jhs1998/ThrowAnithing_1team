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

    private PlayerInput playerInput; // Player Input 컴포넌트
    private bool isPlayerNearby = false; // 플레이어 근처에 있는지 확인
    private bool isTyping = false; // 현재 타이핑 중인지 확인
    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    private void Awake()
    {
        // PlayerInput 컴포넌트 가져오기
        playerInput = GameObject.Find("InputManager").GetComponent<PlayerInput>();
        if (playerInput == null )
        {
            Debug.Log("PlayerInput 못가져옴");
        }
    }

    // 충돌했을때 안내ui 출력
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
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
    private void OnInteraction()
    {
        if(isPlayerNearby)
        {
            //map을 ui로 변경, 대화창 on
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
        SwitchActionMap("UI"); // 플레이어 조작 비활성화
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        SwitchActionMap("Gameplay"); // 플레이어 조작 활성화
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
        dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ""; // 대사 초기화

        foreach (char c in currentDialogue)
        {
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(typingSpeed);

            // 타이핑 중에 즉시 출력 처리
            if (playerInput.actions["Choice"].triggered)
            {
                dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentDialogue;
                break;
            }
        }

        isTyping = false;
    }

    private void HandleDialogueProgress()
    {
        if (isTyping)
        {
            // 타이핑 중이라면 즉시 출력 완료
            StopAllCoroutines();
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = dialogues[currentDialogueIndex];
            isTyping = false;
        }
        else
        {
            // 다음 대사로 이동
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                // 모든 대사가 끝나면 대화창 닫기
                HideDialogueUI();
            }
        }
    }
    private void SwitchActionMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }
}
