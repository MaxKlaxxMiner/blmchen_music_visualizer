﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Local

using System;

#pragma warning disable 169
#pragma warning disable 414
namespace MultiWaveDecoder
{
  public sealed class SyntacticElements : Constants
  {
    // global properties
    DecoderConfig config;
    bool sbrPresent, psPresent;
    int bitsRead;
    // elements
    PCE pce;
    Element[] elements; //SCE, LFE and CPE
    CCE[] cces;
    DSE[] dses;
    FIL[] fils;
    int curElem, curCCE, curDSE, curFIL;
    float[,] data;

    public SyntacticElements(DecoderConfig config)
    {
      this.config = config;

      pce = new PCE();
      elements = new Element[4 * MAX_ELEMENTS];
      cces = new CCE[MAX_ELEMENTS];
      dses = new DSE[MAX_ELEMENTS];
      fils = new FIL[MAX_ELEMENTS];

      startNewFrame();
    }

    public void startNewFrame()
    {
      curElem = 0;
      curCCE = 0;
      curDSE = 0;
      curFIL = 0;
      sbrPresent = false;
      psPresent = false;
      bitsRead = 0;
    }

    public void decode(BitStream inStream)
    {
      int start = inStream.getPosition(); // should be 0

      bool content = true;
      if (!config.getProfile().isErrorResilientProfile())
      {
        Element prev = null;
        int type;
        while (content && (type = inStream.readBits(3)) != ELEMENT_END)
        {
          switch (type)
          {
            case ELEMENT_SCE:
            case ELEMENT_LFE:
            {
              Logger.LogInfo("SCE");
              throw new NotImplementedException();
              //prev = decodeSCE_LFE(inStream);
            } break;
            case ELEMENT_CPE:
            {
              Logger.LogInfo("CPE");
              prev = decodeCPE(inStream);
            } break;
            case ELEMENT_CCE:
            {
              Logger.LogInfo("CCE");
              throw new NotImplementedException();
              //decodeCCE(inStream);
              prev = null;
            } break;
            case ELEMENT_DSE:
            {
              Logger.LogInfo("DSE");
              throw new NotImplementedException();
              //decodeDSE(inStream);
              prev = null;
            } break;
            case ELEMENT_PCE:
            {
              Logger.LogInfo("PCE");
              throw new NotImplementedException();
              //decodePCE(inStream);
              prev = null;
            } break;
            case ELEMENT_FIL:
            {
              Logger.LogInfo("FIL");
              throw new NotImplementedException();
              //decodeFIL(inStream, prev);
              prev = null;
            } break;
          }
        }
        Logger.LogInfo("END");
        content = false;
        prev = null;
      }
      else
      {
        // error resilient raw data block
        throw new NotImplementedException();
        //switch (config.getChannelConfiguration())
        //{
        //  case CHANNEL_CONFIG_MONO: decodeSCE_LFE(inStream); break;
        //  case CHANNEL_CONFIG_STEREO: decodeCPE(inStream); break;
        //  case CHANNEL_CONFIG_STEREO_PLUS_CENTER: decodeSCE_LFE(inStream); decodeCPE(inStream); break;
        //  case CHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO: decodeSCE_LFE(inStream); decodeCPE(inStream); decodeSCE_LFE(inStream); break;
        //  case CHANNEL_CONFIG_FIVE: decodeSCE_LFE(inStream); decodeCPE(inStream); decodeCPE(inStream); break;
        //  case CHANNEL_CONFIG_FIVE_PLUS_ONE: decodeSCE_LFE(inStream); decodeCPE(inStream); decodeCPE(inStream); decodeSCE_LFE(inStream); break;
        //  case CHANNEL_CONFIG_SEVEN_PLUS_ONE: decodeSCE_LFE(inStream); decodeCPE(inStream); decodeCPE(inStream); decodeCPE(inStream); decodeSCE_LFE(inStream); break;
        //  default: throw new AACException("unsupported channel configuration for error resilience: " + config.getChannelConfiguration());
        //}
      }
      inStream.byteAlign();

      bitsRead = inStream.getPosition() - start;
    }

