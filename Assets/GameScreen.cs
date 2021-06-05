using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenMain
{
    int id;
    public Transform container;
    ContentData.Content content;
    public ActionButton button;
    public Text  titleField;

    public override void Init() {
        content = Data.Instance.contentData.content[id];
        titleField.text = content.situacion;
        foreach (ContentData.SituacionData data in content.situaciones)
        {
            ActionButton newButton = Instantiate(button, container);
            newButton.transform.localScale = Vector3.one;
            newButton.OnInit(data);
        }
    }

}
