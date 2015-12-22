using System.ComponentModel;
using System.Runtime.Serialization;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public enum MediaType
    {
        [Description("jpeg")]
        [EnumMember(Value = "image/jpeg")]
        JPEG,
        [Description("png")]
        [EnumMember(Value = "image/png")]
        PNG,
        [Description("bmp")]
        [EnumMember(Value = "image/bmp")]
        BMP,
        [Description("gif")]
        [EnumMember(Value = "image/gif")]
        GIF,
        [Description("mp4")]
        [EnumMember(Value = "video/mp4")]
        MP4,
        [Description("m4a")]
        [EnumMember(Value = "audio/mp4")]
        M4A,
        [Description("mp3")]
        [EnumMember(Value = "audio/mpeg")]
        MP3,
        [Description("wav")]
        [EnumMember(Value = "audio/x-wav")]
        WAV,
        [Description("unknown")]
        [EnumMember(Value = "application/octet-stream")]
        UNKNOWN
    }
}

