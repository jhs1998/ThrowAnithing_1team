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

    private PlayerController _player;
    private bool isPlayerNearby = false; // 플레이어 근처에 있는지 확인
    private bool isTyping = false; // 현재 타이핑 중인지 확인
    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    private void Awake()
    {
        // PlayerController 찾기
        _player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            ShowPopup();
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            HidePopup();
            HideDialogueUI();
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (!isPlayerNearby)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (dialogueUI.activeSelf)
            {
                HandleDialogueProgress();
            }
            else
            {
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
        _player.CantOperate = true; // 플레이어 조작 비활성화
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        _player.CantOperate = false; // 플레이어 조작 활성화
    }

    private void StartDialogue()
    {
        currentDialogueIndex = 0;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in dialogues[currentDialogueIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);

            // 타이핑 중에 e 버튼이 눌리면 즉시 전체 출력
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                dialogueText.text = dialogues[currentDialogueIndex];
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
            dialogueText.text = dialogues[currentDialogueIndex];
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
}
