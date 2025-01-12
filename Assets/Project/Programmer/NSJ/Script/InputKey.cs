using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputKey : MonoBehaviour
{
    public static InputKey Instance;
    #region 인풋매니저
    public enum AxisInputManager { None, Axis, AxisUp, AxisDown }
    public struct InputManagerStruct
    {
        public string Name;
        public AxisInputManager Axis;
        public bool PrevPress;
        public bool CurPress;
    }
    #region 필드
    /// <summary>
    /// a/d , LeftStick
    /// </summary>
    public static InputManagerStruct Horizontal;
    /// <summary>
    /// w/s , LeftStick
    /// </summary>
    public static InputManagerStruct Vertical;
    /// <summary>
    ///  Mouse, RightStick
    /// </summary>
    public static InputManagerStruct MouseX;
    /// <summary>
    /// Mouse, RightStick
    /// </summary>
    public static InputManagerStruct MouseY;
    /// <summary>
    ///  C , D-pad Up
    /// </summary>
    //public static InputManagerStruct Negative;     임시 삭제
    /// <summary>
    ///  E , RB
    /// </summary>
    public static InputManagerStruct PrevInteraction;
    /// <summary>
    ///  esc, Start
    /// </summary>
    public static InputManagerStruct Cancel;
    /// <summary>
    ///  B , D-pad Down
    /// </summary>
    public static InputManagerStruct Inventory;
    /// <summary>
    ///  MouseWheel , ?
    /// </summary>
    public static InputManagerStruct Mouse_ScrollWheel;
    /// <summary>
    ///  Q , X
    /// </summary>
    public static InputManagerStruct Decomposition;
    /// <summary>
    ///  E , B
    /// </summary>
    public static InputManagerStruct InventoryEquip;
    /// <summary>
    ///  c , D-Pad Up
    /// </summary>
    public static InputManagerStruct PopUpClose;

    #endregion
    private static Dictionary<string, InputManagerStruct> InputStructDic = new Dictionary<string, InputManagerStruct>();
    private static List<InputManagerStruct> inputStructs = new List<InputManagerStruct>();

    private void InitInputManager()
    {
        InputStructDic.Clear();
        Horizontal = IGetInputStruct("Horizontal", AxisInputManager.Axis);
        Vertical = IGetInputStruct("Vertical", AxisInputManager.Axis);
        MouseX = IGetInputStruct("Mouse X", AxisInputManager.Axis);
        MouseY = IGetInputStruct("Mouse Y", AxisInputManager.Axis);
        //Negative = IGetInputStruct("Negative", AxisInputManager.AxisUp);
        PrevInteraction = IGetInputStruct("Interaction", AxisInputManager.None);
        Cancel = IGetInputStruct("Cancel", AxisInputManager.None);
        Inventory = IGetInputStruct("Inventory", AxisInputManager.AxisDown);
        Mouse_ScrollWheel = IGetInputStruct("Mouse ScrollWheel", AxisInputManager.None);
        Decomposition = IGetInputStruct("Decomposition", AxisInputManager.None);
        InventoryEquip = IGetInputStruct("InventoryEquip", AxisInputManager.None);
        PopUpClose = IGetInputStruct("PopUp Close", AxisInputManager.AxisUp);
    }
    /// <summary>
    /// 버튼을 누르는 도중 호출
    /// </summary>
    public static bool GetButton(InputManagerStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (inputStruct.Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (inputStruct.Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x < 0)
                return true;
            else
                return false;
        }
        else
        {
            return Input.GetButton(inputStruct.Name);
        }
    }

    public static bool GetButton(string name)
    {
        if (InputStructDic[name].Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (InputStructDic[name].Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x < 0)
                return true;
            else
                return false;
        }
        else
        {
            return Input.GetButton(InputStructDic[name].Name);
        }
    }
    /// <summary>
    /// 버튼을 눌렀을 때 호출
    /// </summary>
    public static bool GetButtonDown(InputManagerStruct inputStruct)
    {
        if (InputStructDic[inputStruct.Name].Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            // 버튼 누르고 뗏을때
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[inputStruct.Name], false);
            }

            if (x > 0 && InputStructDic[inputStruct.Name].PrevPress == false)
            {
                ISetCurPress(InputStructDic[inputStruct.Name], true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].PrevPress == true)
                ISetCurPress(InputStructDic[inputStruct.Name], false);

            if (x < 0 && InputStructDic[inputStruct.Name].PrevPress == false)
            {
                ISetCurPress(InputStructDic[inputStruct.Name], true);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonDown(inputStruct.Name);
        }
    }
    public static bool GetButtonDown(string name)
    {
        if (InputStructDic[name].Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[name], false);
            }
            if (x > 0 && InputStructDic[name].PrevPress == false)
            {
                ISetCurPress(InputStructDic[name], true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].PrevPress == true)
                ISetCurPress(InputStructDic[name], false);

            if (x < 0 && InputStructDic[name].PrevPress == false)
            {
                ISetCurPress(InputStructDic[name], true);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonDown(name);
        }
    }

    //IEnumerator
    /// <summary>
    /// 버튼 누르기를 그만둘떄 호출
    /// </summary>
    public static bool GetButtonUp(InputManagerStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (InputStructDic[inputStruct.Name].Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x > 0 && InputStructDic[inputStruct.Name].PrevPress == false)
                ISetCurPress(InputStructDic[inputStruct.Name], true);

            if (x == 0 && InputStructDic[inputStruct.Name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[inputStruct.Name], false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x < 0 && InputStructDic[inputStruct.Name].PrevPress == false)
                ISetCurPress(InputStructDic[inputStruct.Name], true);

            if (x == 0 && InputStructDic[inputStruct.Name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[inputStruct.Name], false);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonUp(inputStruct.Name);
        }
    }
    public static bool GetButtonUp(string name)
    {
        if (InputStructDic[name].Axis == AxisInputManager.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0 && InputStructDic[name].PrevPress == false)
                ISetCurPress(InputStructDic[name], true);

            if (x == 0 && InputStructDic[name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[name], false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == AxisInputManager.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x < 0 && InputStructDic[name].PrevPress == false)
                ISetCurPress(InputStructDic[name], true);

            if (x == 0 && InputStructDic[name].PrevPress == true)
            {
                ISetCurPress(InputStructDic[name], false);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonUp(name);
        }
    }
    /// <summary>
    /// AxisInputManager 값 1, 0 , -1 반환
    /// </summary>
    public static float GetAxisRaw(InputManagerStruct inputStruct)
    {
        if (inputStruct.Axis == AxisInputManager.None)
            return 0;

        return Input.GetAxisRaw(inputStruct.Name);
    }
    public static float GetAxisRaw(string name)
    {
        if (InputStructDic[name].Axis == AxisInputManager.None)
            return 0;

        return Input.GetAxisRaw(name);
    }
    /// <summary>
    /// AxisInputManager 값 -1 ~ 1 반환
    /// </summary>
    public static float GetAxis(InputManagerStruct inputStruct)
    {
        if (inputStruct.Axis == AxisInputManager.None)
            return 0;

        return Input.GetAxis(inputStruct.Name);
    }
    public static float GetAxis(string name)
    {
        if (InputStructDic[name].Axis == AxisInputManager.None)
            return 0;

        return Input.GetAxis(name);
    }

    private static void ISetCurPress(InputManagerStruct inputStruct, bool isPress)
    {
        InputManagerStruct newInputStruct = InputStructDic[inputStruct.Name];
        newInputStruct.CurPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    }

    private static void ISetPrevPress(InputManagerStruct inputStruct, bool isPress)
    {
        InputManagerStruct newInputStruct = InputStructDic[inputStruct.Name];
        newInputStruct.PrevPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    }

    private static InputManagerStruct IGetInputStruct(string name, AxisInputManager axis)
    {
        InputManagerStruct inputStruct = new InputManagerStruct();
        inputStruct.Name = name;
        inputStruct.Axis = axis;
        inputStruct.CurPress = false;
        InputStructDic.Add(name, inputStruct);
        inputStructs.Add(inputStruct);
        return inputStruct;
    }
    #endregion
    #region 인풋시스템
    public enum Action
    {
        Move, CameraMove, // Axis
        MouseDelta, Jump, Throw, Special, Melee, LoakOn, LoakOff, Dash, Interaction, Drain, OpenSettings, InvenOpen, Cheat
    } // None
    public enum Axis { None, Axis }
    public struct InputStruct
    {
        public Action Name;
        public Axis Axis;
        public bool PrevPress;
        public bool CurPress;
        public Vector2 Vector;
    }
    public static InputStruct Move;
    public static InputStruct CameraMove;
    public static InputStruct MouseDelta;
    public static InputStruct Jump;
    public static InputStruct Throw;
    public static InputStruct Special;
    public static InputStruct Melee;
    public static InputStruct LoakOn;
    public static InputStruct LoakOff;
    public static InputStruct Dash;
    public static InputStruct Interaction;
    public static InputStruct Drain;
    public static InputStruct OpenSetting;
    public static InputStruct InvenOpen;
    public static InputStruct Cheat;


    private Dictionary<Action, InputStruct> m_inputStructDic = new Dictionary<Action, InputStruct>();
    private static Dictionary<Action, InputStruct> _inputStructDic { get { return Instance.m_inputStructDic; } }
    private  List<InputStruct> m_inputStructs = new List<InputStruct>();
    private static List<InputStruct> _inputStructs { get { return Instance.m_inputStructs; } }
    private PlayerInput m_playerInput;
    private static PlayerInput _playerInput { get { return Instance.m_playerInput; } }

    private void Awake()
    {
        if(InitSingleTon() ==false) return;
        Init();
        InitInputManager();

        m_playerInput = GetComponent<PlayerInput>();
    }
    private bool InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }

    private void Init()
    {
        Move = GetInputStruct(Action.Move, Axis.Axis);
        CameraMove = GetInputStruct(Action.CameraMove, Axis.Axis);
        MouseDelta = GetInputStruct(Action.MouseDelta, Axis.Axis);
        Jump = GetInputStruct(Action.Jump, Axis.None);
        Throw = GetInputStruct(Action.Throw, Axis.None);
        Special = GetInputStruct(Action.Special, Axis.None);
        Melee = GetInputStruct(Action.Melee, Axis.None);
        LoakOn = GetInputStruct(Action.LoakOn, Axis.None);
        LoakOff = GetInputStruct(Action.LoakOff, Axis.None);
        Dash = GetInputStruct(Action.Dash, Axis.None);
        Interaction = GetInputStruct(Action.Interaction, Axis.None);
        Drain = GetInputStruct(Action.Drain, Axis.None);
        OpenSetting = GetInputStruct(Action.OpenSettings, Axis.None);
        InvenOpen = GetInputStruct(Action.InvenOpen, Axis.None);
        Cheat = GetInputStruct(Action.Cheat, Axis.None);
    }

    private void LateUpdate()
    {
        foreach (InputManagerStruct inputStruct in inputStructs)
        {
            ISetPrevPress(InputStructDic[inputStruct.Name], InputStructDic[inputStruct.Name].CurPress);
        }

        foreach (InputStruct inputStruct in _inputStructs)
        {
            SetPrevPress(_inputStructDic[inputStruct.Name], _inputStructDic[inputStruct.Name].CurPress);
        }
    }

    public static bool GetButton(InputStruct inputStruct)
    {
        if (_inputStructDic[inputStruct.Name].Axis == Axis.Axis)
            return false;

        if (_inputStructDic[inputStruct.Name].CurPress == true)
            return true;
        else
            return false;
    }
    public static bool GetButtonDown(InputStruct inputStruct)
    {
        if (_inputStructDic[inputStruct.Name].Axis == Axis.Axis)
            return false;

        //if (inputStruct.Name == Action.Jump)
        //    Debug.Log($"{_inputStructDic[inputStruct.Name].CurPress}, {_inputStructDic[inputStruct.Name].PrevPress}");

        if (_inputStructDic[inputStruct.Name].CurPress == true && _inputStructDic[inputStruct.Name].PrevPress == false)
            return true;
        else
            return false;
    }
    
    public static bool GetButtonUp(InputStruct inputStruct)
    {
        if (_inputStructDic[inputStruct.Name].Axis == Axis.Axis)
            return false;

        if (_inputStructDic[inputStruct.Name].CurPress == false && _inputStructDic[inputStruct.Name].PrevPress == true)
            return true;
        else
            return false;
    }

    public static Vector2 GetAxis(InputStruct inputStruct)
    {
        if (_inputStructDic[inputStruct.Name].Axis == Axis.None)
            return default;

        return _inputStructDic[inputStruct.Name].Vector;
    }


    public static void ChangeActionMap(string actionMap)
    {
        Debug.Log(_playerInput);
        _playerInput.defaultActionMap = actionMap;
    }

    private static void SetCurPress(InputStruct inputStruct, bool isPress)
    {
        InputStruct newInputStruct = _inputStructDic[inputStruct.Name];
        newInputStruct.CurPress = isPress;
        _inputStructDic[inputStruct.Name] = newInputStruct;
    }

    private static void SetPrevPress(InputStruct inputStruct, bool isPress)
    {
        InputStruct newInputStruct = _inputStructDic[inputStruct.Name];
        newInputStruct.PrevPress = isPress;
        _inputStructDic[inputStruct.Name] = newInputStruct;
    }
    private static void SetVector(InputStruct inputStruct, Vector2 value)
    {
        InputStruct newInputStruct = _inputStructDic[inputStruct.Name];
        newInputStruct.Vector = value;
        _inputStructDic[inputStruct.Name] = newInputStruct;
    }
    private static InputStruct GetInputStruct(Action name, Axis axis)
    {
        InputStruct inputStruct = new InputStruct();
        inputStruct.Name = name;
        inputStruct.Axis = axis;
        inputStruct.CurPress = false;
        _inputStructDic.Add(name, inputStruct);
        _inputStructs.Add(inputStruct);
        return inputStruct;
    }
    private void ProcessInput(InputValue value, InputStruct input)
    {
        if (input.Axis == Axis.Axis)
        {
            Vector2 vector = value.Get<Vector2>();
            SetVector(input, vector);
        }
        else if (input.Axis == Axis.None)
        {
            // 버튼식이면 0이 아니면 누르고있음, 0이면 뗐음
            float floatValue = value.Get<float>();
            if (floatValue != 0)
            {
                SetCurPress(input, true);
            }
            else
            {
                SetCurPress(input, false);
            }
        }
    }

    private void OnMove(InputValue value)
    {
        ProcessInput(value, Move);
    }
    private void OnCameraMove(InputValue value)
    {
        ProcessInput(value, CameraMove);
    }
    private void OnMouseDelta(InputValue value)
    {
        ProcessInput(value, MouseDelta);
    }
    private void OnJump(InputValue value)
    {
;        ProcessInput(value, Jump);
    }
    private void OnRanged_Attack(InputValue value)
    {
        ProcessInput(value, Throw);
    }
    private void OnSpecial_Attack(InputValue value)
    {
        ProcessInput(value, Special);
    }
    private void OnMelee_Attack(InputValue value)
    {
        ProcessInput(value, Melee);
    }

    private void OnLoak_On(InputValue value)
    {
        ProcessInput(value, LoakOn);
    }
    private void OnLoak_Off(InputValue value)
    {
        ProcessInput(value, LoakOff);
    }
    private void OnDash(InputValue value)
    {
        ProcessInput(value, Dash);
    }
    private void OnInteraction(InputValue value)
    {
        ProcessInput(value, Interaction);
    }
    private void OnDrain(InputValue value)
    {
        ProcessInput(value, Drain);
    }
    private void OnOpen_Settine(InputValue value)
    {
        ProcessInput(value, OpenSetting);
    }

    private void OnInvenOpen(InputValue value)
    {
        ProcessInput(value, InvenOpen);
    }
    private void OnCheat(InputValue value)
    {
        ProcessInput(value, Cheat);
    }
    #endregion
}
