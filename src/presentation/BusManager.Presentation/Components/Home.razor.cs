﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BusManager.Presentation.Components
{
    public partial class Home
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [CascadingParameter(Name = "HeadingColor")]
        public string Color { get; set; }

        [Parameter]
        public RenderFragment VisitShopContent { get; set; }
    }
}
