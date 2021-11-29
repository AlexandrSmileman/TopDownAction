using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TopDownAction
{
    public class InputManager : MonoBehaviour
    {
        private Camera _camera;
        private CameraController _cameraController;
        public ProductionPanel productionPanel;

        public delegate void SetDestinationPosition(Vector3 pos);
        public static event SetDestinationPosition OnSetDestination;

        public delegate void SelectAllUnitsInCameraView(Camera camera);
        public static event SelectAllUnitsInCameraView OnSelectAllUnitsInCameraView;

        private const float CAMERA_SPEED = 40f;
        private float _lastClickTime;
        private bool _dragging = false;
        private Vector3 _rawMousePos = Vector2.zero;
        private Vector3 _prevMousePos = new Vector3();

        private void Awake()
        {
            _camera = Camera.main;
            _cameraController = _camera.GetComponent<CameraController>();
            Input.simulateMouseWithTouches = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsTouchOnUI(Input.mousePosition))
            {
                DesctopEvents();
                //MobileEvents();
            }
        }

        private void DesctopEvents()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if(!_dragging)
                    ClickEvent(Input.mousePosition);
                _dragging = false;
            }

            //move camera
            _camera.transform.position += 
                new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))
                * Time.deltaTime * CAMERA_SPEED;

            if (Input.GetMouseButtonDown(1))
            {
                _rawMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                _cameraController.MooveCamera(_prevMousePos, Input.mousePosition);

                Vector2 delta = Input.mousePosition - _rawMousePos;
                if (delta.magnitude > 30)
                {
                    _dragging = true;
                }
            }

            _prevMousePos = Input.mousePosition;
        }

        private void MobileEvents()
        {

            Vector2 controlPoint = Vector2.zero;

            if (Input.touchCount == 2)
                _dragging = true;


            if (Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Moved)
                {
                    _cameraController.MooveCamera(touch.position - touch.deltaPosition, touch.position);
                }
                else if (touch.phase == TouchPhase.Ended && Vector2.Distance(touch.rawPosition, touch.position) < 30)
                {

                    if (!_dragging)
                        ClickEvent(touch.position);
                    _dragging = false;
                }
            }

            if (Input.touchCount == 0)
                _dragging = false;
        }

        private void ClickEvent(Vector2 point)
        {
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (!Physics.SphereCast(ray, 0.2f, out hit, 100, 1 << 6))
                return; //interact only with the touchable layer = 6

            if (hit.collider.gameObject.TryGetComponent(out HomeBase home)) 
            {
                //if click on homebase, show production panel
                productionPanel.SetHomeBase(home);
                productionPanel.gameObject.SetActive(true);
            }
            else
            {
                if (hit.collider.gameObject.TryGetComponent(out ISelectable selectable))
                {
                    if (Time.time - _lastClickTime < 0.5f) //double click, select all units
                        OnSelectAllUnitsInCameraView?.Invoke(_camera);
                    else
                        selectable.SwitchSelection();
                }
                else
                {
                    OnSetDestination?.Invoke(hit.point);
                }
                
            }

            _lastClickTime = Time.time;
        }

        bool IsTouchOnUI(Vector2 point)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = point;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}