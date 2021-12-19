using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI
{
	public class PlayHeil : MonoBehaviour
	{
		private Button _button;
		private VideoPlayer _videoPlayer;

		[SerializeField] private RectTransform canvas;

		private void OnEnable()
		{
			_videoPlayer = GetComponent<VideoPlayer>();
			_videoPlayer.loopPointReached += StopVideo;
		}

		private void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(PlaySomething);
		}

		private void PlaySomething()
		{
			if (!_videoPlayer.enabled) return;
			canvas.GetComponent<Canvas>().enabled = false;
			_videoPlayer.Play();
		}

		private void StopVideo(VideoPlayer videoPlayer)
		{
			_videoPlayer.enabled = false;
			canvas.GetComponent<Canvas>().enabled = true;
		}
	}
}
