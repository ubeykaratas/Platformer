using Cinemachine;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float transitionValue = .83f;
    [SerializeField] private float transitionTime = 2.0f;
    [SerializeField] private float waitTime = 2.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy trigger
            GetComponent<BoxCollider2D>().enabled = false;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            //Spawn bosses
            ActivateBoss();
            StartCoroutine(AdjustCamera(rb));
        }
    }

    private void ActivateBoss()
    {
        if(bossObject != null)
        {
            bossObject.SetActive(true);
        }
    }

    private System.Collections.IEnumerator AdjustCamera(Rigidbody2D rb)
    {
        CinemachineVirtualCamera vcam = cameraObject.GetComponent<CinemachineVirtualCamera>();
        CinemachineFramingTransposer transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        

        if (vcam == null) yield break;
        float orijinalValue = transposer.m_ScreenX;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return StartCoroutine(LerpScreenX(transposer, orijinalValue, transitionValue));
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(LerpScreenX(transposer, transitionValue, orijinalValue));

        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        MoveBoss();
    }

    private System.Collections.IEnumerator LerpScreenX(CinemachineFramingTransposer transposer, float startValue, float endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            transposer.m_ScreenX = Mathf.Lerp(startValue, endValue, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transposer.m_ScreenX = endValue;
    }

    private void MoveBoss()
    {
        if (bossObject != null)
        {
            bossObject.GetComponent<BossFirst>().enabled = true;
        }
    }

}

