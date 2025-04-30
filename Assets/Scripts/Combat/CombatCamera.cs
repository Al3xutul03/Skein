using UnityEngine;

public class CombatCamera : MonoBehaviour
{
    [SerializeField]
    private float viewAngle = 35.0f;
    [SerializeField]
    private float movementSpeed = 20.0f;
    [SerializeField]
    private float rotationSpeed = 90.0f;

    private GameObject view;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        view = this.transform.GetChild(0).gameObject;

        view.transform.eulerAngles = new Vector3(viewAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(Vector3 movementNormal)
    {
        this.transform.Translate(movementNormal * movementSpeed * Time.deltaTime);
    }

    public void RotateCamera(float direction)
    {
        this.transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }
}
