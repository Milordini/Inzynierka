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
    [SerializeField] Menu menu;
    [SerializeField] TMP_InputField pathText;
    [SerializeField] GameObject gm;
    [SerializeField] GameObject gm1;
    private void Start()
    {
        LoadFiles();
    }

    public void LoadFiles()
    {
        var paths = Directory.GetFiles("Inzynierka/Assets/Maps", "*.map");
        foreach (var path in paths)
        {
            newBut(path);
        }

    }


    public void selectMap(GameObject gm)
    {
        fL.stop();
        SelectMenager.GetInstance().deleteChildren(gm1);
        for (int x = 0; x < content.childCount; x++)
        {
            if (content.GetChild(x).gameObject == gm)
                fL.loadMapInstant(saves[x]);
        }

    }

    public void zapis()
    {
        if (gm1.transform.childCount == 0)
            return;
        int[] tab = menu.mapOpt();
        string path = "Inzynierka/Assets/Maps\\" + pathText.text + ".map";
        fL.saveMap(path, tab[1], tab[0]);
        newBut(path);

    }

    private void newBut(string path)
    {
        StreamReader sr = new StreamReader(path);
        string o = sr.ReadLine();
        string h = sr.ReadLine();
        string w = sr.ReadLine();
        sr.Close();
        saves.Add(new Save(path, h, w));
        GameObject gm = Instantiate(butPref, content);
        gm.name = path;
        gm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(saves[saves.Count - 1].Name);
        gm.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(h);
        gm.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(w);

        gm.transform.GetComponent<Button>().onClick.AddListener(delegate { selectMap(gm); });
        gm.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { deleteSave(gm); });
        gm.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("maps_icon/" + saves[saves.Count - 1].Name);
    }

    public void deleteSave(GameObject gm)
    {
        foreach (Save s in saves)
        {
            if (s.Name.Equals(gm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text))
            {
                File.Delete("Inzynierka/Assets/Maps\\" + s.Name + ".map");
                File.Delete("Inzynierka/Assets/Resources/maps_icon\\" + s.Name + ".png");
                saves.Remove(s);
                Destroy(gm);
                return;
            }
        }
    }
}
