﻿using DokiVnMaker.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DokiVnMaker.MyEditor.Items
{
    [Serializable]
    public class Sound : AudioBase
    {
        public int TrackIndex;

        public Sound() { }

        public Sound(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint,
            Action<NodeBase> onClickCopyNode, Action<NodeBase> onClickRemoveNode, int id)
        {
            ActionType = ActionTypes.Sound;
            Init(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, onClickInPoint, onClickOutPoint, onClickCopyNode, onClickRemoveNode, id);
        }

        public override void Draw()
        {
            GUILayout.BeginArea(Rect, Title, Style);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(SpacePixel);
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.Space(SpacePixel);

            //initialize
            if (Initialize)
            {
                //find origin object
                var origin = AssetDatabase.LoadAssetAtPath(AudioPath, typeof(AudioClip)) as AudioClip;

                if (origin != null)
                {
                    //set background image
                    MyAudio = origin;
                }
                Initialize = false;
            }

            //Choose audio
            GUILayout.BeginHorizontal();
            GUILayout.Label("Sound source", WhiteTxtStyle, GUILayout.Width(LabelWidth));
            MyAudio = EditorGUILayout.ObjectField(MyAudio, typeof(AudioClip), false) as AudioClip;
            GUILayout.EndHorizontal();

            //get audio path
            if (MyAudio != null) AudioPath = AssetDatabase.GetAssetPath(MyAudio);

            //audio volume
            GUILayout.BeginHorizontal();
            GUILayout.Label("Volume", WhiteTxtStyle, GUILayout.Width(LabelWidth));
            Volume = EditorGUILayout.Slider(Volume, 0, 1);
            GUILayout.EndHorizontal();

            //find sound manager object
            var obj = GameObject.FindGameObjectsWithTag("doki_sound_manager").FirstOrDefault();
            if (obj != null)
            {
                var soundManager = obj.GetComponent<SoundManager>();

                //find all sound tracks
                if (soundManager.AudioTracks != null && soundManager.AudioTracks.Length > 0)
                {
                    //sound track list for seletion
                    var list = new List<string>();
                    for (int i = 0; i < soundManager.AudioTracks.Length; i++)
                    {
                        list.Add((i + 1).ToString());
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Track", WhiteTxtStyle, GUILayout.Width(LabelWidth));
                    TrackIndex = EditorGUILayout.Popup(TrackIndex, list.ToArray());
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.EndVertical();
            GUILayout.Space(SpacePixel);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            InPoint.Draw();
            OutPoint.Draw();

            base.Draw();
        }

        public override NodeBase Clone(Vector2 pos, int newId)
        {
            var clone = new Sound(pos, Rect.width, Rect.height, Style, SelectedNodeStyle, InPoint.Style,
                OutPoint.Style, InPoint.OnClickConnectionPoint, OutPoint.OnClickConnectionPoint,
                OnCopyNode, OnRemoveNode, newId)
            {
                Initialize = true,
                AudioPath = AudioPath,
                Volume = Volume,
                TrackIndex = TrackIndex
            };

            return clone;
        }
    }
}