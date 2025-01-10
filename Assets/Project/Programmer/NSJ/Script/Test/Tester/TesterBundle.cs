using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSJ_TesterPanel
{
    public class TesterBundle : BaseUI
    {
        protected struct ButtonStruct
        {
            public Button Button;
            public Image Image;
        }
        protected List<ButtonStruct> _buttons = new List<ButtonStruct>();

        protected int _buttonIndex;

        protected GameObject _player;
        protected bool _canControl;

        [SerializeField] protected Vector3 createOffset = new Vector3(5,0,5);
        [SerializeField] protected Vector3 randomOffset;
        Coroutine _checkHorizontalInput;
        protected virtual void Awake()
        {
            Bind();
            _player = GameObject.FindGameObjectWithTag(Tag.Player);
        }
        protected virtual void OnEnable()
        {
            StartCoroutines();

        }
        protected virtual void OnDisable()
        {
            StopCoroutines();

        }
        protected ButtonStruct GetButtonStruct(string name)
        {
            ButtonStruct buttonStruct = new ButtonStruct();
            buttonStruct.Button = GetUI<Button>(name);
            buttonStruct.Image = GetUI<Image>(name);
            return buttonStruct;
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
            _canControl = false;
            while (true)
            {

                if (InputKey.GetButtonDown(InputKey.Cheat))
                {
                    ChangeHighlight(-10);
                }

                if (InputKey.GetButtonDown(InputKey.PrevInteraction))
                {
                    if (_canControl == false)
                    {
                        _canControl = true;
                        _buttonIndex = 0;
                        ChangeHighlight(_buttonIndex);
                    }
                    else
                    {
                        InteractButton(_buttonIndex);
                    }
                }
                if (InputKey.GetButtonDown(InputKey.PopUpClose))
                {
                    _canControl = false;
                    ChangeHighlight(-10);
                }

                if (_canControl == false)
                {
                    yield return null;
                    continue;
                }

                float x = InputKey.GetAxisRaw(InputKey.Horizontal);
                if (x < 0)
                {
                    _buttonIndex--;
                    if (_buttonIndex < 0)
                        _buttonIndex = 0;
                    ChangeHighlight(_buttonIndex);
                    yield return 0.2f.GetRealTimeDelay();
                }
                else if (x > 0)
                {
                    _buttonIndex++;
                    if (_buttonIndex >= _buttons.Count)
                        _buttonIndex = _buttons.Count - 1;
                    ChangeHighlight(_buttonIndex);
                    yield return 0.2f.GetRealTimeDelay();
                }
                yield return null;
            }
        }

        /// <summary>
        /// 하이라이트 결정 (-10은 모두 끄기)
        /// </summary>
        /// <param name="index"></param>
        private void ChangeHighlight(int index)
        {
            if(index == -10)
            {
                foreach(ButtonStruct button in _buttons)
                {
                    button.Image.color = Color.white;
                }
            }
            for (int i = 0; i < _buttons.Count; i++)
            {
                if(index == i)
                {
                    _buttons[i].Image.color = Color.yellow;
                }
                else
                {
                    _buttons[i].Image.color = Color.white;
                }
            }
        }

        private void InteractButton(int index)
        {
            _buttons[index].Button.onClick.Invoke();
        }
    }
}