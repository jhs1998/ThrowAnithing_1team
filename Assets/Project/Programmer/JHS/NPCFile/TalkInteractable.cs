using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TalkInteractable : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject upPopup; // ������ ���� ������ ����â UI
    [SerializeField] private GameObject pcText; // pc �ؽ�Ʈ
    [SerializeField] private GameObject consoleText; // console �ؽ�Ʈ
    [SerializeField] private GameObject dialogueUI; // ��ȭâ UI
    [SerializeField] private TextMeshProUGUI dialogueText; // ��� ��� �ؽ�Ʈ
    [SerializeField] private string[] dialogues; // ��� ���
    [SerializeField] private float typingSpeed = 0.05f; // Ÿ���� �ӵ�

    [SerializeField] PlayerInput playerInput; // Player Input ������Ʈ
    private bool isPlayerNearby = false; // �÷��̾� ��ó�� �ִ��� Ȯ��
    private bool isTyping = false; // ���� Ÿ���� ������ Ȯ��
    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isItemSpon = false; // ������ �ѹ��� ��ȯ
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public GameObject sponPoint;

    private void Start()
    {
        playerInput = InputKey.PlayerInput;
        if (playerInput == null)
        {
            Debug.Log("PlayerInput �ȵ�");
        }
    }
    // �浹������ �ȳ�ui ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) && !isItemSpon)
        {
            ShowPopup();
            isPlayerNearby = true;
        }
    }
    // �������� �ȳ� ui ��Ȱ��ȭ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            HidePopup();
            isPlayerNearby = false;
        }
    }
    // gameplay�ʿ��� Interaction�׼��� ����ɶ�
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
                        // ��ȭâ�� ���������� ��ȭ ��ŵ
                        HandleDialogueProgress();
                    }
                    else
                    {
                        // ��ȭâ�� ���������� ��ȭâ���� ���� ��ȭ ����
                        ShowDialogueUI();
                        StartDialogue();
                    }
                }
                else if (itemPrefab == null && sponPoint == null)
                {
                    if (dialogueUI.activeSelf)
                    {
                        // ��ȭâ�� ���������� ��ȭ ��ŵ
                        HandleDialogueProgress();
                    }
                    else
                    {
                        // ��ȭâ�� ���������� ��ȭâ���� ���� ��ȭ ����
                        ShowDialogueUI();
                        StartDialogue();
                    }
                }               
            }
        }        
    }

    private void ShowPopup()
    {
        GameObject _pc = pcText;
        GameObject _console = consoleText;
        // �� ����̽��� �´� �ؽ�Ʈ Ȱ��ȭ
        _pc.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.PC);
        _console.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.CONSOLE);

        // �˾� UI Ȱ��ȭ
        upPopup.SetActive(true);
    }

    private void HidePopup()
    {
        // ��� �ؽ�Ʈ�� �˾� UI ��Ȱ��ȭ
        pcText.SetActive(false);
        consoleText.SetActive(false);
        upPopup.SetActive(false);
    }

    private void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
        playerInput.SwitchCurrentActionMap(InputType.UI); // �÷��̾� ���� ��Ȱ��ȭ
        Debug.Log(playerInput.currentActionMap);
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        upPopup.SetActive(false);
        playerInput.SwitchCurrentActionMap(InputType.GAMEPLAY); // �÷��̾� ���� Ȱ��ȭ
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
        dialogueText.text = ""; // ��� �ʱ�ȭ

        foreach (char c in currentDialogue)
        {
            dialogueText.text += c; // �ϳ��� ���
            yield return new WaitForSeconds(typingSpeed);

            // Ÿ���� �߿� 'Choice' �׼��� Ʈ���ŵǸ� ��� ���
            if (playerInput.actions["Choice"].triggered)
            {
                dialogueText.text = currentDialogue; // ��ü ��� ���
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
