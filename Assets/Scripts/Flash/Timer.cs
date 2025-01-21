using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject enemyFab;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private float timer;
    private bool isTimerRunning;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit + 1f;
        isTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if(Mathf.FloorToInt(timer) <= 0)
            {
                OnComplete();
                timer = 0;
                isTimerRunning = false;
            }
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(timer);
        timerText.text = seconds.ToString();
    }

    private void OnComplete()
    {
        Debug.Log("Timer runs out");
        playerObject.GetComponent<Health>().Damage(999);
    }

    public int TimerInt
    {
        get { return Mathf.FloorToInt(timer); }
    }
}