

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static InputKey;

public static partial class InputKey
{
    public enum Axis { None, Axis, AxisUp, AxisDown }
    public struct InputStruct
    {
        public string Name;
        public Axis Axis;
        public bool IsPress;
    }

    /// <summary>
    /// a/d , LeftStick
    /// </summary>
    public static InputStruct Horizontal;
    /// <summary>
    /// w/s , LeftStick
    /// </summary>
    public static InputStruct Vertical;
    /// <summary>
    ///  Mouse, RightStick
    /// </summary>
    public static InputStruct MouseX;
    /// <summary>
    /// Mouse, RightStick
    /// </summary>
    public static InputStruct MouseY;
    /// <summary>
    /// Mouse 0, B
    /// </summary>
    public static InputStruct Throw;
    /// <summary>
    /// Mouse 1, Y
    /// </summary>
    public static InputStruct Special;
    /// <summary>
    /// Space , A
    /// </summary>
    public static InputStruct Jump;
    /// <summary>
    /// v , X
    /// </summary>
    public static InputStruct Melee;
    /// <summary>
    /// Tab , LT
    /// </summary>
    public static InputStruct RockOn;
    /// <summary>
    /// C , LB
    /// </summary>
    public static InputStruct RockCancel;
    /// <summary>
    ///  C , D-pad Up
    /// </summary>
    //public static InputStruct Negative;     임시 삭제
    /// <summary>
    ///  E , RB
    /// </summary>
    public static InputStruct Interaction;
    /// <summary>
    /// Left Shift , RT
    /// </summary>
    public static InputStruct Dash;
    /// <summary>
    ///  Q, LS
    /// </summary>
    public static InputStruct Drain;
    /// <summary>
    ///  esc, Start
    /// </summary>
    public static InputStruct Cancel;
    /// <summary>
    ///  B , D-pad Down
    /// </summary>
    public static InputStruct Inventory;
    /// <summary>
    /// F1 , ?
    /// </summary>
    public static InputStruct Cheat;
    /// <summary>
    ///  MouseWheel , ?
    /// </summary>
    public static InputStruct Mouse_ScrollWheel;
    /// <summary>
    ///  Q , X
    /// </summary>
    public static InputStruct Decomposition;
    /// <summary>
    ///  E , B
    /// </summary>
    public static InputStruct InventoryEquip;
    /// <summary>
    ///  c , D-Pad Up
    /// </summary>
    public static InputStruct PopUpClose;


