﻿using NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CGInfoItem : NodeBase
{
    [NonSerialized]
    public bool Initialize;

    //index for story box selector
    [NonSerialized]
    public int Index;
    public string Path;

    public bool IsWait = true;

    public CGInfoItem() { }

    public CGInfoItem(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle,
        GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint,
        Action<NodeBase> onClickRemoveNode, int id)
    {
        ActionType = ActionTypes.CGInfoItem;
        Init(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, onClickInPoint, onClickOutPoint, onClickRemoveNode, id);
    }

    public override void Draw()
    {
        InPoint.Draw();
        OutPoint.Draw();

        GUILayout.BeginArea(Rect, Title, Style);
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(SpacePixel);
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.Space(SpacePixel);

        //get all cg
        var list = ObjectInfoHelper.GetCGsName();

        if (Initialize)
        {
            //find origin object
            var origin = AssetDatabase.LoadAssetAtPath(Path, typeof(Sprite)) as Sprite;
            if (origin != null)
            {
                //set index
                Index = list.IndexOf(list.Where(c => c == origin.name).FirstOrDefault());

            }
            Initialize = false;
        }

        //selector for cg
        GUILayout.BeginHorizontal();
        GUILayout.Label("CG", WhiteTxtStyle, GUILayout.Width(LabelWidth));
        Index = EditorGUILayout.Popup(Index, list.ToArray());
        GUILayout.EndHorizontal();
        //set cg path
        Path = ValueManager.CGPath + list[Index] + ".jpg";

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //load preview cg
        string path = "Assets/GameSources/CGs/" + list[Index] + ".jpg";
        var imgPriveiw = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
        if (imgPriveiw != null) GUILayout.Label(imgPriveiw.texture, GUILayout.Width(200), GUILayout.Height(113));
        GUILayout.EndHorizontal();

        //is wait for CG appear
        GUILayout.BeginHorizontal();
        GUILayout.Label("Is wait", WhiteTxtStyle, GUILayout.Width(LabelWidth));
        IsWait = EditorGUILayout.Toggle(IsWait);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.Space(SpacePixel);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        base.Draw();
    }


    // override object.Equals
    public override bool Equals(object obj)
    {
        var item = obj as CGInfoItem;
        if (obj == null) return false;

        return Path == item.Path && IsWait == item.IsWait && Position == item.Position && Id == item.Id;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
