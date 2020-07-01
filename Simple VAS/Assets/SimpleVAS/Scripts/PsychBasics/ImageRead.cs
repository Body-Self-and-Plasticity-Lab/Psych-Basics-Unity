using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace UnityPsychBasics
{ 
    public class ImageRead : MonoBehaviour 
    {

        [HideInInspector]
	    public List<Sprite> imageSprites = new List<Sprite>();
        [HideInInspector]
        public List<Sprite> imageSprites2 = new List<Sprite>();
        [HideInInspector]
        public List<Sprite> imagesLikert = new List<Sprite>();
        [HideInInspector]
        public List<string> imageNames1, imageNames2, likertImagesNames;
        public string format;
        public static ImageRead instance;

        public string imageFolder1, imageFolder2, likertImagesFolder;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        // Use this for initialization
        void Start () 
        {
            string[] filePaths = Directory.GetFiles(imageFolder1, "*" + format);

            for(int i = 0; i < filePaths.Length; i++) {
                Texture2D spriteTexture = LoadTexture(filePaths[i]);
                imageSprites.Add(Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), 100f, 0, SpriteMeshType.Tight));
                filePaths[i] = filePaths[i].Replace(imageFolder1, "");
                filePaths[i] = filePaths[i].Replace(format, "");
            }

            imageNames1 = new List<string>(filePaths);
            filePaths = Directory.GetFiles(imageFolder2, "*" + format);

            for (int i = 0; i < filePaths.Length; i++)
            {
                Texture2D spriteTexture = LoadTexture(filePaths[i]);
                imageSprites2.Add(Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), 100f, 0, SpriteMeshType.Tight));
                filePaths[i] = filePaths[i].Replace(imageFolder2, "");
                filePaths[i] = filePaths[i].Replace(format, "");
            }

            imageNames2 = new List<string>(filePaths);
            filePaths = Directory.GetFiles(likertImagesFolder, "*" + format);

            for (int i = 0; i < filePaths.Length; i++)
            {
                Texture2D spriteTexture = LoadTexture(filePaths[i]);
                imagesLikert.Add(Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), 100f, 0, SpriteMeshType.Tight));
                filePaths[i] = filePaths[i].Replace(likertImagesFolder, "");
                filePaths[i] = filePaths[i].Replace(format, "");
            }

            likertImagesNames = new List<string>(filePaths);
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