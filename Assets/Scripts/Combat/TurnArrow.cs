using UnityEngine;

public class TurnArrow : MonoBehaviour
{
    [SerializeField]
    private float height = 3.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private float offsetDistance = 0.25f;
    [SerializeField]
    private float offsetSpeed = 2.0f;

    private float currentTime;
    private GameObject currentCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float currentOffset = Mathf.Sin(currentTime * offsetSpeed) * offsetDistance;
        Vector3 relativePosition = Vector3.up * (height + currentOffset);

        this.transform.position = currentCharacter.transform.position + relativePosition;
        this.transform.Rotate(Vector3.up, rotationSpeed);

        this.currentTime += Time.deltaTime;
        if (this.currentTime >= 2 * Mathf.PI / offsetSpeed)
        {
            this.currentTime -= 2 * Mathf.PI / offsetSpeed;
        }
    }

    public void ChangeCharacter(GameObject character)
    {
        this.currentCharacter = character;
    }

}
