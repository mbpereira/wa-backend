
using WaServer.Consts;

namespace WaServer.Helpers
{
    public static class Extensions
    {
        public static int? GetSkip(this int? sender, int size = App.PageSize)
        {
            if (sender.HasValue)
                return (sender.Value - 1) * size;
            return null;
        }

        public static int? GetTake(this int? sender, int size = App.PageSize)
        {
            if (sender.HasValue)
                return sender * size;
            return null;
        }
    }
}
