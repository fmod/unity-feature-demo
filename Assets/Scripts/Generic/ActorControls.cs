/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The heart of the main actor in the scene. Controls movement,    |
|   interactivity, key UI and playing of footsteps                                              |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class ActorControls : MonoBehaviour
{
    public float m_lookSensitivity;
    public float m_walkSpeed, m_runSpeed;
    public float m_jumpPower;
    public float m_selectDistance = 4.0f;   //The max distance the actor can interact with an ActionObject in the scene
    public GameObject m_gun;    //Used in the shooting gallery only

    CharacterController m_cc;
    Camera m_camera;
    ActionObject m_actionObject;

    Vector3 m_velocity;
    public float m_drag;
    float m_minThreshold = 0.017f;

    bool m_disabledMovement;
    public bool m_disabledMouse;
    bool m_disabledActions;
    bool m_isRunning;
    public bool IsRunning { get { return m_isRunning; } }

    public Vector3 CurrentVelocity { get { return new Vector3(m_velocity.x, 0.0f, m_velocity.z); } } //Get velocity returns velocity on the x and z plane, no y.
    public bool IsGrounded { get { return m_cc.isGrounded; } }

    Footsteps m_footsteps;
    float m_footstepElapsed;

    public Text m_pressKeyText; //Ui for showing to the user what keys to press to activate object

    Quaternion m_cameraBaseRotation;
    Vector3 m_lastPosition;
    bool m_moved;
    Vector3 m_relativity;

    void Start()
    {
        m_footsteps = GetComponent<Footsteps>();
        m_isRunning = false;
        m_drag = 1.0f / (m_drag + 1.0f);
        Application.runInBackground = true;
        m_cc = GetComponent<CharacterController>();
        m_camera = Camera.main;
        if (m_gun)
            m_gun.SetActive(false);

        DisableMouse = m_disabledMouse;
        DisableMovement = m_disabledMovement;

        m_cameraBaseRotation = Camera.main.transform.localRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_isRunning = false;
        }
        Action();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void ActivateGun(bool a_value)
    {
        m_gun.SetActive(a_value);
    }

    public bool DisableMovement
    {
        get
        {
            return m_disabledMovement;
        }
        set
        {
            if (!value)
            {
                //Remove mouse visibility, lock it in the middle of the screen and enable movement and mouse functionality
                m_disabledMovement = false;
            }
            else
            {
                //Give back mouse visibility, unlock it in the middle of the screen and disable movement and mouse functionality
                m_disabledMovement = true;
            }
        }
    }

    public bool DisableMouse
    {
        get
        {
            return m_disabledMouse;
        }
        set
        {
            if (!value)
            {
                //Remove mouse visibility, lock it in the middle of the screen and enable movement and mouse functionality
                m_disabledMouse = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                //Give back mouse visibility, unlock it in the middle of the screen and disable movement and mouse functionality
                m_disabledMouse = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public bool DisableActions
    {
        get
        {
            return m_disabledActions;
        }
        set
        {
            m_disabledActions = value;
        }
    }

    void Move()
    {
        Vector3 posDiff = transform.position - m_lastPosition;
        //Homemade physics
        m_velocity.y -= 9.8f * Time.fixedDeltaTime; //gravity
        m_velocity.x *= m_drag;
        m_velocity.z *= m_drag;
        if (Mathf.Abs(m_velocity.x) <= m_minThreshold)
            m_velocity.x = 0.0f;
        if (Mathf.Abs(m_velocity.z) <= m_minThreshold)
            m_velocity.z = 0.0f;
        if (m_cc.isGrounded)
        {
            m_velocity.y = 0.0f;
        }
        if (!m_disabledMovement)
        {
            if (Input.GetKey(KeyCode.A))
            {
                m_velocity += -transform.right * (m_isRunning ? m_runSpeed : m_walkSpeed) * Time.fixedDeltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_velocity += transform.right * (m_isRunning ? m_runSpeed : m_walkSpeed) * Time.fixedDeltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                m_velocity += transform.forward * (m_isRunning ? m_runSpeed : m_walkSpeed) * Time.fixedDeltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_velocity += -transform.forward * (m_isRunning ? m_runSpeed : m_walkSpeed) * Time.fixedDeltaTime;
            }
            if (Input.GetKey(KeyCode.Space) && m_cc.isGrounded) //Jump only when on the ground
            {
                m_velocity += transform.up * m_jumpPower * Time.fixedDeltaTime;
            }
        }
        m_lastPosition = transform.position;

        if (m_velocity.sqrMagnitude > 0.0f)
        {
            if (m_cc.enabled)
                m_cc.Move(m_velocity * Time.fixedDeltaTime);
        }
        Vector3 diff = posDiff - m_relativity;

        if (m_cc.isGrounded && (CurrentVelocity.sqrMagnitude > 0.1f || (m_moved && (diff).magnitude > 0.050f)))
        {
            m_footstepElapsed += (m_isRunning ? m_runSpeed * 0.7f : m_walkSpeed) * Time.fixedDeltaTime * 0.3f;
            while (m_footstepElapsed > 1.0f)
            {
                m_footsteps.PlayFootstep(m_isRunning);
                m_footstepElapsed -= 1.0f;
            }
        }
        m_relativity = Vector3.zero;
        m_moved = false;
    }

    void Look()
    {
        if (!m_disabledMouse && m_camera)
        {
            float rotation = -Input.GetAxis("Mouse Y") * m_lookSensitivity;
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, Input.GetAxis("Mouse X") * m_lookSensitivity, 0.0f);   //rotate the body left and right
            Quaternion temp = m_camera.transform.localRotation * Quaternion.AngleAxis(rotation, Vector3.right);    //rotate  the camera up and down
            //Lock the camera to not flip
            if (Quaternion.Angle(temp, m_cameraBaseRotation) >= 85.0f)
            {
                if (rotation < 0)
                {
                    temp = m_cameraBaseRotation * Quaternion.AngleAxis(85.0f, -Vector3.right);
                }
                else if (rotation > 0)
                {
                    temp = m_cameraBaseRotation * Quaternion.AngleAxis(85.0f, Vector3.right);
                }
            }
            m_camera.transform.localRotation = temp;
        }
    }

    void Action()
    {
        if (DisableActions)
            return;

        //Raycast
        RaycastHit ray;
        int layerMask = (1 << 7) | (1 << 16);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, m_selectDistance, layerMask))
        {
            //Disable last actionObject outline
            if (m_actionObject)
            {
                if (m_actionObject.InQuestion == 1)
                    m_actionObject.InQuestion = 0;
            }

            //Check if the new object is has actionObject, if so set the current m_actionObject to new object
            GameObject newObj = ray.collider.gameObject;
            ActionObject actionObject = newObj.GetComponent<ActionObject>();
            if (!actionObject)
            {
                actionObject = newObj.GetComponentInParent<ActionObject>();
                if (!actionObject)
                {
                    if(m_actionObject)
                        m_actionObject.ActionReleased(gameObject, KeyCode.None);
                    m_actionObject = null;
                    if (m_pressKeyText)
                        m_pressKeyText.gameObject.SetActive(false);
                    return;
                }
            }

            if (m_actionObject && m_actionObject != actionObject)
                m_actionObject.ActionReleased(gameObject, KeyCode.None);

            m_actionObject = actionObject;
            if (m_actionObject.InQuestion == 0)
                m_actionObject.InQuestion = 1;

            //If any of the action keys are pressed, call use on the actionObject
            for (int i = 0; i < m_actionObject.m_actionKeys.Length; ++i)
            {
                if (Input.GetKeyDown(m_actionObject.m_actionKeys[i]))
                {
                    m_actionObject.InQuestion = 2;
                    if (m_actionObject.m_glowOnce)
                        m_actionObject.LastGlow();
                    m_actionObject.ActionPressed(gameObject, m_actionObject.m_actionKeys[i]);
                    StartCoroutine(ActionPressLimit());
                    break;
                }
                else if (Input.GetKey(m_actionObject.m_actionKeys[i]))
                {
                    m_actionObject.ActionDown(gameObject, m_actionObject.m_actionKeys[i]);
                    StartCoroutine(ActionPressLimit());
                    break;
                }
                else if (Input.GetKeyUp(m_actionObject.m_actionKeys[i]))
                {
                    m_actionObject.ActionReleased(gameObject, m_actionObject.m_actionKeys[i]);
                    StartCoroutine(ActionPressLimit());
                    break;
                }
            }
            //Compile the string to display to the user on which keys to press to activate ActionObject
            if (m_pressKeyText && m_actionObject.m_actionKeys.Length != 0)
            {
                string verb = "";
                if (m_actionObject.m_actionVerbs.Length > 0)
                    verb = m_actionObject.m_actionVerbs[0];
                string text = "Press " + GetKey(m_actionObject.m_actionKeys[0]) + " " + verb;
                for (int i = 1; i < m_actionObject.m_actionKeys.Length; ++i)
                {
                    if (m_actionObject.m_actionVerbs.Length > i)
                        verb = m_actionObject.m_actionVerbs[i];
                    else
                        verb = "";

                    text = text.Insert(text.Length, " or " + GetKey(m_actionObject.m_actionKeys[i]) + " " + verb);
                }
                m_pressKeyText.text = text;
                m_pressKeyText.gameObject.SetActive(true);
            }
            return;
        }
        //If there is no raycast and there is an actionObject, disable it's outline
        if (m_actionObject)
        {
            m_actionObject.ActionReleased(gameObject, KeyCode.None);
            if (m_actionObject.InQuestion == 1)
                m_actionObject.InQuestion = 0;
            m_actionObject = null;
            if (m_pressKeyText)
                m_pressKeyText.gameObject.SetActive(false);
        }
    }

    string GetKey(KeyCode a_code)
    {
        switch (a_code)
        {
            case KeyCode.Mouse0:
                return "LMB";
            case KeyCode.Mouse1:
                return "RMB";
            default:
                return a_code.ToString();
        }
    }

    public void SetRotation(Quaternion a_rotation)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation = a_rotation.eulerAngles;
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        transform.eulerAngles = rotation;

        rotation = Camera.main.transform.eulerAngles;
        rotation = a_rotation.eulerAngles;
        rotation.y = 0.0f;
        rotation.z = 0.0f;
        Camera.main.transform.localEulerAngles = rotation;
    }

    public void MoveActor(Vector3 a_move)
    {
        m_relativity = a_move;
        m_moved = true;
        m_cc.Move(a_move);
    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        if(a_hit.collider.gameObject.name.Contains("Roof"))
            m_velocity.y = Mathf.Abs(m_velocity.y) * -1.0f;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            DisableMouse = false;
        }
    }

    IEnumerator ActionPressLimit()
    {
        if (m_actionObject.m_pressPerSecond == 0.0f)
            yield break;

        DisableActions = true;
        yield return new WaitForSeconds(m_actionObject.m_pressPerSecond);
        DisableActions = false;
    }
}