using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace NSJ_TesterPanel
{
    public class TesterPanel : MonoBehaviour
    {
        [SerializeField] GameObject _canvas;

        private void Awake()
        {
            _canvas.SetActive(false);
        }

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(x => InputKey.GetButtonDown(InputKey.Cheat) == true)
                .Subscribe(x =>
                {
                    if (_canvas.activeSelf == true)
                    {
                        _canvas.SetActive(false);
                        InputKey.ChangeActionMap(ActionMap.GamePlay);
                        Time.timeScale = 1f;
                    }
                    else if (_canvas.activeSelf == false && Time.timeScale != 0)
                    {
                        _canvas.SetActive(true);
                        InputKey.ChangeActionMap(ActionMap.UI);
                        Time.timeScale = 0f;
                    }

                });
            this.UpdateAsObservable()
                .Subscribe(x =>
                {
                    if (_canvas.activeSelf == true && Time.timeScale != 0)
                    {
                        _canvas.SetActive(false);
                    }
                });
        }
    }
}

