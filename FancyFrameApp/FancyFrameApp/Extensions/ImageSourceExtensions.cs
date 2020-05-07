using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FancyFrameApp.Extensions
{
    public static  class ImageSourceExtensions
    {
        public static async Task<Stream> GetStreamAsync(this StreamImageSource imageSource, CancellationToken canellationToken = default(CancellationToken))
        {
            if (imageSource.Stream != null)
            {
                return await imageSource.Stream(canellationToken).ConfigureAwait(false);
            }
            return null;
        }
    }
}
