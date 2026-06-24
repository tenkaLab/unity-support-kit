using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UIElements;

public class AssetUtility
{
    private static VisualElement selectedCard;

    public class AssetInfo<T> where T : Object
    {
        public T asset {get;}
        public string extensionName {get;}
        public string assetName {get;}

        public AssetInfo(T asset, string extensionName, string assetName)
        {
            this.asset = asset;
            this.extensionName = extensionName;
            this.assetName = assetName;
        }
    }

    public static List<AssetInfo<Texture2D>> GetTextureAssets()
    {
        List<AssetInfo<Texture2D>> ret_textures = new List<AssetInfo<Texture2D>>();
        
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Textures" });

        foreach(var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if(texture == null) continue;

            ret_textures.Add(new AssetInfo<Texture2D>(texture, Path.GetExtension(path), texture.name));
        }

        return ret_textures;
    }

    public static List<AssetInfo<AudioClip>> GetAudioClips()
    {
        List<AssetInfo<AudioClip>> ret_audioClips = new List<AssetInfo<AudioClip>>();

        string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Sounds" });

        foreach(var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AudioClip audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            if(audioClip == null) continue;

            ret_audioClips.Add(new AssetInfo<AudioClip>(audioClip, Path.GetExtension(path), audioClip.name));
        }

        return ret_audioClips;
    }
    private static void SetBorderColor(VisualElement element, Color color)
    {
        element.style.borderTopColor = color;
        element.style.borderRightColor = color;
        element.style.borderBottomColor = color;
        element.style.borderLeftColor = color;
    }

    public static VisualElement CreateDefaultCard()
    {
        VisualElement card = new VisualElement();
        card.style.width = 120;
        card.style.height = 110;
        card.style.alignItems = Align.Center;
        card.style.justifyContent = Justify.Center;
        card.style.marginRight = 8;
        card.style.marginBottom = 8;

        card.style.borderTopWidth = 2;
        card.style.borderRightWidth = 2;
        card.style.borderBottomWidth = 2;
        card.style.borderLeftWidth = 2;

        Color transparent = new Color(1, 1, 1, 0);
        Color selectedColor = new Color(0.2f, 0.9f, 1.0f, 1.0f);
        Color hoverColor = new Color(0.4f, 0.7f, 1.0f, 1.0f);

        card.RegisterCallback<MouseEnterEvent>(evt =>
        {
            if(card != selectedCard)
            {
                SetBorderColor(card, hoverColor); 
            }
        });

        card.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            if(card != selectedCard)
            {
                SetBorderColor(card, transparent); 
            }
        });

        card.RegisterCallback<ClickEvent>(evt =>
        {
            if (selectedCard != null)
            {
                SetBorderColor(selectedCard, transparent);
            }

            selectedCard = card;
            SetBorderColor(card, selectedColor);
        });

        return card;
    }

    public static Label CreateDefaultLabel()
    {
        Label label = new Label();
        label.style.unityTextAlign = TextAnchor.MiddleCenter;
        label.style.whiteSpace = WhiteSpace.Normal;

        return label;
    }

    public static Image CreateImage(int width, int height)
    {
        Image icon = new Image();
        icon.style.width = width;
        icon.style.height = height;

        return icon;
    }
}