using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Time.timeScale = 0f;
        messageObject.SetActive(true);
    }
}