    private static Dictionary<string, InputStruct> InputStructDic = new Dictionary<string, InputStruct>();

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        InputStructDic.Clear();
        Horizontal = GetInputStruct("Horizontal", Axis.Axis);
        Vertical = GetInputStruct("Vertical", Axis.Axis);
        MouseX = GetInputStruct("Mouse X", Axis.Axis);
        MouseY = GetInputStruct("Mouse Y", Axis.Axis);
        Throw = GetInputStruct("Throw", Axis.None);
        Special = GetInputStruct("Special", Axis.None);
        Jump = GetInputStruct("Jump", Axis.None);
        Melee = GetInputStruct("Melee", Axis.None);
        RockOn = GetInputStruct("Rock On", Axis.AxisUp);
        //Negative = GetInputStruct("Negative", Axis.AxisUp);
        Interaction = GetInputStruct("Interaction", Axis.None);
        Dash = GetInputStruct("Dash", Axis.AxisUp);
        Drain = GetInputStruct("Drain", Axis.None);
        Cancel = GetInputStruct("Cancel", Axis.None);
        Inventory = GetInputStruct("Inventory", Axis.AxisDown);
        Cheat = GetInputStruct("Cheat", Axis.None);
        Mouse_ScrollWheel = GetInputStruct("Mouse ScrollWheel", Axis.None);
        Decomposition = GetInputStruct("Decomposition", Axis.None);
        InventoryEquip = GetInputStruct("InventoryEquip", Axis.None);
        PopUpClose = GetInputStruct("PopUp Close", Axis.AxisUp);
        RockCancel = GetInputStruct("Rock Cancel", Axis.None);
    }
    /// <summary>
    /// 버튼을 누르는 도중 호출
    /// </summary>
    public static bool GetButton(InputStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (inputStruct.Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (inputStruct.Axis == Axis.AxisDown)
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
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
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
    public static bool GetButtonDown(InputStruct inputStruct)
    {
        if (InputStructDic[inputStruct.Name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            // 버튼 누르고 뗏을때
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].IsPress == true)
            {
                InputStructDic[inputStruct.Name].SetIsPress(false);
            }
            // 버튼 누르기 시작했을 때
            if (x > 0 && InputStructDic[inputStruct.Name].IsPress == false)
            {
                InputStructDic[inputStruct.Name].SetIsPress(true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].IsPress == true)
                InputStructDic[inputStruct.Name].SetIsPress(false);

            if (x < 0 && InputStructDic[inputStruct.Name].IsPress == false)
            {
                InputStructDic[inputStruct.Name].SetIsPress(true);
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
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].IsPress == true)
            {
                InputStructDic[name].SetIsPress(false);
            }
            if (x > 0 && InputStructDic[name].IsPress == false)
            {
                InputStructDic[name].SetIsPress(true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].IsPress == true)
                InputStructDic[name].SetIsPress(false);

            if (x < 0 && InputStructDic[name].IsPress == false)
            {
                InputStructDic[name].SetIsPress(true);
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
    public static bool GetButtonUp(InputStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (InputStructDic[inputStruct.Name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x > 0 && InputStructDic[inputStruct.Name].IsPress == false)
                InputStructDic[inputStruct.Name].SetIsPress(true);

            if (x == 0 && InputStructDic[inputStruct.Name].IsPress == true)
            {
                InputStructDic[inputStruct.Name].SetIsPress(false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x < 0 && InputStructDic[inputStruct.Name].IsPress == false)
                InputStructDic[inputStruct.Name].SetIsPress(true);

            if (x == 0 && InputStructDic[inputStruct.Name].IsPress == true)
            {
                InputStructDic[inputStruct.Name].SetIsPress(false);
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
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0 && InputStructDic[name].IsPress == false)
                InputStructDic[name].SetIsPress(true);

            if (x == 0 && InputStructDic[name].IsPress == true)
            {
                InputStructDic[name].SetIsPress(false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x < 0 && InputStructDic[name].IsPress == false)
                InputStructDic[name].SetIsPress(true);

            if (x == 0 && InputStructDic[name].IsPress == true)
            {
                InputStructDic[name].SetIsPress(false);
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
    /// Axis 값 1, 0 , -1 반환
    /// </summary>
    public static float GetAxisRaw(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxisRaw(inputStruct.Name);
    }
    public static float GetAxisRaw(string name)
    {
        if (InputStructDic[name].Axis == Axis.None)
            return 0;

        return Input.GetAxisRaw(name);
    }
    /// <summary>
    /// Axis 값 -1 ~ 1 반환
    /// </summary>
    public static float GetAxis(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxis(inputStruct.Name);
    }
    public static float GetAxis(string name)
    {
        if (InputStructDic[name].Axis == Axis.None)
            return 0;

        return Input.GetAxis(name);
    }

    private static void SetIsPress(this InputStruct inputStruct, bool isPress)
    {
        CoroutineHandler.StartRoutine(SetIsPressRoutine(inputStruct, isPress));
    }

    private static IEnumerator SetIsPressRoutine(InputStruct inputStruct, bool isPress)
    {
        yield return null;
        InputStruct newInputStruct = InputStructDic[inputStruct.Name];
        newInputStruct.IsPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    } 

    private static InputStruct GetInputStruct(string name, Axis axis)
    {
        InputStruct inputStruct = new InputStruct();
        inputStruct.Name = name;
        inputStruct.Axis = axis;
        inputStruct.IsPress = false;
        InputStructDic.Add(name, inputStruct);
        return inputStruct;
    }
}
