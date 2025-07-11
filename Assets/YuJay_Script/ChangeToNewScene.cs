using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToNewScene : MonoBehaviour
{
    
    

    public void ChangeSceneTo(string sceneName)
    {

        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(!SingletonUICanvas.theStaticCanvas.gameObject.activeSelf);
        SceneManager.LoadScene(sceneName);
        

    }

    
}
