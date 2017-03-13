package net.sourceforge.jaad.mp4.api;

import java.io.DataInputStream;
import java.io.IOException;
import java.nio.charset.Charset;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

class ID3Frame {

	static int ALBUM_TITLE = 1413565506; //TALB
	static int ALBUM_SORT_ORDER = 1414745921; //TSOA
	static int ARTIST = 1414546737; //TPE1
	static int ATTACHED_PICTURE = 1095780675; //APIC
	static int AUDIO_ENCRYPTION = 1095061059; //AENC
	static int AUDIO_SEEK_POINT_INDEX = 1095979081; //ASPI
	static int BAND = 1414546738; //TPE2
	static int BEATS_PER_MINUTE = 1413632077; //TBPM
	static int COMMENTS = 1129270605; //COMM
	static int COMMERCIAL_FRAME = 1129270610; //COMR
	static int COMMERCIAL_INFORMATION = 1464029005; //WCOM
	static int COMPOSER = 1413697357; //TCOM
	static int CONDUCTOR = 1414546739; //TPE3
	static int CONTENT_GROUP_DESCRIPTION = 1414091825; //TIT1
	static int CONTENT_TYPE = 1413697358; //TCON
	static int COPYRIGHT = 1464029008; //WCOP
	static int COPYRIGHT_MESSAGE = 1413697360; //TCOP
	static int ENCODED_BY = 1413828163; //TENC
	static int ENCODING_TIME = 1413760334; //TDEN
	static int ENCRYPTION_METHOD_REGISTRATION = 1162756946; //ENCR
	static int EQUALISATION = 1162958130; //EQU2
	static int EVENT_TIMING_CODES = 1163150159; //ETCO
	static int FILE_OWNER = 1414485838; //TOWN
	static int FILE_TYPE = 1413893204; //TFLT
	static int GENERAL_ENCAPSULATED_OBJECT = 1195724610; //GEOB
	static int GROUP_IDENTIFICATION_REGISTRATION = 1196575044; //GRID
	static int INITIAL_KEY = 1414219097; //TKEY
	static int INTERNET_RADIO_STATION_NAME = 1414681422; //TRSN
	static int INTERNET_RADIO_STATION_OWNER = 1414681423; //TRSO
	static int MODIFIED_BY = 1414546740; //TPE4
	static int INVOLVED_PEOPLE_LIST = 1414090828; //TIPL
	static int INTERNATIONAL_STANDARD_RECORDING_CODE = 1414746691; //TSRC
	static int LANGUAGES = 1414283598; //TLAN
	static int LENGTH = 1414284622; //TLEN
	static int LINKED_INFORMATION = 1279872587; //LINK
	static int LYRICIST = 1413830740; //TEXT
	static int MEDIA_TYPE = 1414350148; //TMED
	static int MOOD = 1414352719; //TMOO
	static int MPEG_LOCATION_LOOKUP_TABLE = 1296845908; //MLLT
	static int MUSICIAN_CREDITS_LIST = 1414349644; //TMCL
	static int MUSIC_CD_IDENTIFIER = 1296254025; //MCDI
	static int OFFICIAL_ARTIST_WEBPAGE = 1464811858; //WOAR
	static int OFFICIAL_AUDIO_FILE_WEBPAGE = 1464811846; //WOAF
	static int OFFICIAL_AUDIO_SOURCE_WEBPAGE = 1464811859; //WOAS
	static int OFFICIAL_INTERNET_RADIO_STATION_HOMEPAGE = 1464816211; //WORS
	static int ORIGINAL_ALBUM_TITLE = 1414480204; //TOAL
	static int ORIGINAL_ARTIST = 1414484037; //TOPE
	static int ORIGINAL_FILENAME = 1414481486; //TOFN
	static int ORIGINAL_LYRICIST = 1414483033; //TOLY
	static int ORIGINAL_RELEASE_TIME = 1413762898; //TDOR
	static int OWNERSHIP_FRAME = 1331121733; //OWNE
	static int PART_OF_A_SET = 1414549331; //TPOS
	static int PAYMENT = 1464877401; //WPAY
	static int PERFORMER_SORT_ORDER = 1414745936; //TSOP
	static int PLAYLIST_DELAY = 1413762137; //TDLY
	static int PLAY_COUNTER = 1346588244; //PCNT
	static int POPULARIMETER = 1347375181; //POPM
	static int POSITION_SYNCHRONISATION_FRAME = 1347375955; //POSS
	static int PRIVATE_FRAME = 1347570006; //PRIV
	static int PRODUCED_NOTICE = 1414550095; //TPRO
	static int PUBLISHER = 1414550850; //TPUB
	static int PUBLISHERS_OFFICIAL_WEBPAGE = 1464882498; //WPUB
	static int RECOMMENDED_BUFFER_SIZE = 1380078918; //RBUF
	static int RECORDING_TIME = 1413763651; //TDRC
	static int RELATIVE_VOLUME_ADJUSTMENT = 1381384498; //RVA2
	static int RELEASE_TIME = 1413763660; //TDRL
	static int REVERB = 1381388866; //RVRB
	static int SEEK_FRAME = 1397048651; //SEEK
	static int SET_SUBTITLE = 1414746964; //TSST
	static int SIGNATURE_FRAME = 1397311310; //SIGN
	static int ENCODING_TOOLS_AND_SETTINGS = 1414746949; //TSSE
	static int SUBTITLE = 1414091827; //TIT3
	static int SYNCHRONISED_LYRIC = 1398361172; //SYLT
	static int SYNCHRONISED_TEMPO_CODES = 1398363203; //SYTC
	static int TAGGING_TIME = 1413764167; //TDTG
	static int TERMS_OF_USE = 1431520594; //USER
	static int TITLE = 1414091826; //TIT2
	static int TITLE_SORT_ORDER = 1414745940; //TSOT
	static int TRACK_NUMBER = 1414677323; //TRCK
	static int UNIQUE_FILE_IDENTIFIER = 1430669636; //UFID
	static int UNSYNCHRONISED_LYRIC = 1431522388; //USLT
	static int USER_DEFINED_TEXT_INFORMATION_FRAME = 1415075928; //TXXX
	static int USER_DEFINED_URL_LINK_FRAME = 1465407576; //WXXX
	private static string[] TEXT_ENCODINGS = {"ISO-8859-1", "UTF-16"/*BOM*/, "UTF-16", "UTF-8"};
	private static string[] VALID_TIMESTAMPS = {"yyyy, yyyy-MM", "yyyy-MM-dd", "yyyy-MM-ddTHH", "yyyy-MM-ddTHH:mm", "yyyy-MM-ddTHH:mm:ss"};
	private static string UNKNOWN_LANGUAGE = "xxx";
	private long size;
	private int id, flags, groupID, encryptionMethod;
	private byte[] data;

