using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using MoleculeLogic;
using UnifiedInput;
using UnityEngine.Networking;
using MixedReality.Toolkit.UX;

namespace MoleculeUI
{
    public class BigPanel : BasePanelEx<BigPanel>
    {
        private MoleculeFactory mf;


        public override void Awake()
        {
            base.Awake();

            GameObject go = GameObject.Find("MoleculeFactory");
            if (go != null)
            {
                mf = go.GetComponent<MoleculeFactory>();
            }

        }

        public void OnEnable()
        {
            {
                var buttons = this.GetComponentsInChildren<Button>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    var b = buttons[i];
                    if (b.name.StartsWith("Button"))
                    {
                        string bname = b.name.Substring(6, b.name.Length - 6);
                        if (bname.StartsWith("Recent"))
                        {
                            Text text = buttons[i].GetComponentInChildren<Text>();
                            string filename = PlayerPrefs.GetString(bname);
                            if (filename.Length > 0)
                                filename = Path.GetFileNameWithoutExtension(filename);
                            text.text = filename;
                        }
                    }
                }
            }
            {
                var buttons = this.GetComponentsInChildren<PressableButton>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    var b = buttons[i];
                    if (b.name.StartsWith("Button"))
                    {
                        string bname = b.name.Substring(6, b.name.Length - 6);
                        if (bname.StartsWith("Recent"))
                        {
                            Text text = buttons[i].GetComponentInChildren<Text>();
                            string filename = PlayerPrefs.GetString(bname);
                            if (filename.Length > 0)
                                filename = Path.GetFileNameWithoutExtension(filename);
                            text.text = filename;
                        }
                    }
                }
            }

        }

        public void OpenZipCallback(string path)
        {
            mf.CreateFromZip(path);
            PlayerPrefs.SetString("Recent1", PlayerPrefs.GetString("Recent0"));
            PlayerPrefs.SetString("Recent0", path);
            DynamicPanel.Instance.SetAllVis(false);
        }

        public void OpenFileCallback(string path)
        {
            mf.CreateFromFile(path);
            DynamicPanel.Instance.SetAllVis(false);
        }

        public void OpenRecent(string path)
        {
            mf.CreateFromZip(path);
            DynamicPanel.Instance.SetAllVis(false);
        }

        public void loadPDBFromInternet(string pdbStr)
        {
            string pdbid = pdbStr;
            pdbid = pdbid.Replace("\n", "");
#if UNITY_WEBGL && !UNITY_EDITOR
            string url = "./fetch.php?http://www.rcsb.org/pdb/download/downloadFile.do?fileFormat=pdb&compression=NO&structureId=" + pdbid;
#else
            string url = "http://www.rcsb.org/pdb/download/downloadFile.do?fileFormat=pdb&compression=NO&structureId=" + pdbid;
#endif
            mf.CreateFromZip("userdata/receptors/1AQ1.pdbqt");
            //mf.StartCoroutine(mf.CreateFromInternet(url, "pdb" + pdbid, ".pdb"));
            DynamicPanel.Instance.SetAllVis(false);
        }

        private void OpenQRCode(string path)
        {
            if (path == null)
                return;

            if (path.StartsWith("zip:"))
            {
                path = path.Substring(4);
                mf.CreateFromZip(path);
                PlayerPrefs.SetString("Recent1", PlayerPrefs.GetString("Recent0"));
                PlayerPrefs.SetString("Recent0", path);
            }
            else if (path.StartsWith("file:"))
            {
                path = path.Substring(5);
                mf.CreateFromFile(path);
            }
            else if (path.StartsWith("resource:"))
            {
                path = path.Substring(9);
                mf.CreateFromResource(path);
            }
            else if (path.StartsWith("http:")||path.StartsWith("https:")||path.StartsWith("ftp:"))
            {
                mf.StartCoroutine(mf.CreateFromInternet(path, Path.GetFileNameWithoutExtension(path), ".pdb"));
            }
            DynamicPanel.Instance.SetAllVis(false);
        }

        public override void OnClick(GameObject sender)
        {
            string objname = sender.name;
            if (objname.StartsWith("Button"))
            {
                string bname = objname.Substring(6, objname.Length - 6);
                if (bname.StartsWith("Recent"))
                {
                    string path = PlayerPrefs.GetString(bname);
                    OpenRecent(path);
                }
                else if (bname == "LoadFromFile")
                {
                    FileBrowserPanelEx filebrowserpanel = FileBrowserPanelEx.Instance;
                    filebrowserpanel.SetVisible(true);
                    filebrowserpanel.Selected -= OpenFileCallback;
                    filebrowserpanel.Selected += OpenFileCallback;
                    filebrowserpanel.Init("*.pdb|*.ent|*.mol2|*.ml2|*.sy2|*.pdbqt", null);
                    CruveMainPanel.Instance.SetVisible(false);
                    CurveOverlayPanel.Instance.SetVisible(false);
                }
                else if (bname == "LoadFromResources")
                {
                    ZipBrowserPanelEx filebrowserpanel = ZipBrowserPanelEx.Instance;
                    filebrowserpanel.SetVisible(true);
                    filebrowserpanel.Selected -= OpenZipCallback;
                    filebrowserpanel.Selected += OpenZipCallback;
                    filebrowserpanel.Init("*.pdb|*.ent|*.mol2|*.ml2|*.sy2|*.pdbqt", null);
                    CruveMainPanel.Instance.SetVisible(false);
                    CurveOverlayPanel.Instance.SetVisible(false);
                }
                else if (bname == "LoadFromInternet")
                {
                    //Transform pdbidfieldgo = sender.transform.parent.FindChild("InputField");
                    //if (pdbidfieldgo != null)
                    //{
                    //    Transform tplaceholder = pdbidfieldgo.FindChild("Placeholder");
                    //    if (tplaceholder != null)
                    //    {
                    //        Text t = tplaceholder.GetComponent<Text>();
                    //        string pdbid = t.text;
                    //        pdbid = pdbid.Replace("\n", "");
                    //        string url = "http://www.rcsb.org/pdb/download/downloadFile.do?fileFormat=pdb&compression=NO&structureId=" + pdbid;
                    //        mf.StartCoroutine(mf.CreateFromInternet(url, "pdb" + pdbid, ".pdb"));
                    //    }
                    //}
                    InputField inputfield = sender.transform.parent.GetComponentInChildren<InputField>();
                    if (inputfield != null)
                    {
                        loadPDBFromInternet(inputfield.text);
                    }
                }
                else if (bname == "LoadByQRCode")
                {
                    QRCodeScanPanel filebrowserpanel = QRCodeScanPanel.Instance;
                    filebrowserpanel.SetVisible(true);
                    filebrowserpanel.Selected -= OpenQRCode;
                    filebrowserpanel.Selected += OpenQRCode;
                    CruveMainPanel.Instance.SetVisible(false);
                    CurveOverlayPanel.Instance.SetVisible(false);
                }

            }
        }


        public void Update()
        {
            CheckTextField();

        }


        private void CheckTextField()
        {
            if (!UnifiedInputManager.GetMouseButtonDown(0))
                return;

            InputField currentInputField = null;
            if (UnifiedInputManager.OnUI)
            {
                if (UnifiedInputManager.CurObj)
                {
                    currentInputField = UnifiedInputManager.CurObj.GetComponentInParent<InputField>();
                }
            }
            if (currentInputField != null && currentInputField.GetComponentInParent<BigPanel>() != null)
            {
                KeyboardPanelEx keyboardpanel = KeyboardPanelEx.Instance;
                if (keyboardpanel != null)
                {
                    Vector3 pos = currentInputField.transform.position
                        - Camera.main.transform.forward * 0.2f;
                    pos += new Vector3(0, -0.3f, 0);
                    keyboardpanel.transform.position = pos;
                    keyboardpanel.inputfield = currentInputField;
                    keyboardpanel.gameObject.SetActive(true);
                    //这里只会+，不会-，不知道该在哪里-
                    keyboardpanel.onLoadingPDB -= loadPDBFromInternet;
                    keyboardpanel.onLoadingPDB += loadPDBFromInternet;
                }
            }


        }

    }
}