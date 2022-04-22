using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
	public Text dialog;
	string _fileName = "NoGame_kr.xml";
	float setTime;
	int i = 0;
	// Start is called before the first frame update
	void Start()
    {
		//Load_XML(_fileName);
	}

	private void Load_XML(string FileName)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/Resources/" + FileName);
		i += 1;
		XmlNodeList text_Table = xmlDoc.GetElementsByTagName("A1_"+ i +"");
			
		foreach (XmlNode text in text_Table)
		{
			dialog.text = text.InnerText;
		}
		//string[] Dialogs = new string[];
	}

    void Update()
    {
        setTime += Time.deltaTime;
        if(setTime > 3)
        {
			Load_XML(_fileName);
			setTime = 0;
        }
    }
}