	ID3Frame(DataInputStream in) throws IOException {
		id = in.readInt();
		size = ID3Tag.readSynch(in);
		flags = in.readShort();

		if(isInGroup()) groupID = in.read();
		if(isEncrypted()) encryptionMethod = in.read();
		//TODO: data length indicator, unsync

		data = new byte[(int) size];
		in.readFully(data);
	}

	//header data
	public int getID() {
		return id;
	}

	public long getSize() {
		return size;
	}

	public bool isInGroup() {
		return (flags&0x40)==0x40;
	}

	public int getGroupID() {
		return groupID;
	}

	public bool isCompressed() {
		return (flags&8)==8;
	}

	public bool isEncrypted() {
		return (flags&4)==4;
	}

	public int getEncryptionMethod() {
		return encryptionMethod;
	}

	//content data
	public byte[] getData() {
		return data;
	}

	public string getText() {
		return new string(data, Charset.forName(TEXT_ENCODINGS[0]));
	}

	public string getEncodedText() {
		//first byte indicates encoding
		int enc = data[0];

		//charsets 0,3 end with '0'; 1,2 end with '00'
		int t = -1;
		for(int i = 1; i<data.length&&t<0; i++) {
			if(data[i]==0&&(enc==0||enc==3||data[i+1]==0)) t = i;
		}
		return new string(data, 1, t-1, Charset.forName(TEXT_ENCODINGS[enc]));
	}

	public int getNumber() {
		return Integer.parseInt(new string(data));
	}

	public int[] getNumbers() {
		//multiple numbers separated by '/'
		string x = new string(data, Charset.forName(TEXT_ENCODINGS[0]));
		int i = x.indexOf('/');
		int[] y;
		if(i>0) y = new int[]{Integer.parseInt(x.substring(0, i)), Integer.parseInt(x.substring(i+1))};
		else y = new int[]{Integer.parseInt(x)};
		return y;
	}

	public Date getDate() {
		//timestamp lengths: 4,7,10,13,16,19
		int i = (int) Math.floor(data.length/3)-1;
		Date date;
		if(i>=0&&i<VALID_TIMESTAMPS.length) {
			SimpleDateFormat sdf = new SimpleDateFormat(VALID_TIMESTAMPS[i]);
			date = sdf.parse(new string(data), new ParsePosition(0));
		}
		else date = null;
		return date;
	}

	public Locale getLocale() {
		string s = new string(data).toLowerCase();
		Locale l;
		if(s.equals(UNKNOWN_LANGUAGE)) l = null;
		else l = new Locale(s);
		return l;
	}
}
