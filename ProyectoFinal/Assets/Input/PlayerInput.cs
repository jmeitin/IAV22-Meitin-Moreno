// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Andando"",
            ""id"": ""aac716a8-d88f-4937-8329-62664e799408"",
            ""actions"": [
                {
                    ""name"": ""Movimiento"",
                    ""type"": ""Button"",
                    ""id"": ""e3839e9a-83cf-44fc-8bc5-2257eb7bffe8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mirar"",
                    ""type"": ""Value"",
                    ""id"": ""1cf4ac96-7b3f-47a0-b494-e57803a14878"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""5e269894-a6e8-4d97-933b-fee9487d7c65"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8621504a-c35f-4cd3-8b00-77620a014f9e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""79947263-a717-4b22-9be7-70f81e2a7508"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6ebbc9c8-37a3-4f28-b419-cbe665f8c718"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2ca01187-3562-47c2-8039-d10ae46060ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimiento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9efbcfd0-e1cb-4d3d-8e64-81132f0a1ab5"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mirar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Andando
        m_Andando = asset.FindActionMap("Andando", throwIfNotFound: true);
        m_Andando_Movimiento = m_Andando.FindAction("Movimiento", throwIfNotFound: true);
        m_Andando_Mirar = m_Andando.FindAction("Mirar", throwIfNotFound: true);
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

    // Andando
    private readonly InputActionMap m_Andando;
    private IAndandoActions m_AndandoActionsCallbackInterface;
    private readonly InputAction m_Andando_Movimiento;
    private readonly InputAction m_Andando_Mirar;
    public struct AndandoActions
    {
        private @PlayerInput m_Wrapper;
        public AndandoActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movimiento => m_Wrapper.m_Andando_Movimiento;
        public InputAction @Mirar => m_Wrapper.m_Andando_Mirar;
        public InputActionMap Get() { return m_Wrapper.m_Andando; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AndandoActions set) { return set.Get(); }
        public void SetCallbacks(IAndandoActions instance)
        {
            if (m_Wrapper.m_AndandoActionsCallbackInterface != null)
            {
                @Movimiento.started -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMovimiento;
                @Movimiento.performed -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMovimiento;
                @Movimiento.canceled -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMovimiento;
                @Mirar.started -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMirar;
                @Mirar.performed -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMirar;
                @Mirar.canceled -= m_Wrapper.m_AndandoActionsCallbackInterface.OnMirar;
            }
            m_Wrapper.m_AndandoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movimiento.started += instance.OnMovimiento;
                @Movimiento.performed += instance.OnMovimiento;
                @Movimiento.canceled += instance.OnMovimiento;
                @Mirar.started += instance.OnMirar;
                @Mirar.performed += instance.OnMirar;
                @Mirar.canceled += instance.OnMirar;
            }
        }
    }
    public AndandoActions @Andando => new AndandoActions(this);
    public interface IAndandoActions
    {
        void OnMovimiento(InputAction.CallbackContext context);
        void OnMirar(InputAction.CallbackContext context);
    }
}
