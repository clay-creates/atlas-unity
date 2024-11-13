//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/Input Actions/ARSlingshotInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public partial class @ARSlingshotInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ARSlingshotInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ARSlingshotInputs"",
    ""maps"": [
        {
            ""name"": ""Slingshot"",
            ""id"": ""e01164e8-f159-48d2-bbb9-993f4c63b019"",
            ""actions"": [
                {
                    ""name"": ""Touch Position"",
                    ""type"": ""Value"",
                    ""id"": ""66c8ef7c-32d5-47ce-8141-02acf43a7e48"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Touch Press"",
                    ""type"": ""Button"",
                    ""id"": ""51ebde34-3723-489b-b9aa-fe2de99cdcdf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start Drag"",
                    ""type"": ""Value"",
                    ""id"": ""f6ac2701-f253-44b5-8e61-ecfa20034479"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""End Drag"",
                    ""type"": ""Value"",
                    ""id"": ""679de238-1bf9-4b5f-9e99-3c10ce04481e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73ec5deb-1487-4761-ba85-a935c857efcc"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b577f384-b821-4d9b-afcc-14b01bf8fac7"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4e48e09-78ba-464f-bf32-84420d290584"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75f1c69c-09d6-4bd4-90fe-d1ff514a9dcb"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""End Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Slingshot
        m_Slingshot = asset.FindActionMap("Slingshot", throwIfNotFound: true);
        m_Slingshot_TouchPosition = m_Slingshot.FindAction("Touch Position", throwIfNotFound: true);
        m_Slingshot_TouchPress = m_Slingshot.FindAction("Touch Press", throwIfNotFound: true);
        m_Slingshot_StartDrag = m_Slingshot.FindAction("Start Drag", throwIfNotFound: true);
        m_Slingshot_EndDrag = m_Slingshot.FindAction("End Drag", throwIfNotFound: true);
    }

    ~@ARSlingshotInputs()
    {
        Debug.Assert(!m_Slingshot.enabled, "This will cause a leak and performance issues, ARSlingshotInputs.Slingshot.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Slingshot
    private readonly InputActionMap m_Slingshot;
    private List<ISlingshotActions> m_SlingshotActionsCallbackInterfaces = new List<ISlingshotActions>();
    private readonly InputAction m_Slingshot_TouchPosition;
    private readonly InputAction m_Slingshot_TouchPress;
    private readonly InputAction m_Slingshot_StartDrag;
    private readonly InputAction m_Slingshot_EndDrag;
    public struct SlingshotActions
    {
        private @ARSlingshotInputs m_Wrapper;
        public SlingshotActions(@ARSlingshotInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPosition => m_Wrapper.m_Slingshot_TouchPosition;
        public InputAction @TouchPress => m_Wrapper.m_Slingshot_TouchPress;
        public InputAction @StartDrag => m_Wrapper.m_Slingshot_StartDrag;
        public InputAction @EndDrag => m_Wrapper.m_Slingshot_EndDrag;
        public InputActionMap Get() { return m_Wrapper.m_Slingshot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SlingshotActions set) { return set.Get(); }
        public void AddCallbacks(ISlingshotActions instance)
        {
            if (instance == null || m_Wrapper.m_SlingshotActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SlingshotActionsCallbackInterfaces.Add(instance);
            @TouchPosition.started += instance.OnTouchPosition;
            @TouchPosition.performed += instance.OnTouchPosition;
            @TouchPosition.canceled += instance.OnTouchPosition;
            @TouchPress.started += instance.OnTouchPress;
            @TouchPress.performed += instance.OnTouchPress;
            @TouchPress.canceled += instance.OnTouchPress;
            @StartDrag.started += instance.OnStartDrag;
            @StartDrag.performed += instance.OnStartDrag;
            @StartDrag.canceled += instance.OnStartDrag;
            @EndDrag.started += instance.OnEndDrag;
            @EndDrag.performed += instance.OnEndDrag;
            @EndDrag.canceled += instance.OnEndDrag;
        }

        private void UnregisterCallbacks(ISlingshotActions instance)
        {
            @TouchPosition.started -= instance.OnTouchPosition;
            @TouchPosition.performed -= instance.OnTouchPosition;
            @TouchPosition.canceled -= instance.OnTouchPosition;
            @TouchPress.started -= instance.OnTouchPress;
            @TouchPress.performed -= instance.OnTouchPress;
            @TouchPress.canceled -= instance.OnTouchPress;
            @StartDrag.started -= instance.OnStartDrag;
            @StartDrag.performed -= instance.OnStartDrag;
            @StartDrag.canceled -= instance.OnStartDrag;
            @EndDrag.started -= instance.OnEndDrag;
            @EndDrag.performed -= instance.OnEndDrag;
            @EndDrag.canceled -= instance.OnEndDrag;
        }

        public void RemoveCallbacks(ISlingshotActions instance)
        {
            if (m_Wrapper.m_SlingshotActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISlingshotActions instance)
        {
            foreach (var item in m_Wrapper.m_SlingshotActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SlingshotActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SlingshotActions @Slingshot => new SlingshotActions(this);
    public interface ISlingshotActions
    {
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
        void OnStartDrag(InputAction.CallbackContext context);
        void OnEndDrag(InputAction.CallbackContext context);
    }
}