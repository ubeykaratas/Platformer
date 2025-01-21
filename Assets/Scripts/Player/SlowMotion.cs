using System.Collections;
using UnityEngine;

public class SlowMotion : Base
{
    Player player;

    [SerializeField] private UnityEngine.UI.Button slowMotionButton;
    [SerializeField] private float slowTimeScale = 0.1f;
    [SerializeField] private float slowDownTime = 2.0f;
    [SerializeField] private float coolDown = 5.0f;
    [SerializeField] bool canSlowTime = true;
    private float fixedDeltaTime;


    private void Awake()
    {
        player = GetComponent<Player>();
        fixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public override void Start()
    {
        slowMotionButton.gameObject.SetActive(true);
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = .02f * Time.timeScale;
        slowMotionButton.interactable = false;
        canSlowTime = false;

        //Slow time in [given -> slowDownTime] seconds
        //(NOTE: If slowDownTime = 1.0f and slowTimeScale = 0.1f, the WaitForSeconds(slowDownTime * slowTimeScale) function waits for 1 sec not 0.1! )
        yield return new WaitForSeconds(slowDownTime * slowTimeScale);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = .02f * Time.timeScale;

        yield return new WaitForSeconds(coolDown);
        canSlowTime = true;
        slowMotionButton.interactable = true;
    }

    public void SlowMotionButton()
    {
        if (canSlowTime)
        {
            StartCoroutine(SlowTime());
        }
    }

}
