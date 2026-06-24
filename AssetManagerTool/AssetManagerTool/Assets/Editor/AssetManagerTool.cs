using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class AssetManagerTool : EditorWindow
{
    [MenuItem("AssetManagerTool/Open")]
    public static void ShowAssetManagerWindow()
    {
        AssetManagerTool wnd = GetWindow<AssetManagerTool>();
        wnd.titleContent = new GUIContent("AssetManagerTool");
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

        var textureAssets = AssetUtility.GetTextureAssets();
        if(textureAssets.Count == 0) return;

        // Show texture assets
        foreach(var texture in textureAssets)
        {
            var card = AssetUtility.CreateDefaultCard();
            
            var icon = AssetUtility.CreateImage(48, 48);
            icon.image = AssetPreview.GetMiniThumbnail(texture.asset);

            var tex_label = AssetUtility.CreateDefaultLabel();
            tex_label.text = $"{texture.assetName}{texture.extensionName}";

            card.Add(icon);
            card.Add(tex_label);

            assetContainer.Add(card); 
        }

        var audioClips = AssetUtility.GetAudioClips();
        if(audioClips.Count == 0) return;

        // Show Audio clips
        foreach(var audioClip in audioClips)
        {
            var card = AssetUtility.CreateDefaultCard();

            var icon = AssetUtility.CreateImage(48, 48);
            icon.image = AssetPreview.GetMiniThumbnail(audioClip.asset);

            var audio_label = AssetUtility.CreateDefaultLabel();
            audio_label.text = $"{audioClip.assetName}{audioClip.extensionName}";

            card.Add(icon);
            card.Add(audio_label);

            assetContainer.Add(card); 
        }
    }
}
