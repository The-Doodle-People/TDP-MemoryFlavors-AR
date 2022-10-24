/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vuforia;

namespace VFX
{
    /// <summary>
    /// A component allowing user to manipulate an object
    /// i.e., translating/rotating the object,
    /// using screen-touch gestures.
    /// </summary>
    public class ObjectManipulator : MonoBehaviour
    {
        [Header("Motion Constraints")]
        public bool ConstrainX;
        public bool ConstrainY;
        public bool ConstrainZ;

        [Header("Game Object Layer")]
        public bool UsePickableLayer;
        public string PickableLayer;

        GameObject mPickedObject;
        Vector3 mLastMousePos;
        
        void OnEnable()
        {
            mLastMousePos = Input.mousePosition;
        }

        void Update()
        {
            if (!VuforiaApplication.Instance.IsRunning || IsPointerOverUIObject())
                return;

            if (Application.isMobilePlatform)
                HandleTouchGestures();
            else
                HandleMouseEvents();
        }
        
        void HandleMouseEvents()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                TryPickObject(Input.mousePosition);
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                mPickedObject = null;
            
            if (mPickedObject)
            {
                // Left mouse button down
                if (Input.GetMouseButton(0))
                {
                    var dx = Mathf.Clamp((Input.mousePosition.x - mLastMousePos.x) / Screen.width, -0.5f, 0.5f);
                    var angle = -dx * 180;
                    RotateObject(angle);
                }

                // Right mouse button down
                if (Input.GetMouseButton(1))
                {
                    var worldSpaceMotion = ScreenToWorldMotion(Input.mousePosition - mLastMousePos);
                    DragObject(worldSpaceMotion);
                }
            }

            // Remember last mouse position
            mLastMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        
        void HandleTouchGestures()
        {
            if (Input.touchCount == 0)
            {
                mPickedObject = null;
                return;
            }

            if (!mPickedObject)
                TryPickObject(Input.touches[0].position);

            if (mPickedObject)
            {
                if (Input.touchCount == 1)
                {
                    var dx = Input.GetTouch(0).deltaPosition.x / Screen.width;
                    var angle = -dx * 180;
                    RotateObject(angle);
                }
                else if (Input.touchCount == 2)
                {
                    var pan = GetTouchPanScreenDelta();
                    var worldSpaceMotion = ScreenToWorldMotion(pan);
                    DragObject(worldSpaceMotion);
                }
            }
        }

        bool TryPickObject(Vector2 screenPosition)
        {
            var cam = VuforiaCameraUtil.GetCamera();
            if (!cam)
                return false;

            var ray = cam.ScreenPointToRay(screenPosition);
            if (UsePickableLayer && Physics.Raycast(ray, out RaycastHit hit, cam.farClipPlane, layerMask: 
                LayerMask.NameToLayer(PickableLayer)))
            {
                mPickedObject = hit.collider.gameObject;
                return true;
            }
            if (Physics.Raycast(ray, out hit, cam.farClipPlane))
            {
                mPickedObject = hit.collider.gameObject;
                return true;
            }
            return false;
        }

        Vector2 GetTouchPanScreenDelta()
        {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            var touchMotion = 0.5f * (touch0.deltaPosition + touch1.deltaPosition);
            return touchMotion;
        }

        Vector3 ScreenToWorldMotion(Vector2 screenMotion)
        {
            var cam = VuforiaCameraUtil.GetCamera();
            if (!cam)
                return Vector3.zero;

            var dx = 2.0f * screenMotion.x / Screen.width;
            var dy = 2.0f * screenMotion.y / Screen.height;

            var viewPoint = cam.transform.position;
            var viewDir = cam.transform.forward;
            var objPos = mPickedObject ? mPickedObject.transform.position : viewPoint;
            var camToObj = objPos - viewPoint;
            var depth = Vector3.Dot(camToObj, viewDir);
            var vertFovRad = CameraUtil.GetVerticalFovRadians(cam);
            var horizFovRad = CameraUtil.GetHorizontalFovRadians(cam);
            var motionScaleX = depth * Mathf.Tan(0.5f * horizFovRad);
            var motionScaleY = depth * Mathf.Tan(0.5f * vertFovRad);

            var cameraSpaceMotion = new Vector3(motionScaleX * dx, motionScaleY * dy, 0);
            var worldSpaceMotion = cam.transform.TransformVector(cameraSpaceMotion);
            return worldSpaceMotion;
        }

        void DragObject(Vector3 worldSpaceMotion)
        {
            if (mPickedObject == null)
                return;

            var worldDx = ConstrainX ? 0 : worldSpaceMotion.x;
            var worldDy = ConstrainY ? 0 : worldSpaceMotion.y;
            var worldDz = ConstrainZ ? 0 : worldSpaceMotion.z;
            mPickedObject.transform.position += new Vector3(worldDx, worldDy, worldDz);
        }

        void RotateObject(float angle)
        {
            if (!mPickedObject)
                return;

            mPickedObject.transform.Rotate(Vector3.up, angle, Space.World);
        }

        bool IsPointerOverUIObject()
        {
            if (EventSystem.current == null)
                return false;

            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
