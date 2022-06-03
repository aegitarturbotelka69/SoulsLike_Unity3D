namespace SLGame.UI
{
    public static class UIExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_image"> Reference of image </param>
        /// <param name="p_transparency"> Transparency. By default its changes to opaque </param>
        public static void SetTransparency(this UnityEngine.UI.Image p_image, float p_transparency = 1f)
        {
            if (p_image != null)
            {
                UnityEngine.Color __alpha = p_image.color;
                __alpha.a = p_transparency;
                p_image.color = __alpha;
            }
        }
    }
}