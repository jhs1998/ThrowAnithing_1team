using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
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
                .Where(x => Input.GetKeyDown(KeyCode.F1) == true)
                .Subscribe(x =>
                {
                    _canvas.SetActive(!_canvas.activeSelf);
                    if(_canvas.activeSelf == true)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                });
        }
    }
}

