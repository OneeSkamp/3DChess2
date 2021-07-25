// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/ChessControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ChessControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ChessControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ChessControls"",
    ""maps"": [
        {
            ""name"": ""Chess"",
            ""id"": ""9cceda6b-3e74-4742-8aec-f1a7adeb7e28"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""9478cead-0d07-4f78-891d-3c9eae42d61e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a821a8e4-4eb6-4e11-83e7-198efc330e81"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b01ac413-38ba-49e7-bbab-ffe31b17abfb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0abb5d5f-59d2-44bf-8a53-d76859288735"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Chess
        m_Chess = asset.FindActionMap("Chess", throwIfNotFound: true);
        m_Chess_Click = m_Chess.FindAction("Click", throwIfNotFound: true);
        m_Chess_MousePosition = m_Chess.FindAction("MousePosition", throwIfNotFound: true);
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

    // Chess
    private readonly InputActionMap m_Chess;
    private IChessActions m_ChessActionsCallbackInterface;
    private readonly InputAction m_Chess_Click;
    private readonly InputAction m_Chess_MousePosition;
    public struct ChessActions
    {
        private @ChessControls m_Wrapper;
        public ChessActions(@ChessControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Chess_Click;
        public InputAction @MousePosition => m_Wrapper.m_Chess_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Chess; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChessActions set) { return set.Get(); }
        public void SetCallbacks(IChessActions instance)
        {
            if (m_Wrapper.m_ChessActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_ChessActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_ChessActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_ChessActionsCallbackInterface.OnClick;
                @MousePosition.started -= m_Wrapper.m_ChessActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_ChessActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_ChessActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_ChessActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public ChessActions @Chess => new ChessActions(this);
    public interface IChessActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
