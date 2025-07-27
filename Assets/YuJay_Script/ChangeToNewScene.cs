using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToNewScene : MonoBehaviour
{
    public GameObject balatoGroup;
    

    public void ChangeSceneToCyber()
    {

        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("Cyberpunk Level");

    }
    public void ChangeSceneToFishing()
    {

        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("YuJay_Fishing");

    }
    public void ChangeSceneToDino()
    {

        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("Dino Level");
    }
    
    public void ChangeSceneTo(string sceneName)
    {

        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(true);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
    
    
    public void ChangeSceneToDinoStory()
    {
        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("Dino Story Level");
    }
    
    public void ChangeSceneToCyberStory()
    {
        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("Cyberpunk Story Level");
    }
    
    
    public void ChangeSceneToFishingStory()
    {
        SingletonUICanvas.theStaticCanvas.gameObject.SetActive(false);
        SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(false);
        balatoGroup.SetActive(false);
        SceneManager.LoadScene("YuJay_Fishing Story");
    }
}
