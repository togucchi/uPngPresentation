using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Toguchi
{
	public class PngPresentationManager : MonoBehaviour {
		public RenderTexture targetTexture; //描画するRenderTexture

		[SerializeField]
		string folderName = ""; //nullの場合はStreamingAssets直下

		[SerializeField]
		bool useArrowKey = true; //trueにすると矢印キーでスライド遷移

		List<Texture2D> textureList;

		int currentIndex = 0;

		// Use this for initialization
		void Start () {
			textureList = new List<Texture2D>();
			GetTextures(folderName);
			
			if(textureList[0] != null) 
				Graphics.Blit(textureList[0],targetTexture);
		}
		
		// Update is called once per frame
		void Update () {
			if(useArrowKey)
			{
				if(Input.GetKeyDown(KeyCode.RightArrow))
				{
					NextSlide();
				}
				else if(Input.GetKeyDown(KeyCode.LeftArrow))
				{
					PreviousSlide();
				}
			}
		}

		void NextSlide()
		{
			if(currentIndex < textureList.Count - 1)
			{
				currentIndex++;
				Graphics.Blit(textureList[currentIndex], targetTexture);
			}
		}

		void PreviousSlide()
		{
			if(currentIndex > 0)
			{
				currentIndex--;
				Graphics.Blit(textureList[currentIndex], targetTexture);
			}
		}

		void GetTextures(string folder)
		{
			string folderPath = Path.Combine(Application.streamingAssetsPath, folder);

			DirectoryInfo di = new DirectoryInfo(@folderPath);
			FileInfo[] files = di.GetFiles("*.png", SearchOption.AllDirectories);

			foreach (System.IO.FileInfo f in files)
			{
				textureList.Add(PngReader.ReadPng(f.FullName));
			}
		}
	}

	//参考: https://qiita.com/r-ngtm/items/6cff25643a1a6ba82a6c
	public static class PngReader
	{
		static byte[] ReadPngFile(string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			BinaryReader bin = new BinaryReader(fileStream);
			byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

			bin.Close();

			return values;
		}

		public static Texture2D ReadPng(string path)
		{
			byte[] readBinary = ReadPngFile(path);

			int pos = 16; // 16バイトから開始

			int width = 0;
			for (int i = 0; i < 4; i++)
			{
				width = width * 256 + readBinary[pos++];
			}

			int height = 0;
			for (int i = 0; i < 4; i++)
			{
				height = height * 256 + readBinary[pos++];
			}

			Texture2D texture = new Texture2D(width, height);
			texture.LoadImage(readBinary);

			return texture;
		}
	}
}