    //private Element decodeSCE_LFE(BitStream in) throws AACException {
    //if(elements[curElem]==null) elements[curElem] = new SCE_LFE(config.getFrameLength());
    //((SCE_LFE) elements[curElem]).decode(in, config);
    //curElem++;
    //return elements[curElem-1];
    //}

    Element decodeCPE(BitStream inStream)
    {
      if (elements[curElem] == null) elements[curElem] = new CPE(config.getFrameLength());
      ((CPE)elements[curElem]).decode(inStream, config);
      curElem++;
      return elements[curElem - 1];
    }

    //private void decodeCCE(BitStream in) throws AACException {
    //if(curCCE==MAX_ELEMENTS) throw new AACException("too much CCE elements");
    //if(cces[curCCE]==null) cces[curCCE] = new CCE(config.getFrameLength());
    //cces[curCCE].decode(in, config);
    //curCCE++;
    //}

    //private void decodeDSE(BitStream in) throws AACException {
    //if(curDSE==MAX_ELEMENTS) throw new AACException("too much CCE elements");
    //if(dses[curDSE]==null) dses[curDSE] = new DSE();
    //dses[curDSE].decode(in);
    //curDSE++;
    //}

    //private void decodePCE(BitStream in) throws AACException {
    //pce.decode(in);
    //config.setProfile(pce.getProfile());
    //config.setSampleFrequency(pce.getSampleFrequency());
    //config.setChannelConfiguration(ChannelConfiguration.forInt(pce.getChannelCount()));
    //}

    //private void decodeFIL(BitStream in, Element prev) throws AACException {
    //if(curFIL==MAX_ELEMENTS) throw new AACException("too much FIL elements");
    //if(fils[curFIL]==null) fils[curFIL] = new FIL(config.isSBRDownSampled());
    //fils[curFIL].decode(in, prev, config.getSampleFrequency(), config.isSBREnabled(), config.isSmallFrameUsed());
    //curFIL++;

    //if(prev!=null&&prev.isSBRPresent()) {
    //sbrPresent = true;
    //if(!psPresent&&prev.getSBR().isPSUsed()) psPresent = true;
    //}
    //}

    //public void process(FilterBank filterBank) throws AACException {
    //Profile profile = config.getProfile();
    //SampleFrequency sf = config.getSampleFrequency();
    ////ChannelConfiguration channels = config.getChannelConfiguration();

    //int chs = config.getChannelConfiguration().getChannelCount();
    //if(chs==1&&psPresent) chs++;
    //int mult = sbrPresent ? 2 : 1;
    ////only reallocate if needed
    //if(data==null||chs!=data.Length||(mult*config.getFrameLength())!=data[0].Length) data = new float[chs,mult*config.getFrameLength()];

    //int channel = 0;
    //Element e;
    //SCE_LFE scelfe;
    //CPE cpe;
    //for(int i = 0; i<elements.Length&&channel<chs; i++) {
    //e = elements[i];
    //if(e==null) continue;
    //if(e instanceof SCE_LFE) {
    //scelfe = (SCE_LFE) e;
    //channel += processSingle(scelfe, filterBank, channel, profile, sf);
    //}
    //else if(e instanceof CPE) {
    //cpe = (CPE) e;
    //processPair(cpe, filterBank, channel, profile, sf);
    //channel += 2;
    //}
    //else if(e instanceof CCE) {
    ////applies invquant and save the result in the CCE
    //((CCE) e).process();
    //channel++;
    //}
    //}
    //}

    //private int processSingle(SCE_LFE scelfe, FilterBank filterBank, int channel, Profile profile, SampleFrequency sf) throws AACException {
    //ICStream ics = scelfe.getICStream();
    //ICSInfo info = ics.getInfo();
    //LTPrediction ltp = info.getLTPrediction1();
    //int elementID = scelfe.getElementInstanceTag();

    ////inverse quantization
    //float[] iqData = ics.getInvQuantData();

    ////prediction
    //if(profile.equals(Profile.AAC_MAIN)&&info.isICPredictionPresent()) info.getICPrediction().process(ics, iqData, sf);
    //if(LTPrediction.isLTPProfile(profile)&&info.isLTPrediction1Present()) ltp.process(ics, iqData, filterBank, sf);

    ////dependent coupling
    //processDependentCoupling(false, elementID, CCE.BEFORE_TNS, iqData, null);

