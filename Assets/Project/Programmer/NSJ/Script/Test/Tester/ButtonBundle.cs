using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSJ_TesterPanel
{
    public class ButtonBundle : BaseUI
    {
        enum Bundle { Monster, BlueChip, EquipMent,Object, Size}
        struct ButtonStruct
        {
            [HideInInspector] public GameObject Bundle;
            [HideInInspector] public GameObject ButtonOn;
        }
        [SerializeField] GameObject _monster;
        [SerializeField] GameObject _blueChip;
        [SerializeField] GameObject _equipment;
        [SerializeField] GameObject _object;

        private ButtonStruct[] _bundles = new ButtonStruct[(int)Bundle.Size];
        private Bundle _curBundle;
        private bool _canControl;

        Coroutine _checkHorizontalInput;
        private void Awake()
        {
            Bind();
            Init();
        }
        private void Start()
        {
            SubscribesEvent();
            ChangeBundle(Bundle.Monster);
        }
        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(GetUI<Button>("MonsterButton").gameObject);
            StartCoroutines();
        }

        private void OnDisable()
        {
            StopCoroutines();
        }



        private void ChangeBundle(Bundle bundle)
        {
            _curBundle = bundle;
            for (int i = 0; i < _bundles.Length; i++) 
            {
                if(i == (int)bundle)
                {
                    _bundles[i].Bundle.SetActive(true);
                    _bundles[i].ButtonOn.SetActive(true);
                }
                else
                {
                    _bundles[i].Bundle.SetActive(false);
                    _bundles[i].ButtonOn.SetActive(false);
                }
            }
        }

        private void Init()
        {
            _bundles[(int)Bundle.Monster] = GetStruct(_monster,GetUI("MonsterButtonOn"));
            _bundles[(int)Bundle.BlueChip] = GetStruct(_blueChip,GetUI("BlueChipButtonOn"));
            _bundles[(int)Bundle.EquipMent] = GetStruct(_equipment, GetUI("EquipmentButtonOn"));
            _bundles[(int)Bundle.Object] = GetStruct(_object, GetUI("ObjectButtonOn"));
        }
        private void SubscribesEvent()
        {
            GetUI<Button>("MonsterButton").onClick.AddListener(() => ChangeBundle(Bundle.Monster));
            GetUI<Button>("BlueChipButton").onClick.AddListener(() => ChangeBundle(Bundle.BlueChip));
            GetUI<Button>("EquipmentButton").onClick.AddListener(() => ChangeBundle(Bundle.EquipMent));
            GetUI<Button>("ObjectButton").onClick.AddListener(() => ChangeBundle(Bundle.Object));
        }

        private ButtonStruct GetStruct(GameObject buddle, GameObject buttonOn)
        {
            ButtonStruct instance = new ButtonStruct();
            instance.Bundle = buddle;
            instance.ButtonOn = buttonOn;
            return instance;
        }
        private void StartCoroutines()
        {
            if (_checkHorizontalInput == null)
            {
                _checkHorizontalInput = StartCoroutine(CheckHorizontalInput());
            }
        }

        private void StopCoroutines()
        {
            if (_checkHorizontalInput != null)
            {
                StopCoroutine(_checkHorizontalInput);
                _checkHorizontalInput = null;
            }
        }
        IEnumerator CheckHorizontalInput()
        {
            _canControl = true;
            while (true)
            {
                if (InputKey.GetButtonDown(InputKey.Interaction))
                {
                    _canControl = false;
                }
                if (InputKey.GetButtonDown(InputKey.PopUpClose))
                {
                    _canControl = true;
                }

                if (_canControl == false)
                {
                    yield return null;
                    continue;
                }

                float x = InputKey.GetAxisRaw(InputKey.Horizontal);
                if (x < 0)
                {
                    _curBundle--;
                    if ((int)_curBundle < 0)
                        _curBundle = 0;
                    ChangeBundle(_curBundle);
                    yield return 0.2f.GetRealTimeDelay();
                }
                else if (x > 0)
                {
                    _curBundle++;
                    if (_curBundle == Bundle.Size)
                        _curBundle = Bundle.Size - 1;
                    ChangeBundle(_curBundle);
                    yield return 0.2f.GetRealTimeDelay();
                }
                yield return null;
            }
        }
    }
}