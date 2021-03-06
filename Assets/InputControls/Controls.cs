// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""GroundMovement"",
            ""id"": ""ab3f1268-3ce6-4a52-b867-b59fa344c576"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""13144506-4bc1-468a-8804-367a50ede926"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""bf634ff1-c2a5-4fbc-804e-09c221b2cc40"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d3d87b2e-10c1-42d7-9c41-f5c3f1f4323a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""61d20924-7eeb-4cbf-8b63-8f026a2dab02"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""81010a50-5ee0-4ca5-abac-d48e6f7db480"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""86df0abe-7d1f-486a-92de-ced87a38f7c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38e1ffed-4a92-48c2-b121-80759d734c9a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5079e52-5485-4dc6-aa7d-53c32e67c30a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD keys"",
                    ""id"": ""d634f2f9-b0d3-4bc8-a937-4e86abe4a7fa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fdef31c0-7591-46cb-932b-a13b61630ef2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d04856e1-da9f-454e-8e5a-b363a26527b1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a1c2bea7-8692-447d-86eb-021beebf234c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e869b239-bab5-49c5-a444-a674b5452850"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow keys"",
                    ""id"": ""44e2633b-66d8-4988-999d-c1ccf4f3a645"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""69f9512b-2b87-4bcc-b6e5-225393c30891"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bf95521b-2e23-439b-a3d0-87283f2e06ed"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1ddfc0fd-a2cb-422d-907c-ce5159780693"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bd6b359c-d832-4c82-a1d9-ad093184eaf5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""906337cd-6e12-4ccf-81a0-cdb93ad8360d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ca03b361-fd16-452d-8933-139e9bdee85d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""afd32d7a-f94a-42ec-8420-406691f7b3b0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""344f17e5-58c1-4ddd-a552-b06f13c74099"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0ab3577-0f62-4d27-837d-fd5002662474"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""287b6548-032e-4705-abbb-548b27554b34"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36711df6-410d-468a-a7fa-9d3e7cdfdc7c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""401f434c-ad37-4d40-b9f4-ae7fe46e4082"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a87b69b-7947-4915-9de6-47f45711383e"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a1b0814-17bc-48cb-8527-2118b4fa375a"",
                    ""path"": ""<GamePad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19e368ed-ce03-40c0-8240-c8f7e06a6421"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7845dde3-9596-409a-aaa3-4c399c1334b5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92d2c71b-d2f0-431b-82b2-813a92ad8344"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fef9f029-f8e8-4539-bb48-6b24d42c67ed"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Jetpack"",
            ""id"": ""3547a161-a371-4d23-89e4-765a241154e3"",
            ""actions"": [
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""930c2118-e96a-43d1-ad6e-f26755f412a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""f22fc642-7926-4bad-a971-afbefb8d7af7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0e78e9e4-c5dd-444a-a8df-f0343b3944ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ForwardDash"",
                    ""type"": ""Button"",
                    ""id"": ""f502abcf-9a32-4a4a-840c-f2b1d265ea04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ce3aa2b-ddd6-4cac-9990-458c5d5eee4f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""cdd547e8-a60f-4f8b-be10-e4a94645ae50"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""86d87980-51f9-4a89-a925-21377d496e6c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ea4795df-856d-43a4-9754-c5fdc3005578"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7957a263-2392-46a5-8ee2-6fb9c9cebe44"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""9c9fb047-e9f4-4e9d-a0ca-ac273064dca5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b40e264-683f-4d5f-9a4d-4466eaf89b4a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""68166285-dabe-421a-afcd-312eb23af128"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aa0d2d92-ea5a-434d-a98c-8f5199461163"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad - left stick"",
                    ""id"": ""e67021e8-5191-436c-9cbd-376fbbfe5895"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0961549e-2427-47b6-97c1-3e6d12854d96"",
                    ""path"": ""<GamePad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c5907db0-a28c-4905-b92c-dd26dad377c5"",
                    ""path"": ""<GamePad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""68c18932-26ed-413b-89ff-c516a1de01f7"",
                    ""path"": ""<GamePad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""12f62d02-ebf4-4c64-9cb7-532e69f603d7"",
                    ""path"": ""<GamePad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a27368fa-65bd-498d-97ae-6c38b3dc39c5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df813819-d524-4cf0-84bb-81ae74db916b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8c7c0a6-778d-473a-97d9-6e8d282448d6"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ForwardDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84fcf85f-c261-4f8b-a1f1-222e17560267"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ForwardDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""2a850fe5-294f-47b7-b3f2-ad2ec9cb3290"",
            ""actions"": [
                {
                    ""name"": ""TogglePause"",
                    ""type"": ""Button"",
                    ""id"": ""6bc8cbe9-d31b-4b84-bc12-0bfb96c8b8b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleJournal"",
                    ""type"": ""Button"",
                    ""id"": ""5e680ccb-8c58-41f8-a4fc-8a6e94d7a864"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a3f581b6-4d24-4693-a627-679f76aa9f30"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50f50ab9-703e-42ac-9de7-93f55017a820"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c414679-64b8-4d5f-885c-7f69542f66fe"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleJournal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60599dc7-dcc2-4596-8db4-8a5cc623c4be"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleJournal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GroundMovement
        m_GroundMovement = asset.FindActionMap("GroundMovement", throwIfNotFound: true);
        m_GroundMovement_Jump = m_GroundMovement.FindAction("Jump", throwIfNotFound: true);
        m_GroundMovement_Movement = m_GroundMovement.FindAction("Movement", throwIfNotFound: true);
        m_GroundMovement_Interact = m_GroundMovement.FindAction("Interact", throwIfNotFound: true);
        m_GroundMovement_CameraRotate = m_GroundMovement.FindAction("CameraRotate", throwIfNotFound: true);
        m_GroundMovement_Shoot = m_GroundMovement.FindAction("Shoot", throwIfNotFound: true);
        m_GroundMovement_Dash = m_GroundMovement.FindAction("Dash", throwIfNotFound: true);
        // Jetpack
        m_Jetpack = asset.FindActionMap("Jetpack", throwIfNotFound: true);
        m_Jetpack_Dash = m_Jetpack.FindAction("Dash", throwIfNotFound: true);
        m_Jetpack_Steering = m_Jetpack.FindAction("Steering", throwIfNotFound: true);
        m_Jetpack_Shoot = m_Jetpack.FindAction("Shoot", throwIfNotFound: true);
        m_Jetpack_ForwardDash = m_Jetpack.FindAction("ForwardDash", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_TogglePause = m_Menu.FindAction("TogglePause", throwIfNotFound: true);
        m_Menu_ToggleJournal = m_Menu.FindAction("ToggleJournal", throwIfNotFound: true);
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

    // GroundMovement
    private readonly InputActionMap m_GroundMovement;
    private IGroundMovementActions m_GroundMovementActionsCallbackInterface;
    private readonly InputAction m_GroundMovement_Jump;
    private readonly InputAction m_GroundMovement_Movement;
    private readonly InputAction m_GroundMovement_Interact;
    private readonly InputAction m_GroundMovement_CameraRotate;
    private readonly InputAction m_GroundMovement_Shoot;
    private readonly InputAction m_GroundMovement_Dash;
    public struct GroundMovementActions
    {
        private @Controls m_Wrapper;
        public GroundMovementActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_GroundMovement_Jump;
        public InputAction @Movement => m_Wrapper.m_GroundMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_GroundMovement_Interact;
        public InputAction @CameraRotate => m_Wrapper.m_GroundMovement_CameraRotate;
        public InputAction @Shoot => m_Wrapper.m_GroundMovement_Shoot;
        public InputAction @Dash => m_Wrapper.m_GroundMovement_Dash;
        public InputActionMap Get() { return m_Wrapper.m_GroundMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundMovementActions set) { return set.Get(); }
        public void SetCallbacks(IGroundMovementActions instance)
        {
            if (m_Wrapper.m_GroundMovementActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnInteract;
                @CameraRotate.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnCameraRotate;
                @Shoot.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnShoot;
                @Dash.started -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GroundMovementActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_GroundMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @CameraRotate.started += instance.OnCameraRotate;
                @CameraRotate.performed += instance.OnCameraRotate;
                @CameraRotate.canceled += instance.OnCameraRotate;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public GroundMovementActions @GroundMovement => new GroundMovementActions(this);

    // Jetpack
    private readonly InputActionMap m_Jetpack;
    private IJetpackActions m_JetpackActionsCallbackInterface;
    private readonly InputAction m_Jetpack_Dash;
    private readonly InputAction m_Jetpack_Steering;
    private readonly InputAction m_Jetpack_Shoot;
    private readonly InputAction m_Jetpack_ForwardDash;
    public struct JetpackActions
    {
        private @Controls m_Wrapper;
        public JetpackActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dash => m_Wrapper.m_Jetpack_Dash;
        public InputAction @Steering => m_Wrapper.m_Jetpack_Steering;
        public InputAction @Shoot => m_Wrapper.m_Jetpack_Shoot;
        public InputAction @ForwardDash => m_Wrapper.m_Jetpack_ForwardDash;
        public InputActionMap Get() { return m_Wrapper.m_Jetpack; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JetpackActions set) { return set.Get(); }
        public void SetCallbacks(IJetpackActions instance)
        {
            if (m_Wrapper.m_JetpackActionsCallbackInterface != null)
            {
                @Dash.started -= m_Wrapper.m_JetpackActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_JetpackActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_JetpackActionsCallbackInterface.OnDash;
                @Steering.started -= m_Wrapper.m_JetpackActionsCallbackInterface.OnSteering;
                @Steering.performed -= m_Wrapper.m_JetpackActionsCallbackInterface.OnSteering;
                @Steering.canceled -= m_Wrapper.m_JetpackActionsCallbackInterface.OnSteering;
                @Shoot.started -= m_Wrapper.m_JetpackActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_JetpackActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_JetpackActionsCallbackInterface.OnShoot;
                @ForwardDash.started -= m_Wrapper.m_JetpackActionsCallbackInterface.OnForwardDash;
                @ForwardDash.performed -= m_Wrapper.m_JetpackActionsCallbackInterface.OnForwardDash;
                @ForwardDash.canceled -= m_Wrapper.m_JetpackActionsCallbackInterface.OnForwardDash;
            }
            m_Wrapper.m_JetpackActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Steering.started += instance.OnSteering;
                @Steering.performed += instance.OnSteering;
                @Steering.canceled += instance.OnSteering;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ForwardDash.started += instance.OnForwardDash;
                @ForwardDash.performed += instance.OnForwardDash;
                @ForwardDash.canceled += instance.OnForwardDash;
            }
        }
    }
    public JetpackActions @Jetpack => new JetpackActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_TogglePause;
    private readonly InputAction m_Menu_ToggleJournal;
    public struct MenuActions
    {
        private @Controls m_Wrapper;
        public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TogglePause => m_Wrapper.m_Menu_TogglePause;
        public InputAction @ToggleJournal => m_Wrapper.m_Menu_ToggleJournal;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @TogglePause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnTogglePause;
                @TogglePause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnTogglePause;
                @TogglePause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnTogglePause;
                @ToggleJournal.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnToggleJournal;
                @ToggleJournal.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnToggleJournal;
                @ToggleJournal.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnToggleJournal;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TogglePause.started += instance.OnTogglePause;
                @TogglePause.performed += instance.OnTogglePause;
                @TogglePause.canceled += instance.OnTogglePause;
                @ToggleJournal.started += instance.OnToggleJournal;
                @ToggleJournal.performed += instance.OnToggleJournal;
                @ToggleJournal.canceled += instance.OnToggleJournal;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IGroundMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IJetpackActions
    {
        void OnDash(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnForwardDash(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnTogglePause(InputAction.CallbackContext context);
        void OnToggleJournal(InputAction.CallbackContext context);
    }
}
