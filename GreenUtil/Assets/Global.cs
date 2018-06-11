using System;

namespace GreenUtil.Assets
{
    internal static class Global
    {
        internal static Random Random = new Random((int)DateTime.Now.Ticks);
    }
}
