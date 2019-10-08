namespace RaycastInteraction
{
    using UnityEngine;
    using UnityEngine.Events;

#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(RaycastInteraction))]
    public class RaycastInteractionEditor : Editor
    {

        /// <summary>
        /// Buttons to be pressed on the inspector GUI 
        /// </summary> 
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RaycastInteraction myScript = (RaycastInteraction)target;

            if (GUILayout.Button("On Enter"))
            {
                myScript.OnRaycastEnter();
            }
            if (GUILayout.Button("On Exit"))
            {
                myScript.OnRaycastExit();
            }
            if (GUILayout.Button("On Input Down"))
            {
                myScript.OnRaycastInput(); 
            }
        }
    }
#endif

    /// <summary>
    /// Simple raycast interaction that can be used in prototyping.
    /// </summary>
    public class RaycastInteraction : MonoBehaviour
    {
        /// <summary>
        /// Raycast origin of the gaze interaction.
        /// </summary>
        [Tooltip("Will default to camera")]
        [SerializeField] private Transform _raycastOrigin;
        /// <summary>
        /// Target interactive object. 
        /// </summary>
        [SerializeField] private Collider _interactiveObject;
        /// <summary>
        /// Max distance for the raycast.
        /// </summary>
        [SerializeField] private float _maxDistance = 50f;
        /// <summary>
        /// Delay between _onGazeInputInvoke scripts happening. 
        /// </summary>
        [SerializeField] private float _delayBetweenClicks = 1f;
        /// <summary>
        /// Layer mask for the raycast.
        /// </summary>
        [Header("Interaction Layer")]
        [SerializeField] private LayerMask _layerMask;
        /// <summary>
        /// On raycast hit enter, invoke this unity event.
        /// </summary>
        [SerializeField] private UnityEvent _onRaycastHitEnter;
        /// <summary>
        /// On raycast hit exit invoke this unity event.
        /// </summary>
        [SerializeField] private UnityEvent _onRaycastHitExit;
        /// <summary>
        /// On raycast hit and correct input, invoke this unity event.
        /// </summary>
        [SerializeField] private UnityEvent _onRaycastHitInput;
        /// <summary>
        /// Turns true when the raycast hit is entered on the interactive object.
        /// </summary>
        private bool _hasBeenGazedAt = false;
        /// <summary>
        /// Is the desired input down? 
        /// </summary>
        private bool _isInputDown = false;
        /// <summary>
        /// Time when input was down, and raycast was hitting the interactive object.
        /// </summary>
        private float _previousRaycastHitInputDownTime;
        /// <summary>
        /// Raycast hit. 
        /// </summary>
        private RaycastHit hit;

        #region Monobehaviors
        /// <summary>
        /// Error checks for raycast origin.
        /// </summary>
        void Awake()
        {
            if (_raycastOrigin == null)
            {
                Debug.Log("Raycast origin is null, deferring to camera");
                _raycastOrigin = Camera.main.transform;
            }
        }
        /// <summary>
        /// Does the raycasting. 
        /// </summary>
        public void Update()
        {
            Debug.DrawRay(_raycastOrigin.position, _raycastOrigin.forward * _maxDistance, Color.green);

            if (Physics.Raycast(_raycastOrigin.position, _raycastOrigin.forward, out hit, _maxDistance, ~_layerMask.value))
            {
                OnRaycastHit(hit.transform);

            }
            else if (_hasBeenGazedAt)
            {
                OnRaycastExit();
            }
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// When the raycast hits a collider. 
        /// </summary>
        /// <param name="hitTransfrom"></param>
        private void OnRaycastHit(Transform hitTransfrom)
        {
            if (hit.transform == _interactiveObject.transform)
            {
                if (!_hasBeenGazedAt)
                {
                    OnRaycastEnter();
                }
                if (_isInputDown)
                {
                    OnRaycastInput();
                }
            }
            else if (_hasBeenGazedAt)
            {
                OnRaycastExit();
            }
        }
        /// <summary>
        /// Checks delay.
        /// </summary>
        /// <returns></returns>
        private bool CanGazeAfterClick()
        {
            if (Time.time - _previousRaycastHitInputDownTime > _delayBetweenClicks)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Public functions

        /// <summary>
        /// Invoked when raycast enters interactive object.
        /// </summary>
        public void OnRaycastEnter()
        {
            Debug.Log("On Raycast Enter");
            _onRaycastHitEnter.Invoke();
            _hasBeenGazedAt = true;
        }

        /// <summary>
        /// Invoked when raycast leaves the interactive object.
        /// </summary>
        public void OnRaycastExit()
        {
            Debug.Log("On Raycast Exit");
            _hasBeenGazedAt = false;
            _onRaycastHitExit.Invoke();

        }

        /// <summary>
        /// Invoked when raycast also has the corresponding input. Checks the delay.
        /// </summary>
        public void OnRaycastInput()
        {
            if (CanGazeAfterClick())
            {
                Debug.Log("On Raycast Input.");
                _previousRaycastHitInputDownTime = Time.time;
                _onRaycastHitInput.Invoke();
            }
        }

        /// <summary>
        /// Called from outside function to indicate whether or not the input needed is down.
        /// </summary>
        public void InputDown()
        {
            _isInputDown = true;
        }

        /// <summary>
        /// Called when the desired input stops.
        /// </summary>
        public void InputUp()
        {
            _isInputDown = false;
        }

        /// <summary>
        /// Switch Raycast Origin
        /// </summary>
        /// <param name="newRaycastOrigin"></param>
        public void SwitchRaycastOrigin(Transform newRaycastOrigin)
        {
            Debug.Log("Raycast origin has been switched.");
            _raycastOrigin = newRaycastOrigin;
        }
        #endregion
    }
}