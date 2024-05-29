using UnityEngine;

public class FreeMouseLook : MonoBehaviour
{
    [SerializeField]
    private Camera freelookCamera;

    [SerializeField]
    private float Sensitivity = 20.0f;

    void Update()
    {
        var rotationX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        var rotationY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            transform.Rotate(Vector3.right, rotationY);
            transform.Rotate(transform.up, rotationX);
            transform.SetPositionAndRotation(transform.position,
                Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0));
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        if (Input.mouseScrollDelta == Vector2.up) freelookCamera.fieldOfView =
                Mathf.Clamp(freelookCamera.fieldOfView - 5.0f, 20, 75);

        if (Input.mouseScrollDelta == Vector2.down) freelookCamera.fieldOfView =
            Mathf.Clamp(freelookCamera.fieldOfView + 5.0f, 20, 75);


        if (transform.rotation.eulerAngles.x < 90)
        {
            transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, -10, 60), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, 300, 370), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        Ray ray = freelookCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit DescriptionHit;
        bool outOfCamera = Physics.Raycast(ray, out DescriptionHit);
        if (outOfCamera && !Input.GetMouseButton(1))
        {
            if (DescriptionHit.transform.GetComponent<Description>() != null &&
                DescriptionHit.transform.GetComponent<Description>().description != ObjectManager.Instance.UIManager.GetDescription())
            {
                ObjectManager.Instance.UIManager.SetDescription(DescriptionHit.transform.GetComponent<Description>().description);
            }
        }
        else
        {
            if (ObjectManager.Instance.UIManager.GetDescription() != string.Empty)
            {
                ObjectManager.Instance.UIManager.SetDescription(string.Empty);
            }
        }
    }
}