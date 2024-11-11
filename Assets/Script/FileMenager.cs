using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileMenager : MonoBehaviour
{
    [SerializeField] GameObject butPref;
    [SerializeField] List<Save> saves = new List<Save>();
    [SerializeField] Transform content;
    [SerializeField] fileLoader fL;
    [SerializeField] GameObject gm;
    [SerializeField] GameObject gm1;
    private void Start()
    {
        LoadFiles();
    }

    public void LoadFiles()
    {
        var paths = Directory.GetFiles("Assets/Maps","*.map");
        Debug.Log(paths.Length);
        foreach (var path in paths)
        {
            StreamReader sr = new StreamReader(path);
            string o = sr.ReadLine();
            string h = sr.ReadLine();
            string w = sr.ReadLine();
            saves.Add(new Save(path, h, w));
            GameObject gm = Instantiate(butPref, content);
            gm.name = path;
            gm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(saves[saves.Count - 1].Name);
            gm.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(h);
            gm.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(w);
            sr.Close();

            gm.transform.GetComponent<Button>().onClick.AddListener(delegate { selectMap(gm); });
            gm.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("maps_icon/" + saves[saves.Count - 1].Name);
        }

    }


    public void selectMap(GameObject gm)
    {
        fL.stop();
       SelectMenager.GetInstance().deleteChildren(gm1);
        for(int x = 0;x<content.childCount;x++)
        {
            if(content.GetChild(x).gameObject == gm)
                fL.loadMapInstant(saves[x]);
        }

    }



}
