using UnityEngine;
using UnityEngine.UIElements;

public class MenuButtonEvent : MonoBehaviour
{

    [SerializeField] VisualTreeAsset menuUI;
    [SerializeField] VisualTreeAsset StageUI;
    private UIDocument UID;
    private Button StartButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        UID = GetComponent<UIDocument>();

        StartButton = UID.rootVisualElement.Q("StartButton") as Button;

        StartButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
    }

    private void OnPlayButtonClick(ClickEvent evt)
    {
        Debug.Log("시작 버튼 누름");
        UID.visualTreeAsset = StageUI;
        UID.rootVisualElement.schedule.Execute(() => {
        var controller = GetComponent<StageUIController>();
        controller?.Init(); 
        }).ExecuteLater(1);
    }


    private void OnDisable()
    {
        StartButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
    }



}
