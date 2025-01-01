

using System.Linq;
using System.Numerics;
using UnityEngine;
using static InputKey;

public static partial class InputKey
{
    public enum Axis { None, AxisUp, AxisDown }
    public struct InputStruct
    {
        public string Name;
        public Axis Axis;
        public bool IsPress;
    }

    public static InputStruct Horizontals;
    public static string Horizontal => Horizontals.Name; 
    public static InputStruct Verticals;
    public static string Vertical => Verticals.Name;
    public static InputStruct MouseXs;
    public static string MouseX => MouseXs.Name;
    public static InputStruct MouseYs;
    public static string MouseY => MouseYs.Name;
    public static InputStruct Throw;
    public static InputStruct Special;
    public static InputStruct Jump;
    public static InputStruct Melee;
    public static InputStruct RockOn;
    public static InputStruct Negative;
    public static InputStruct Interaction;
    public static InputStruct Dash;
    public static InputStruct Drain;
    public static InputStruct Escape;
    public static InputStruct Inventory;
    public static InputStruct Cheat;
    public static InputStruct Mouse_ScrollWheels;
    public static string Mouse_ScrollWheel  => Mouse_ScrollWheels.Name;
    public static InputStruct Decomposition;
    public static InputStruct InventoryEquip;
    public static InputStruct PopUpClose;



    [RuntimeInitializeOnLoadMethod]
    private static void Test()
    {
        Horizontals = GetInputStruct("Horizontal", Axis.None);
        Verticals = GetInputStruct("Vertical", Axis.None);
        MouseXs = GetInputStruct("Mouse X", Axis.None);
        MouseYs = GetInputStruct("Mouse Y", Axis.None);
        Throw = GetInputStruct("Throw", Axis.None);
        Special = GetInputStruct("Special", Axis.None);
        Jump = GetInputStruct("Jump", Axis.None);
        Melee = GetInputStruct("Melee", Axis.None);
        RockOn = GetInputStruct("Rock On", Axis.AxisUp);
        Negative = GetInputStruct("Negative", Axis.None);
        Interaction = GetInputStruct("Interaction", Axis.None);
        Dash = GetInputStruct("Dash", Axis.AxisUp);
        Drain = GetInputStruct("Drain", Axis.None);
        Escape = GetInputStruct("Escape", Axis.None);
        Inventory = GetInputStruct("Inventory", Axis.AxisDown);
        Cheat = GetInputStruct("Cheat", Axis.None);
        Mouse_ScrollWheels = GetInputStruct("Mouse ScrollWheel", Axis.None);
        Decomposition = GetInputStruct("Decomposition", Axis.None);
        InventoryEquip = GetInputStruct("InventoryEquip", Axis.None);
        PopUpClose = GetInputStruct("PopUp Close", Axis.AxisUp);
    }
    private static InputStruct GetInputStruct(string name, Axis axis)
    {
        InputStruct inputStruct = new InputStruct();
        inputStruct.Name = name;
        inputStruct.Axis = axis;
        inputStruct.IsPress = false;
        return inputStruct;
    }
    public static bool GetButton(InputStruct inputStruct)
    {
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
        if (inputStruct.Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            CheckInput(x, ref inputStruct);

            if (x > 0 && inputStruct.IsPress == false)
            {
                return true;
            }
            else
                return false;
        }
        else if (inputStruct.Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            CheckInput(x, ref inputStruct);

            if (x < 0 && inputStruct.IsPress == false)
            {
                inputStruct.IsPress = true;
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
    public static bool GetBUttonUp(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
               if (x != 0 && inputStruct.IsPress == false)
                inputStruct.IsPress = true;

            if (x == 0 && inputStruct.IsPress == true)
            {
                inputStruct.IsPress = false;
                return true;
            }
            else
                return false;
        }
        else if (inputStruct.Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x != 0 && inputStruct.IsPress == false)
                inputStruct.IsPress = true;

            if (x < 0 == inputStruct.IsPress == true)
            {
                inputStruct.IsPress = false;
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

    private static void CheckInput(float x, ref InputStruct inputStruct)
    {
        if (x == 0 && inputStruct.IsPress == true)
            inputStruct.IsPress = false;
        else if (x != 0 && inputStruct.IsPress == false)
            inputStruct.IsPress = true;
    }
}
