﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DokiVnMaker.Story
{
    [NodeTint("#ff99ff")]
    public class BackgroundImage : StoryNodeBase
    {
        // Use this for initialization
        protected override void Init()
        {
            name = "Background image";
            base.Init();
        }

        public Sprite image;
    }
}
