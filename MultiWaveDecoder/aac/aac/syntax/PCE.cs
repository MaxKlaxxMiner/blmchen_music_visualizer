// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable AssignmentInConditionalExpression
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable 169
namespace MultiWaveDecoder
{
  public sealed class PCE : Element
  {
    const int MAX_FRONT_CHANNEL_ELEMENTS = 16;
    const int MAX_SIDE_CHANNEL_ELEMENTS = 16;
    const int MAX_BACK_CHANNEL_ELEMENTS = 16;
    const int MAX_LFE_CHANNEL_ELEMENTS = 4;
    const int MAX_ASSOC_DATA_ELEMENTS = 8;
    const int MAX_VALID_CC_ELEMENTS = 16;

    public sealed class TaggedElement
    {
      readonly bool isCPE;
      readonly int tag;

      public TaggedElement(bool isCPE, int tag)
      {
        this.isCPE = isCPE;
        this.tag = tag;
      }

      public bool isIsCPE()
      {
        return isCPE;
      }

      public int getTag()
      {
        return tag;
      }
    }

    public sealed class CCE
    {
      readonly bool isIndSW;
      readonly int tag;

      public CCE(bool isIndSW, int tag)
      {
        this.isIndSW = isIndSW;
        this.tag = tag;
      }

      public bool isIsIndSW()
      {
        return isIndSW;
      }

      public int getTag()
      {
        return tag;
      }
    }

    Profile profile;
    SampleFrequency sampleFrequency = SampleFrequency.forInt(-1);
    int frontChannelElementsCount, sideChannelElementsCount, backChannelElementsCount;
    int lfeChannelElementsCount, assocDataElementsCount;
    int validCCElementsCount;
    bool monoMixdown, stereoMixdown, matrixMixdownIDXPresent;
    int monoMixdownElementNumber, stereoMixdownElementNumber, matrixMixdownIDX;
    bool pseudoSurround;
    readonly TaggedElement[] frontElements = new TaggedElement[MAX_FRONT_CHANNEL_ELEMENTS];
    readonly TaggedElement[] sideElements = new TaggedElement[MAX_SIDE_CHANNEL_ELEMENTS];
    readonly TaggedElement[] backElements = new TaggedElement[MAX_BACK_CHANNEL_ELEMENTS];
    readonly int[] lfeElementTags = new int[MAX_LFE_CHANNEL_ELEMENTS];
    readonly int[] assocDataElementTags = new int[MAX_ASSOC_DATA_ELEMENTS];
    readonly CCE[] ccElements = new CCE[MAX_VALID_CC_ELEMENTS];
    byte[] commentFieldData;

    public void decode(BitStream inStream)
    {
      readElementInstanceTag(inStream);

      profile = new Profile(inStream.readBits(2));

      sampleFrequency = SampleFrequency.forInt(inStream.readBits(4));

      frontChannelElementsCount = inStream.readBits(4);
      sideChannelElementsCount = inStream.readBits(4);
      backChannelElementsCount = inStream.readBits(4);
      lfeChannelElementsCount = inStream.readBits(2);
      assocDataElementsCount = inStream.readBits(3);
      validCCElementsCount = inStream.readBits(4);

      if (monoMixdown = inStream.readBool())
      {
        Logger.LogServe("mono mixdown present, but not yet supported");
        monoMixdownElementNumber = inStream.readBits(4);
      }
      if (stereoMixdown = inStream.readBool())
      {
        Logger.LogServe("stereo mixdown present, but not yet supported");
        stereoMixdownElementNumber = inStream.readBits(4);
      }
      if (matrixMixdownIDXPresent = inStream.readBool())
      {
        Logger.LogServe("matrix mixdown present, but not yet supported");
        matrixMixdownIDX = inStream.readBits(2);
        pseudoSurround = inStream.readBool();
      }

      readTaggedElementArray(frontElements, inStream, frontChannelElementsCount);

      readTaggedElementArray(sideElements, inStream, sideChannelElementsCount);

      readTaggedElementArray(backElements, inStream, backChannelElementsCount);

      for (int i = 0; i < lfeChannelElementsCount; i++)
      {
        lfeElementTags[i] = inStream.readBits(4);
      }

      for (int i = 0; i < assocDataElementsCount; i++)
      {
        assocDataElementTags[i] = inStream.readBits(4);
      }

      for (int i = 0; i < validCCElementsCount; i++)
      {
        ccElements[i] = new CCE(inStream.readBool(), inStream.readBits(4));
      }

      inStream.byteAlign();

      int commentFieldBytes = inStream.readBits(8);
      commentFieldData = new byte[commentFieldBytes];
      for (int i = 0; i < commentFieldBytes; i++)
      {
        commentFieldData[i] = (byte)inStream.readBits(8);
      }
    }

    static void readTaggedElementArray(TaggedElement[] te, BitStream inStream, int len)
    {
      for (int i = 0; i < len; ++i)
      {
        te[i] = new TaggedElement(inStream.readBool(), inStream.readBits(4));
      }
    }

    public Profile getProfile()
    {
      return profile;
    }

    public SampleFrequency getSampleFrequency()
    {
      return sampleFrequency;
    }

    public int getChannelCount()
    {
      return frontChannelElementsCount + sideChannelElementsCount + backChannelElementsCount + lfeChannelElementsCount + assocDataElementsCount;
    }
  }
}
