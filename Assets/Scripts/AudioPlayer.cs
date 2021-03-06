﻿using System;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : Script
    {
        public AudioSource AudioSource;
        public AudioClip[] Music;
        public AudioClip BlinkSound;
        public AudioClip SuccessSound;
        public AudioClip SwapSound;
        public UITexture MuteButton;

        public void Awake()
        {
            Refresh();
            PlayInGameNext(Music[CRandom.GetRandom(0, Music.Length)]);
        }

        public void Refresh()
        {
            AudioSource.mute = Profile.Mute;
            MuteButton.mainTexture = Resources.Load<Texture2D>(Profile.Mute ? "Images/UI/UnmuteButton" : "Images/UI/MuteButton");
        }

        public void PlayMusicByIndex(object index)
        {
            if (Profile.Mute)
            {
                Profile.Mute = false;
                Refresh();
            }

            PlayInGameNext(Music[Convert.ToInt32(index)]);
        }

        public void PlayBlink()
        {
            PlayEffect(BlinkSound);
        }

        public void PlaySuccess()
        {
            PlayEffect(SuccessSound);
        }

        public void PlaySwap()
        {
            PlayEffect(SwapSound);
        }

        public void ScheduleFix()
        {
            if (Time.timeScale > 0)
            {
                var nexts = Music.Where(i => i != AudioSource.clip).ToList();
                var next = nexts[CRandom.GetRandom(0, nexts.Count)];

                if (AudioSource.isPlaying)
                {
                    TaskScheduler.Kill(888);
                    TaskScheduler.CreateTask(() => PlayInGameNext(next), 888, AudioSource.clip.length - AudioSource.time);
                }
                else
                {
                    PlayInGameNext(next);
                }
            }
            else
            {
                TaskScheduler.Kill(888);
            }
        }

        private void PlayEffect(AudioClip clip)
        {
            if (Profile.Mute) return;

            var audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(clip);
            Destroy(audioSource, clip.length);
        }

        private void PlayInGameNext(AudioClip clip)
        {
            AudioSource.Stop();
            AudioSource.clip = clip;
            AudioSource.loop = false;
            AudioSource.Play();

            var nexts = Music.Where(i => i != clip).ToList();
            var next = nexts[CRandom.GetRandom(0, nexts.Count)];

            TaskScheduler.Kill(888);
            TaskScheduler.CreateTask(() => PlayInGameNext(next), 888, clip.length);
        }
    }
}