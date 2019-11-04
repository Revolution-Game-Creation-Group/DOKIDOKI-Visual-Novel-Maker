﻿using UnityEngine;
using UnityEditor;
using XNode;

namespace DokiVnMaker.Story
{
    [NodeTint("#ffff99")]
    public class StartPoint : Node
    {
        [outputlist(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public StoryNodeBase output;

        protected override void Init()
        {   
            name = "START";
            base.Init();
        }
    }
}