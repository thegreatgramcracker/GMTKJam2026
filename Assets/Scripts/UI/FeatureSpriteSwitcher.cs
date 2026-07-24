using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FeatureSpriteSwitcher : MonoBehaviour
{
    public string relativeDir;
    public FeatureSprite[] featureSprites;
    Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        FeatureSprite feature = featureSprites.FirstOrDefault(f => f.feature == MapManager.instance.player.GetFeatureRelative(relativeDir));
        if (feature != null)
        {
            img.enabled = true;
            img.sprite = feature.sprite;
        }
        else
        {
            img.enabled = false;
        }
    }
}
[System.Serializable]
public class FeatureSprite
{
    public FeatureType feature;
    public Sprite sprite;
}
