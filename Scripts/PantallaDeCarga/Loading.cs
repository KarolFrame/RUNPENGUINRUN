using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        string levelToLoad = LevelLoader.nextLevel;

        StartCoroutine(MakeTheLoad(levelToLoad));
    }

    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while(operation.isDone == false)
        {
            yield return null;
        }
    }

}