    ////TNS
    //if(ics.isTNSDataPresent()) ics.getTNS().process(ics, iqData, sf, false);

    ////dependent coupling
    //processDependentCoupling(false, elementID, CCE.AFTER_TNS, iqData, null);

    ////filterbank
    //filterBank.process(info.getWindowSequence(), info.getWindowShape(ICSInfo.CURRENT), info.getWindowShape(ICSInfo.PREVIOUS), iqData, data[channel], channel);

    //if(LTPrediction.isLTPProfile(profile)) ltp.updateState(data[channel], filterBank.getOverlap(channel), profile);

    ////dependent coupling
    //processIndependentCoupling(false, elementID, data[channel], null);

    ////gain control
    //if(ics.isGainControlPresent()) ics.getGainControl().process(iqData, info.getWindowShape(ICSInfo.CURRENT), info.getWindowShape(ICSInfo.PREVIOUS), info.getWindowSequence());

    ////SBR
    //int chs = 1;
    //if(sbrPresent&&config.isSBREnabled()) {
    //if(data[channel].Length==config.getFrameLength()) LOGGER.log(Level.WARNING, "SBR data present, but buffer has normal size!");
    //SBR sbr = scelfe.getSBR();
    //if(sbr.isPSUsed()) {
    //chs = 2;
    //scelfe.getSBR().process(data[channel], data[channel+1], false);
    //}
    //else scelfe.getSBR().process(data[channel], false);
    //}
    //return chs;
    //}

    //private void processPair(CPE cpe, FilterBank filterBank, int channel, Profile profile, SampleFrequency sf) throws AACException {
    //ICStream ics1 = cpe.getLeftChannel();
    //ICStream ics2 = cpe.getRightChannel();
    //ICSInfo info1 = ics1.getInfo();
    //ICSInfo info2 = ics2.getInfo();
    //LTPrediction ltp1 = info1.getLTPrediction1();
    //LTPrediction ltp2 = cpe.isCommonWindow() ? info1.getLTPrediction2() : info2.getLTPrediction1();
    //int elementID = cpe.getElementInstanceTag();

    ////inverse quantization
    //float[] iqData1 = ics1.getInvQuantData();
    //float[] iqData2 = ics2.getInvQuantData();

    ////MS
    //if(cpe.isCommonWindow()&&cpe.isMSMaskPresent()) MS.process(cpe, iqData1, iqData2);
    ////main prediction
    //if(profile.equals(Profile.AAC_MAIN)) {
    //if(info1.isICPredictionPresent()) info1.getICPrediction().process(ics1, iqData1, sf);
    //if(info2.isICPredictionPresent()) info2.getICPrediction().process(ics2, iqData2, sf);
    //}
    ////IS
    //IS.process(cpe, iqData1, iqData2);

    ////LTP
    //if(LTPrediction.isLTPProfile(profile)) {
    //if(info1.isLTPrediction1Present()) ltp1.process(ics1, iqData1, filterBank, sf);
    //if(cpe.isCommonWindow()&&info1.isLTPrediction2Present()) ltp2.process(ics2, iqData2, filterBank, sf);
    //else if(info2.isLTPrediction1Present()) ltp2.process(ics2, iqData2, filterBank, sf);
    //}

    ////dependent coupling
    //processDependentCoupling(true, elementID, CCE.BEFORE_TNS, iqData1, iqData2);

    ////TNS
    //if(ics1.isTNSDataPresent()) ics1.getTNS().process(ics1, iqData1, sf, false);
    //if(ics2.isTNSDataPresent()) ics2.getTNS().process(ics2, iqData2, sf, false);

    ////dependent coupling
    //processDependentCoupling(true, elementID, CCE.AFTER_TNS, iqData1, iqData2);

    ////filterbank
    //filterBank.process(info1.getWindowSequence(), info1.getWindowShape(ICSInfo.CURRENT), info1.getWindowShape(ICSInfo.PREVIOUS), iqData1, data[channel], channel);
    //filterBank.process(info2.getWindowSequence(), info2.getWindowShape(ICSInfo.CURRENT), info2.getWindowShape(ICSInfo.PREVIOUS), iqData2, data[channel+1], channel+1);

