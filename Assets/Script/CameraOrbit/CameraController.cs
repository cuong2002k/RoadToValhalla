using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    private Vector2 mouseInput;

    // what is following?
    [SerializeField] private Transform focus = default;


    [SerializeField] private Transform pivotCameraTranform = default;

    [Header("Camera Settings")]
    private float smoothTime = 1f;
    [SerializeField]
    [Range(25f, 100f)] private float cameraUpandDownSpeed = 40f;
    [SerializeField]
    [Range(25f, 100f)] private float cameraLeftAndRightSpeed = 40f;
    private float minimunLookAngle = -30f;
    private float maxLookAngle = 60f;

    [Header("Camera value")]
    private Vector3 currentVelocity;
    private float upAndDownLookAngle;
    private float leftAndRightLookAngle;
    private bool canRotation;

    private void Start()
    {
        canRotation = true;
    }

    private void OnEnable()
    {
        InputManager.OpenInventoryEvent += HandlerRotationCheck;
    }

    private void OnDisable()
    {
        InputManager.OpenInventoryEvent -= HandlerRotationCheck;
    }

    private void Update()
    {
        if (canRotation)
        {
            mouseInput = InputManager.Instance.MouseInput;
        }
    }

    private void LateUpdate()
    {

        Follow();
        RotationCamera();
    }

    private void Follow()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(this.transform.position, focus.position, ref currentVelocity, smoothTime * Time.deltaTime);
        this.transform.position = targetPosition;
    }

    private void RotationCamera()
    {

        leftAndRightLookAngle += mouseInput.x * cameraLeftAndRightSpeed * Time.deltaTime;

        upAndDownLookAngle -= (mouseInput.y * Time.deltaTime * cameraUpandDownSpeed);

        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimunLookAngle, maxLookAngle);

        Vector3 cameraRotation = Vector3.zero;
        cameraRotation.y = leftAndRightLookAngle;
        Quaternion targetAngleLR = Quaternion.Euler(cameraRotation);
        transform.rotation = targetAngleLR;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        Quaternion targetAngleUD = Quaternion.Euler(cameraRotation);
        pivotCameraTranform.localRotation = targetAngleUD;



    }

    private void HandlerRotationCheck()
    {
        canRotation = !canRotation;
    }
}
