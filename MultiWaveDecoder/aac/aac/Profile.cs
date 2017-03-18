// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
namespace MultiWaveDecoder
{
  /// <summary>
  /// Different AAC profiles.
  /// The function <code>Decoder.canDecode</code> specifies if the decoder can handle a given format.
  /// More precisely, the ISO standard calls these 'object types'.
  /// </summary>
  public sealed class Profile
  {
    public enum ProfileType
    {
      /// <summary>
      /// unknown
      /// </summary>
      UNKNOWN = 0,
      /// <summary>
      /// AAC Main Profile
      /// </summary>
      AAC_MAIN = 1,
      /// <summary>
      /// AAC Low Complexity
      /// </summary>
      AAC_LC = 2,
      /// <summary>
      /// AAC Scalable Sample Rate
      /// </summary>
      AAC_SSR = 3,
      /// <summary>
      /// AAC Long Term Prediction
      /// </summary>
      AAC_LTP = 4,
      /// <summary>
      /// AAC SBR
      /// </summary>
      AAC_SBR = 5,
      /// <summary>
      /// Scalable AAC
      /// </summary>
      AAC_SCALABLE = 6,
      /// <summary>
      /// TwinVQ
      /// </summary>
      TWIN_VQ = 7,
      /// <summary>
      /// AAC Low Delay
      /// </summary>
      AAC_LD = 11,
      /// <summary>
      /// Error Resilient AAC Low Complexity
      /// </summary>
      ER_AAC_LC = 17,
      /// <summary>
      /// Error Resilient AAC SSR
      /// </summary>
      ER_AAC_SSR = 18,
      /// <summary>
      /// Error Resilient AAC Long Term Prediction
      /// </summary>
      ER_AAC_LTP = 19,
      /// <summary>
      /// Error Resilient Scalable AAC
      /// </summary>
      ER_AAC_SCALABLE = 20,
      /// <summary>
      /// Error Resilient TwinVQ
      /// </summary>
      ER_TWIN_VQ = 21,
      /// <summary>
      /// Error Resilient BSAC
      /// </summary>
      ER_BSAC = 22,
      /// <summary>
      /// Error Resilient AAC Low Delay
      /// </summary>
      ER_AAC_LD = 23
    }

    public readonly ProfileType type;
    public readonly bool supported;

    public Profile(int code = 1)
    {
      type = code > 0 && code < ALL.Length ? ALL[code] : ProfileType.UNKNOWN;
      switch (type)
      {
        case ProfileType.AAC_MAIN:
        case ProfileType.AAC_LC:
        case ProfileType.AAC_LTP:
        case ProfileType.AAC_SBR:
        case ProfileType.AAC_SSR:
        case ProfileType.ER_AAC_LC:
        case ProfileType.ER_AAC_LTP:
        case ProfileType.ER_AAC_LD: supported = true; break;
        default: supported = false; break;
      }
    }

    static readonly ProfileType[] ALL =
    {
      ProfileType.AAC_MAIN, ProfileType.AAC_LC, ProfileType.AAC_SSR, ProfileType.AAC_LTP, ProfileType.AAC_SBR, ProfileType.AAC_SCALABLE, ProfileType.TWIN_VQ, ProfileType.UNKNOWN,
      ProfileType.UNKNOWN, ProfileType.UNKNOWN, ProfileType.AAC_LD, ProfileType.UNKNOWN, ProfileType.UNKNOWN, ProfileType.UNKNOWN, ProfileType.UNKNOWN, ProfileType.UNKNOWN,
      ProfileType.ER_AAC_LC, ProfileType.ER_AAC_SSR, ProfileType.ER_AAC_LTP, ProfileType.ER_AAC_SCALABLE, ProfileType.ER_TWIN_VQ, ProfileType.ER_BSAC, ProfileType.ER_AAC_LD
    };

    /// <summary>
    /// Returns a bool, indicating if this profile contains error resilient tools. That is, if it's index is higher than 16, since the first error resilient profile is ER_AAC_LC (17).
    /// This method is mainly used internally.
    /// </summary>
    /// <returns>true if the profile uses error resilience</returns>
    public bool isErrorResilientProfile()
    {
      return type >= ProfileType.ER_AAC_LC;
    }

    public override string ToString()
    {
      return (new { type, supported }).ToString();
    }
  }
}
