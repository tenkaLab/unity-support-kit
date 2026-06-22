using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class AssetManagerTool : EditorWindow
{
    [MenuItem("AssetManagerTool/Open")]
    public static void ShowAssetManagerWindow()
    {
        AssetManagerTool wnd = GetWindow<AssetManagerTool>();
        wnd.titleContent = new GUIContent("AssetManagerTool");
    }

    private List<Texture2D> GetTextureAssets()
    {
        List<Texture2D> ret_textures = new List<Texture2D>();
        
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Textures" });

        foreach(var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if(texture == null) continue;

            ret_textures.Add(texture);
        }

        return ret_textures;
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Search AssetName");
        label.style.fontSize = 20;
        label.style.marginTop = 10;
        label.style.marginBottom = 10;
        root.Add(label);

        TextField searchInput = new TextField();
        searchInput.style.marginBottom = 10;
        searchInput.style.maxWidth = 300;
        searchInput.style.height = 30;
        root.Add(searchInput);

        VisualElement assetContainer = new VisualElement();
        assetContainer.style.flexDirection = FlexDirection.Row;
        assetContainer.style.flexWrap = Wrap.Wrap;
        assetContainer.style.alignItems = Align.FlexStart;

        root.Add(assetContainer);

        var textureAssets = GetTextureAssets();
        if(textureAssets.Count == 0) return;

        // Show texture assets
        foreach(var asset in textureAssets)
        {
            Image assetIcon = new Image();
            assetIcon.style.marginLeft = 10;
            assetIcon.style.marginRight = 10;
            assetIcon.style.marginBottom = 10;
            assetIcon.image = AssetPreview.GetMiniThumbnail(asset);
            assetIcon.style.width = 100;
            assetIcon.style.height = 100;
            assetContainer.Add(assetIcon);   
        }
    }
}
