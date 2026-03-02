using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.ComponentModel.Design.Serialization;
using UnityEditor;

public class StageUIController : MonoBehaviour
{
    
    public void Init()
    {

        Debug.Log("Stage UI 컴포넌트 호출");
        var root = GetComponent<UIDocument>().rootVisualElement;

        for (int i = 1; i <= 7; i++)
        {
            string buttonName = $"Stage{i}";
            Button stageButton = root.Q<Button>(buttonName);
            int stageIndex = i;
            if (stageButton != null)
            {
                stageButton.RegisterCallback<ClickEvent>(evt => LoadStage(stageIndex));
            }
        }

    Button backBtn = root.Query<Button>("BackButton");

    if (backBtn != null)
    {
        backBtn.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene("MainMenu"));
    }
            

    }

    private void LoadStage(int num)
    {
        string sceneName = $"Stage{num}";
        SceneManager.LoadScene(sceneName);
    }
    

}
