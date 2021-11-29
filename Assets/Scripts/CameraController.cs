using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownAction
{
    public class CameraController : Singleton<CameraController>
    {
        private Camera _camera;
        private float _perspectiveZoomSpeed = 0.2f;
        private int _width = 500;
        private int _height = 500;
        Plane _plane = new Plane(Vector3.up, 0);

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            ZoomScreen();
        }

        void ZoomScreen()
        {
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (_camera.orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    _camera.orthographicSize += deltaMagnitudeDiff * _camera.orthographicSize / Screen.height * 2f;

                    // Make sure the orthographic size never drops below zero.
                    _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 4, 20);
                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    _camera.fieldOfView += deltaMagnitudeDiff * _perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 15f, 60f);
                }
            }

            float d = Input.mouseScrollDelta.y;
            if (d != 0)
            {

                if (_camera.orthographic)
                {
                    float scrollspeed = 30f;
                    // ... change the orthographic size based on the change in distance between the touches.
                    _camera.orthographicSize -= d * scrollspeed * _camera.orthographicSize / Screen.height * 2f;

                    // Make sure the orthographic size never drops below zero.
                    _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 4, 20);
                }
                else
                {
                    float scrollspeed = 10f;
                    // Otherwise change the field of view based on the change in distance between the touches.
                    _camera.fieldOfView -= d * scrollspeed * _perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 15f, 60f);
                }
            }
        }

        public void MooveCamera(Vector2 prevPos, Vector2 currentPos)
        {

            if (_camera.orthographic)
            {
                Vector2 delta = currentPos - prevPos;
                Vector2 pos = new Vector2(_camera.transform.position.x, _camera.transform.position.z);
                pos -= delta / Screen.height * (_camera.orthographicSize * 2f);
                pos.x = Mathf.Clamp(pos.x, 1, _width);
                pos.y = Mathf.Clamp(pos.y, 1, _height);
                _camera.transform.position = new Vector3(pos.x, _camera.transform.position.y, pos.y);
            }
            else
            {

                Vector3 delta = Vector3.zero;
                float distance;
                Ray ray = Camera.main.ScreenPointToRay(currentPos);
                if (_plane.Raycast(ray, out distance))
                {
                    delta = ray.GetPoint(distance);
                }

                ray = Camera.main.ScreenPointToRay(prevPos);
                if (_plane.Raycast(ray, out distance))
                {
                    delta -= ray.GetPoint(distance);
                }

                _camera.transform.position -= delta;
            }



        }

        public static bool PointInCameraViewport(Camera camera, Vector3 point)
        {
            var v = camera.WorldToViewportPoint(point);
            return v.x > 0 && v.x < 1 && v.y > 0 && v.y < 1 && v.z > 0;
        }
    }
}