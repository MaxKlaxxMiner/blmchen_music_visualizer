﻿using System;

namespace MultiWaveDecoder
{
  public sealed class ITunesProtection : Protection
  {
    //private string userID, userName, userKey;
    //private byte[] privateKey, initializationVector;

    public ITunesProtection(IBox sinf)
      : base(sinf)
    {
      throw new NotImplementedException();

      //  Box schi = sinf.getChild(BoxType.SCHEME_INFORMATION_BOX);
      //  userID = new string(((FairPlayDataBox) schi.getChild(BoxType.FAIRPLAY_USER_ID_BOX)).getData());

      //  //user name box is filled with 0
      //  byte[] b = ((FairPlayDataBox) schi.getChild(BoxType.FAIRPLAY_USER_NAME_BOX)).getData();
      //  int i = 0;
      //  while(b[i]!=0) {
      //    i++;
      //  }
      //  userName = new string(b, 0, i-1);

      //  userKey = new string(((FairPlayDataBox) schi.getChild(BoxType.FAIRPLAY_USER_KEY_BOX)).getData());
      //  privateKey = ((FairPlayDataBox) schi.getChild(BoxType.FAIRPLAY_PRIVATE_KEY_BOX)).getData();
      //  initializationVector = ((FairPlayDataBox) schi.getChild(BoxType.FAIRPLAY_IV_BOX)).getData();
    }

    public override Scheme getScheme()
    {
      return Scheme.ITUNES_FAIR_PLAY;
    }

    //public string getUserID() {
    //  return userID;
    //}

    //public string getUserName() {
    //  return userName;
    //}

    //public string getUserKey() {
    //  return userKey;
    //}

    //public byte[] getPrivateKey() {
    //  return privateKey;
    //}

    //public byte[] getInitializationVector() {
    //  return initializationVector;
    //}
  }
}
