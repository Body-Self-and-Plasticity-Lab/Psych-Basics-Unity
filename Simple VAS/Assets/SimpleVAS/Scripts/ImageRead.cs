using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace SimpleVAS { 
    public class ImageRead : MonoBehaviour {

	    public static List<Sprite> imageSprites = new List<Sprite>();

        // Use this for initialization
	    void Start () {

            string[] filePaths = Directory.GetFiles("./Images/", "*.jpg");

            foreach(string path in filePaths) {
                Texture2D spriteTexture = LoadTexture(path);
                imageSprites.Add(Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), 100f, 0, SpriteMeshType.Tight));
            }
        
        }

        public Texture2D LoadTexture(string FilePath) //adapted from https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
        {
            // Load a PNG or JPG file from disk to a Texture2D
            Texture2D _tex2D;
            byte[] _fileData;

            if (File.Exists(FilePath)) {
                _fileData = File.ReadAllBytes(FilePath);
                _tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (_tex2D.LoadImage(_fileData))           // Load the imagedata into the texture (size is set automatically)
                    return _tex2D;                 // If data = readable -> return texture
            }
            return null;                     // Return null if load failed
        }

    }


}