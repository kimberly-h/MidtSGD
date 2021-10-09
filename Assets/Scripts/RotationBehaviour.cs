using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public CharacterController CharacterController;
    public Transform camTransform;

    public float speed = 6f;
    public float rotationSmoothTime = 0.1f;
    float rotationSmoothVelocity;

    Animator animations; 

    private void Start()
    {
        animations = GetComponent <Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            CharacterController.Move(moveDir.normalized * speed * Time.deltaTime);

            animations.SetBool("Walk", true);
        }

        else
        {
            animations.SetBool("Walk", false);
        }
    }
}
