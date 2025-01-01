

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static InputKey;

public static partial class InputKey
{
    public enum Axis { None, Axis,AxisUp, AxisDown }
    public struct InputStruct
    {
        public string Name;
        public Axis Axis;
        public bool IsPress;
    }

    public static InputStruct Horizontal;
    public static InputStruct Vertical;
    public static InputStruct MouseX;
    public static InputStruct MouseY;
    public static InputStruct Throw;
    public static InputStruct Special;
    public static InputStruct Jump;
    public static InputStruct Melee;
    public static InputStruct RockOn;
    public static InputStruct RockCancel;
    public static InputStruct Negative;
    public static InputStruct Interaction;
    public static InputStruct Dash;
    public static InputStruct Drain;
    public static InputStruct Escape;
    public static InputStruct Inventory;
    public static InputStruct Cheat;
    public static InputStruct Mouse_ScrollWheel;
    public static InputStruct Decomposition;
    public static InputStruct InventoryEquip;
    public static InputStruct PopUpClose;


    private static Dictionary<string , InputStruct> InputStructDic = new Dictionary<string , InputStruct>();

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
        Negative = GetInputStruct("Negative", Axis.AxisUp);
        Interaction = GetInputStruct("Interaction", Axis.None);
        Dash = GetInputStruct("Dash", Axis.AxisUp);
        Drain = GetInputStruct("Drain", Axis.None);
        Escape = GetInputStruct("Escape", Axis.None);
        Inventory = GetInputStruct("Inventory", Axis.AxisDown);
        Cheat = GetInputStruct("Cheat", Axis.None);
        Mouse_ScrollWheel = GetInputStruct("Mouse ScrollWheel", Axis.None);
        Decomposition = GetInputStruct("Decomposition", Axis.None);
        InventoryEquip = GetInputStruct("InventoryEquip", Axis.None);
        PopUpClose = GetInputStruct("PopUp Close", Axis.AxisUp);
        RockCancel = GetInputStruct("Rock Cancel", Axis.None);
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
    public static bool GetButtonDown(InputStruct inputStruct)
    {
        if (InputStructDic[inputStruct.Name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].IsPress == true)
            {
                InputStructDic[inputStruct.Name].SetIsPress(false);
            }
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
    public static float GetAxisRaw(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxisRaw(inputStruct.Name);
    }

    public static float GetAxis(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxis(inputStruct.Name);
    }


    private static void SetIsPress(this InputStruct inputStruct, bool isPress)
    {
        InputStruct newInputStruct =  InputStructDic[inputStruct.Name];
        newInputStruct.IsPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    }
}
