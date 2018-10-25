﻿using NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BrancheBox : NodeBase
{
    public string Dialogue;

    public List<string> Branches;

    public Color Color;
    public int FontSize = ValueManager.DefaultFontSize;

    public BrancheBox() { }

    public BrancheBox(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle,
        GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint,
        Action<NodeBase> onClickCopyNode, Action<NodeBase> onClickRemoveNode, int id)
    {
        ActionType = ActionTypes.BrancheBox;
        Init(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, onClickInPoint, onClickOutPoint, onClickCopyNode, onClickRemoveNode, id);
    }

    public override void Draw()
    {
        InPoint.Draw();

        GUILayout.BeginArea(Rect, Title, Style);
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(SpacePixel);
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.Space(SpacePixel);

        //init branches
        if (Branches == null)
        { Branches = new List<string>() { "", "" }; }

        GUILayout.Label("Branches");
        GUILayout.BeginVertical("Box");
        for (int i = 0; i < Branches.Count; i++)
        {
            GUILayout.BeginHorizontal();
            var branche = Branches[i];
            Branches[i] = EditorGUILayout.TextField(i + ".", branche);
            //delete branch
            if (i >= 2)
            {
                if (GUILayout.Button("x", GUILayout.Width(20)))
                {
                    Branches.RemoveAt(i);
                }
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        //add new branche
        if (GUILayout.Button("+"))
        {
            Branches.Add("");
        }

        //FontSize = EditorGUILayout.IntField("Font size:", FontSize);
        ////dialogue text box
        //GUILayout.Label("Dialogue");
        //Dialogue = EditorGUILayout.TextArea(Dialogue, GUILayout.Height(50));

        GUILayout.EndVertical();
        GUILayout.Space(SpacePixel);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        base.Draw();
    }

    public override NodeBase Clone(Vector2 pos, int newId)
    {
        var clone = new BrancheBox(pos, Rect.width, Rect.height, Style, SelectedNodeStyle, InPoint.Style,
            OutPoint.Style, InPoint.OnClickConnectionPoint, OutPoint.OnClickConnectionPoint,
            OnCopyNode, OnRemoveNode, newId)
        {
            Dialogue = Dialogue,
            Branches = Branches,
            Color = Color,
            FontSize = FontSize,
        };

        return clone;
    }

    // override object.Equals
    //public override bool Equals(object obj)
    //{
    //    var item = obj as BrancheBox;
    //    if (obj == null) return false;

    //    return Dialogue == item.Dialogue && Branches.SequenceEqual(item.Branches) && Color == item.Color && FontSize == item.FontSize
    //        && Position == item.Position && Id == item.Id;
    //}

    //// override object.GetHashCode
    //public override int GetHashCode()
    //{
    //    return base.GetHashCode();
    //}
}