    //if(LTPrediction.isLTPProfile(profile)) {
    //ltp1.updateState(data[channel], filterBank.getOverlap(channel), profile);
    //ltp2.updateState(data[channel+1], filterBank.getOverlap(channel+1), profile);
    //}

    ////independent coupling
    //processIndependentCoupling(true, elementID, data[channel], data[channel+1]);

    ////gain control
    //if(ics1.isGainControlPresent()) ics1.getGainControl().process(iqData1, info1.getWindowShape(ICSInfo.CURRENT), info1.getWindowShape(ICSInfo.PREVIOUS), info1.getWindowSequence());
    //if(ics2.isGainControlPresent()) ics2.getGainControl().process(iqData2, info2.getWindowShape(ICSInfo.CURRENT), info2.getWindowShape(ICSInfo.PREVIOUS), info2.getWindowSequence());

    ////SBR
    //if(sbrPresent&&config.isSBREnabled()) {
    //if(data[channel].Length==config.getFrameLength()) LOGGER.log(Level.WARNING, "SBR data present, but buffer has normal size!");
    //cpe.getSBR().process(data[channel], data[channel+1], false);
    //}
    //}

    //private void processIndependentCoupling(bool channelPair, int elementID, float[] data1, float[] data2) {
    //int index, c, chSelect;
    //CCE cce;
    //for(int i = 0; i<cces.Length; i++) {
    //cce = cces[i];
    //index = 0;
    //if(cce!=null&&cce.getCouplingPoint()==CCE.AFTER_IMDCT) {
    //for(c = 0; c<=cce.getCoupledCount(); c++) {
    //chSelect = cce.getCHSelect(c);
    //if(cce.isChannelPair(c)==channelPair&&cce.getIDSelect(c)==elementID) {
    //if(chSelect!=1) {
    //cce.applyIndependentCoupling(index, data1);
    //if(chSelect!=0) index++;
    //}
    //if(chSelect!=2) {
    //cce.applyIndependentCoupling(index, data2);
    //index++;
    //}
    //}
    //else index += 1+((chSelect==3) ? 1 : 0);
    //}
    //}
    //}
    //}

    //private void processDependentCoupling(bool channelPair, int elementID, int couplingPoint, float[] data1, float[] data2) {
    //int index, c, chSelect;
    //CCE cce;
    //for(int i = 0; i<cces.Length; i++) {
    //cce = cces[i];
    //index = 0;
    //if(cce!=null&&cce.getCouplingPoint()==couplingPoint) {
    //for(c = 0; c<=cce.getCoupledCount(); c++) {
    //chSelect = cce.getCHSelect(c);
    //if(cce.isChannelPair(c)==channelPair&&cce.getIDSelect(c)==elementID) {
    //if(chSelect!=1) {
    //cce.applyDependentCoupling(index, data1);
    //if(chSelect!=0) index++;
    //}
    //if(chSelect!=2) {
    //cce.applyDependentCoupling(index, data2);
    //index++;
    //}
    //}
    //else index += 1+((chSelect==3) ? 1 : 0);
    //}
    //}
    //}
    //}

    //public void sendToOutput(SampleBuffer buffer) {
    //bool be = buffer.isBigEndian();

    //int chs = data.Length;
    //int mult = (sbrPresent&&config.isSBREnabled()) ? 2 : 1;
    //int length = mult*config.getFrameLength();
    //int freq = mult*config.getSampleFrequency().getFrequency();

    //byte[] b = buffer.getData();
    //if(b.Length!=chs*length*2) b = new byte[chs*length*2];

    //float[] cur;
    //int i, j, off;
    //short s;
    //for(i = 0; i<chs; i++) {
    //cur = data[i];
    //for(j = 0; j<length; j++) {
    //s = (short) Math.max(Math.min(Math.round(cur[j]), Short.MAX_VALUE), Short.MIN_VALUE);
    //off = (j*chs+i)*2;
    //if(be) {
    //b[off] = (byte) ((s>>8)&BYTE_MASK);
    //b[off+1] = (byte) (s&BYTE_MASK);
    //}
    //else {
    //b[off+1] = (byte) ((s>>8)&BYTE_MASK);
    //b[off] = (byte) (s&BYTE_MASK);
    //}
    //}
    //}

    //buffer.setData(b, freq, chs, 16, bitsRead);
    //}
    //}
  }
}
