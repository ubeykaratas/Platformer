using UnityEngine;
using UnityEngine.SceneManagement;

public class Flash : MonoBehaviour
{
    [SerializeField] private float loadTime = 1f;
    [SerializeField] private string sceneName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsSceneExist(sceneName))
            {
                StartCoroutine(Teleport(collision.GetComponent<Rigidbody2D>()));
            }
            else
            {
                Debug.LogWarning("Scene does not found");
            } 
        }
    }


    private System.Collections.IEnumerator Teleport(Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }

    private bool IsSceneExist(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneFileName.Equals(sceneName))
            {
                return true;
            }
        }
        return false;
    }
}
