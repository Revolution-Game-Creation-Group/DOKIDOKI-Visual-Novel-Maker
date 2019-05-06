﻿using System;
using UnityEditor;
using UnityEngine;

namespace DokiVnMaker.MyEditor.Items
{
    [Serializable]
    public class BackgroundItem : NodeBase
    {
        [NonSerialized]
        public Sprite Image;

        public string Path;
        public bool IsWait;

        public BackgroundItem()
        {

            ActionType = ActionTypes.BackgroundItem;

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
                var origin = AssetDatabase.LoadAssetAtPath(Path, typeof(Sprite)) as Sprite;

                if (origin != null)
                {
                    //set background image
                    Image = origin;
                }
                Initialize = false;
            }

            //Choose image
            GUILayout.BeginHorizontal();
            GUILayout.Label("Image", WhiteTxtStyle, GUILayout.Width(LabelWidth));
            Image = EditorGUILayout.ObjectField(Image, typeof(Sprite), false) as Sprite;
            GUILayout.EndHorizontal();

            //is wait for background appear
            GUILayout.BeginHorizontal();
            GUILayout.Label("Is wait", WhiteTxtStyle, GUILayout.Width(LabelWidth));
            IsWait = EditorGUILayout.Toggle(IsWait);
            GUILayout.EndHorizontal();

            if (Image != null)
            {
                //get path
                Path = AssetDatabase.GetAssetPath(Image);
                //show preview
                GUILayout.Label(Image.texture, GUILayout.Width(200), GUILayout.Height(113));
                if (Rect.height == DefaultRectHeight) Rect.height = DefaultRectHeight + 110;
            }
            else
            {
                //clear path
                Path = "";
                if (Rect.height != DefaultRectHeight) Rect.height = DefaultRectHeight;
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
            var clone = new BackgroundItem()
            {
                Initialize = true,
                Path = Path,
                IsWait = IsWait,
            };
            clone.Init(pos, Rect.width, Rect.height, Style, SelectedNodeStyle, InPoint.Style,
                OutPoint.Style, InPoint.OnClickConnectionPoint, OutPoint.OnClickConnectionPoint,
                OnCopyNode, OnRemoveNode, newId);

            return clone;
        }
    }
}