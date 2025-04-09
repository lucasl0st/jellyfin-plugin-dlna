using System.IO;
using System.Linq;
using Jellyfin.Plugin.Dlna.Model;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dlna;
using MediaBrowser.Model.Session;

namespace Jellyfin.Plugin.Dlna.PlayTo;

/// <summary>
/// Defines the <see cref="PlaylistItemFactory" />.
/// </summary>
public static class PlaylistItemFactory
{
    /// <summary>
    /// Creates a new playlist item.
    /// </summary>
    /// <param name="item">The <see cref="Photo"/>.</param>
    /// <param name="profile">The <see cref="DlnaDeviceProfile"/>.</param>
    public static PlaylistItem Create(Photo item, DlnaDeviceProfile profile)
    {
        var playlistItem = new PlaylistItem { StreamInfo = new StreamInfo { ItemId = item.Id, MediaType = DlnaProfileType.Photo, DeviceProfile = profile }, Profile = profile };

        var transcodingProfile = profile.TranscodingProfiles
            .FirstOrDefault(i => i.Type == DlnaProfileType.Photo);

        if (transcodingProfile is not null)
        {
            playlistItem.StreamInfo.PlayMethod = PlayMethod.Transcode;
            playlistItem.StreamInfo.Container = "." + transcodingProfile.Container.TrimStart('.');
        }

        return playlistItem;
    }

    private static bool IsSupported(DirectPlayProfile profile, Photo item)
    {
        return false;
    }
}
