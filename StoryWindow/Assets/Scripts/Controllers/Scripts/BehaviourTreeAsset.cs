using System;
using System.IO;
using System.Windows.Forms;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Application = UnityEngine.Application;

public class BehaviourTreeAsset : ScriptableObject
{
    [SerializeField] internal bool IsLoaded = false;
    [SerializeField] private string _directory;
    
    private BehaviourTree _behaviourTree;

    public BehaviourTree BehaviourTree => _behaviourTree;

    public void CreateTree()
    {
        var saveDialog = new SaveFileDialog();
        saveDialog.InitialDirectory = Application.dataPath;
        saveDialog.Title = "Create new behaviour tree";
        saveDialog.DefaultExt = "json";
        saveDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            var newTree = new BehaviourTree();
            var json = JsonConvert.SerializeObject(newTree);
            File.WriteAllText(saveDialog.FileName, json);
            _directory = saveDialog.FileName;
            _behaviourTree = newTree;

            AssetDatabase.Refresh();
        }
    }

    public void LoadTree()
    {
        if (_directory != String.Empty)
        {
            BinaryReader binaryReader = new BinaryReader(File.OpenRead(_directory));
            var json = binaryReader.ReadString();
            var loadedTree = JsonConvert.DeserializeObject<BehaviourTree>(json);
            
            if(loadedTree == null)
                return;

            _behaviourTree = loadedTree;
            return;
        }

        var openDialog = new OpenFileDialog();
        openDialog.InitialDirectory = Application.dataPath;
        openDialog.Title = "Create new behaviour tree";
        openDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";

        if (openDialog.ShowDialog() == DialogResult.OK)
        {
            BinaryReader binaryReader = new BinaryReader(File.OpenRead(openDialog.FileName));
            var json = binaryReader.ReadString();
            var loadedTree = JsonConvert.DeserializeObject<BehaviourTree>(json);
            
            if(loadedTree == null)
                return;

            _behaviourTree = loadedTree;
            _directory = openDialog.FileName;
        }
    }

    public void SaveTree()
    {
        if(_behaviourTree == null)
            return;
        
        if(_directory == string.Empty)
            return;
        
        var json = JsonConvert.SerializeObject(_behaviourTree);
        BinaryWriter binaryWriter = new BinaryWriter(File.Create(_directory));
        binaryWriter.Write(json);
    }

    public void OpenStoryWindow()
    {
        if(_behaviourTree == null)
            return;
        
        BehaviourTreeEditor.OpenWindow(_behaviourTree);
    }
}
