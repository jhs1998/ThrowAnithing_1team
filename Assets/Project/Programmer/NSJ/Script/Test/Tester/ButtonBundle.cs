using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSJ_TesterPanel
{
    public class ButtonBundle : BaseUI
    {
        enum Bundle { Monster, BlueChip, EquipMent, Size}
        struct ButtonStruct
        {
            [HideInInspector] public GameObject Buddle;
            [HideInInspector] public GameObject ButtonOn;
        }
        [SerializeField] GameObject _monster;
        [SerializeField] GameObject _blueChip;
        [SerializeField] GameObject _equipment;

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
                    _bundles[i].Buddle.SetActive(true);
                    _bundles[i].ButtonOn.SetActive(true);
                }
                else
                {
                    _bundles[i].Buddle.SetActive(false);
                    _bundles[i].ButtonOn.SetActive(false);
                }
            }
        }

        private void Init()
        {
            _bundles[(int)Bundle.Monster] = GetStruct(_monster,GetUI("MonsterButtonOn"));
            _bundles[(int)Bundle.BlueChip] = GetStruct(_blueChip,GetUI("BlueChipButtonOn"));
            _bundles[(int)Bundle.EquipMent] = GetStruct(_equipment, GetUI("EquipmentButtonOn"));
        }
        private void SubscribesEvent()
        {
            GetUI<Button>("MonsterButton").onClick.AddListener(() => ChangeBundle(Bundle.Monster));
            GetUI<Button>("BlueChipButton").onClick.AddListener(() => ChangeBundle(Bundle.BlueChip));
            GetUI<Button>("EquipmentButton").onClick.AddListener(() => ChangeBundle(Bundle.EquipMent));
        }

        private ButtonStruct GetStruct(GameObject buddle, GameObject buttonOn)
        {
            ButtonStruct instance = new ButtonStruct();
            instance.Buddle = buddle;
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

                if (Input.GetButtonDown(InputKey.Interactive))
                {
                    _canControl = false;
                }
                if (Input.GetButtonDown(InputKey.Negative))
                {
                    _canControl = true;
                }

                if (_canControl == false)
                {
                    yield return null;
                    continue;
                }

                float x = Input.GetAxisRaw(InputKey.Horizontal);
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