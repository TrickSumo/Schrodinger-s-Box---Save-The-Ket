using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogsCanvasController : MonoBehaviour
{

    public TextMeshProUGUI dailogBox;
    public Button confimButton;
    // Start is called before the first frame update
    private void Awake() {
        gameObject.SetActive(false);
        confimButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";
        confimButton.onClick.AddListener(CloseCanvas);

    }
public void updateDialog(string dialogText){
    gameObject.SetActive(true);
    dailogBox.text=dialogText;
    StartCoroutine(DisableDialogCanvas());
}

private IEnumerator DisableDialogCanvas() {
    yield return new WaitForSeconds(9f);
    gameObject.SetActive(false);
}

private void CloseCanvas(){
    gameObject.SetActive(false);
}


}
