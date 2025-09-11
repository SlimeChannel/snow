namespace snow
{
    using System.Collections.Generic;
    using UnityEngine;
    public static class ColorList
    {
        public static Dictionary<string, Color> List = new()
        {
            {"white", new Color(1, 1, 1)},
            {"red", new Color(1, 0.5f, 0.5f)},
            {"green", new Color(0.5f, 1, 0.5f)},
            {"blue", new Color(0.5f, 0.5f, 1)},
            {"yellow", new Color(1, 1, 0.5f)},
            {"purple", new Color(1, 0.5f, 1)},
            {"cyan", new Color(0.5f, 1, 1)},
            {"orange", new Color(1, 0.75f, 0.5f)},
            {"lime", new Color(0.75f, 1, 0.5f)},
            {"scarlet", new Color(1, 0.5f, 0.75f)},
            {"indigo", new Color(0.75f, 0.5f, 1)},
            {"unnatural", new Color(0.5f, 1, 0.75f)},
            {"azure", new Color(0.5f, 0.75f, 1)},
            {"messy", new Color(0.75f, 0.75f, 0.75f)}
        };
    }
}