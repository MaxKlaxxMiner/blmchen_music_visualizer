﻿
using System;
using System.Linq;

namespace MultiWaveDecoder
{
  public sealed class M4ADemuxer
  {
    M4AStream stream;

    public M4ADemuxer(M4AStream stream)
    {
      if (!Probe(stream)) throw new Exception("not a m4a-file!");

      this.stream = stream;
    }

    //  var Demuxer, M4ADemuxer,
    //    extend = function(child, parent) { for (var key in parent) { if (hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; },
    //    hasProp = {}.hasOwnProperty,
    //    indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

    //  Demuxer = require("../demuxer");

    //  M4ADemuxer = (function(superClass) {
    //    var BITS_PER_CHANNEL, TYPES, after, atom, atoms, bool, containers, diskTrack, genres, meta, string;

    //    extend(M4ADemuxer, superClass);

    //    function M4ADemuxer() {
    //      return M4ADemuxer.__super__.constructor.apply(this, arguments);
    //    }

    //    Demuxer.register(M4ADemuxer);

    static readonly string[] Types = { "M4A ", "M4P ", "M4B ", "M4V ", "isom", "mp42", "qt  " };

    public static bool Probe(M4AStream buffer)
    {
      return buffer.PeekString(4, 4) == "ftyp" && Types.Contains(buffer.PeekString(8, 4));
    }

    //    M4ADemuxer.prototype.init = function() {
    //      this.atoms = [];
    //      this.offsets = [];
    //      this.track = null;
    //      return this.tracks = [];
    //    };

    //    atoms = {};

    //    containers = {};

    //    atom = function(name, fn) {
    //      var c, container, k, len1, ref;
    //      c = [];
    //      ref = name.split(".").slice(0, -1);
    //      for (k = 0, len1 = ref.length; k < len1; k++) {
    //        container = ref[k];
    //        c.push(container);
    //        containers[c.join(".")] = true;
    //      }
    //      if (atoms[name] == null) {
    //        atoms[name] = {};
    //      }
    //      return atoms[name].fn = fn;
    //    };

    //    after = function(name, fn) {
    //      if (atoms[name] == null) {
    //        atoms[name] = {};
    //      }
    //      return atoms[name].after = fn;
    //    };

    //    M4ADemuxer.prototype.readChunk = function() {
    //      var handler, path, type;
    //      this["break"] = false;
    //      while (this.stream.available(1) && !this["break"]) {
    //        if (!this.readHeaders) {
    //          if (!this.stream.available(8)) {
    //            return;
    //          }
    //          this.len = this.stream.readUInt32() - 8;
    //          this.type = this.stream.readString(4);
    //          if (this.len === 0) {
    //            continue;
    //          }
    //          this.atoms.push(this.type);
    //          this.offsets.push(this.stream.offset + this.len);
    //          this.readHeaders = true;
    //        }
    //        path = this.atoms.join(".");
    //        handler = atoms[path];
    //        if (handler != null ? handler.fn : void 0) {
    //          if (!(this.stream.available(this.len) || path === "mdat")) {
    //            return;
    //          }
    //          handler.fn.call(this);
    //          if (path in containers) {
    //            this.readHeaders = false;
    //          }
    //        } else if (path in containers) {
    //          this.readHeaders = false;
    //        } else {
    //          if (!this.stream.available(this.len)) {
    //            return;
    //          }
    //          this.stream.advance(this.len);
    //        }
    //        while (this.stream.offset >= this.offsets[this.offsets.length - 1]) {
    //          handler = atoms[this.atoms.join(".")];
    //          if (handler != null ? handler.after : void 0) {
    //            handler.after.call(this);
    //          }
    //          type = this.atoms.pop();
    //          this.offsets.pop();
    //          this.readHeaders = false;
    //        }
    //      }
    //    };

    //    atom("ftyp", function() {
    //      var ref;
    //      if (ref = this.stream.readString(4), indexOf.call(TYPES, ref) < 0) {
    //        return this.emit("error", "Not a valid M4A file.");
    //      }
    //      return this.stream.advance(this.len - 4);
    //    });

    //    atom("moov.trak", function() {
    //      this.track = {};
    //      return this.tracks.push(this.track);
    //    });

    //    atom("moov.trak.tkhd", function() {
    //      this.stream.advance(4);
    //      this.stream.advance(8);
    //      this.track.id = this.stream.readUInt32();
    //      return this.stream.advance(this.len - 16);
    //    });

    //    atom("moov.trak.mdia.hdlr", function() {
    //      this.stream.advance(4);
    //      this.stream.advance(4);
    //      this.track.type = this.stream.readString(4);
    //      this.stream.advance(12);
    //      return this.stream.advance(this.len - 24);
    //    });

    //    atom("moov.trak.mdia.mdhd", function() {
    //      this.stream.advance(4);
    //      this.stream.advance(8);
    //      this.track.timeScale = this.stream.readUInt32();
    //      this.track.duration = this.stream.readUInt32();
    //      return this.stream.advance(4);
    //    });

    //    BITS_PER_CHANNEL = {
    //      ulaw: 8,
    //      alaw: 8,
    //      in24: 24,
    //      in32: 32,
    //      fl32: 32,
    //      fl64: 64
    //    };

    //    atom("moov.trak.mdia.minf.stbl.stsd", function() {
    //      var format, numEntries, ref, ref1, version;
    //      this.stream.advance(4);
    //      numEntries = this.stream.readUInt32();
    //      if (this.track.type !== "soun") {
    //        return this.stream.advance(this.len - 8);
    //      }
    //      if (numEntries !== 1) {
    //        return this.emit("error", "Only expecting one entry in sample description atom!");
    //      }
    //      this.stream.advance(4);
    //      format = this.track.format = {};
    //      format.formatID = this.stream.readString(4);
    //      this.stream.advance(6);
    //      this.stream.advance(2);
    //      version = this.stream.readUInt16();
    //      this.stream.advance(6);
    //      format.channelsPerFrame = this.stream.readUInt16();
    //      format.bitsPerChannel = this.stream.readUInt16();
    //      this.stream.advance(4);
    //      format.sampleRate = this.stream.readUInt16();
    //      this.stream.advance(2);
    //      if (version === 1) {
    //        format.framesPerPacket = this.stream.readUInt32();
    //        this.stream.advance(4);
    //        format.bytesPerFrame = this.stream.readUInt32();
    //        this.stream.advance(4);
    //      } else if (version !== 0) {
    //        this.emit("error", "Unknown version in stsd atom");
    //      }
    //      if (BITS_PER_CHANNEL[format.formatID] != null) {
    //        format.bitsPerChannel = BITS_PER_CHANNEL[format.formatID];
    //      }
    //      format.floatingPoint = (ref = format.formatID) === "fl32" || ref === "fl64";
    //      format.littleEndian = format.formatID === "sowt" && format.bitsPerChannel > 8;
    //      if ((ref1 = format.formatID) === "twos" || ref1 === "sowt" || ref1 === "in24" || ref1 === "in32" || ref1 === "fl32" || ref1 === "fl64" || ref1 === "raw " || ref1 === "NONE") {
    //        return format.formatID = "lpcm";
    //      }
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stsd.alac", function() {
    //      this.stream.advance(4);
    //      return this.track.cookie = this.stream.readBuffer(this.len - 4);
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stsd.esds", function() {
    //      var offset;
    //      offset = this.stream.offset + this.len;
    //      this.track.cookie = M4ADemuxer.readEsds(this.stream);
    //      return this.stream.seek(offset);
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stsd.wave.enda", function() {
    //      return this.track.format.littleEndian = !!this.stream.readUInt16();
    //    });

    //    M4ADemuxer.readDescrLen = function(stream) {
    //      var c, count, len;
    //      len = 0;
    //      count = 4;
    //      while (count--) {
    //        c = stream.readUInt8();
    //        len = (len << 7) | (c & 0x7f);
    //        if (!(c & 0x80)) {
    //          break;
    //        }
    //      }
    //      return len;
    //    };

    //    M4ADemuxer.readEsds = function(stream) {
    //      var codec_id, flags, len, tag;
    //      stream.advance(4);
    //      tag = stream.readUInt8();
    //      len = M4ADemuxer.readDescrLen(stream);
    //      if (tag === 0x03) {
    //        stream.advance(2);
    //        flags = stream.readUInt8();
    //        if (flags & 0x80) {
    //          stream.advance(2);
    //        }
    //        if (flags & 0x40) {
    //          stream.advance(stream.readUInt8());
    //        }
    //        if (flags & 0x20) {
    //          stream.advance(2);
    //        }
    //      } else {
    //        stream.advance(2);
    //      }
    //      tag = stream.readUInt8();
    //      len = M4ADemuxer.readDescrLen(stream);
    //      if (tag === 0x04) {
    //        codec_id = stream.readUInt8();
    //        stream.advance(1);
    //        stream.advance(3);
    //        stream.advance(4);
    //        stream.advance(4);
    //        tag = stream.readUInt8();
    //        len = M4ADemuxer.readDescrLen(stream);
    //        if (tag === 0x05) {
    //          return stream.readBuffer(len);
    //        }
    //      }
    //      return null;
    //    };

    //    atom("moov.trak.mdia.minf.stbl.stts", function() {
    //      var entries, i, k, ref;
    //      this.stream.advance(4);
    //      entries = this.stream.readUInt32();
    //      this.track.stts = [];
    //      for (i = k = 0, ref = entries; k < ref; i = k += 1) {
    //        this.track.stts[i] = {
    //          count: this.stream.readUInt32(),
    //          duration: this.stream.readUInt32()
    //        };
    //      }
    //      return this.setupSeekPoints();
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stsc", function() {
    //      var entries, i, k, ref;
    //      this.stream.advance(4);
    //      entries = this.stream.readUInt32();
    //      this.track.stsc = [];
    //      for (i = k = 0, ref = entries; k < ref; i = k += 1) {
    //        this.track.stsc[i] = {
    //          first: this.stream.readUInt32(),
    //          count: this.stream.readUInt32(),
    //          id: this.stream.readUInt32()
    //        };
    //      }
    //      return this.setupSeekPoints();
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stsz", function() {
    //      var entries, i, k, ref;
    //      this.stream.advance(4);
    //      this.track.sampleSize = this.stream.readUInt32();
    //      entries = this.stream.readUInt32();
    //      if (this.track.sampleSize === 0 && entries > 0) {
    //        this.track.sampleSizes = [];
    //        for (i = k = 0, ref = entries; k < ref; i = k += 1) {
    //          this.track.sampleSizes[i] = this.stream.readUInt32();
    //        }
    //      }
    //      return this.setupSeekPoints();
    //    });

    //    atom("moov.trak.mdia.minf.stbl.stco", function() {
    //      var entries, i, k, ref;
    //      this.stream.advance(4);
    //      entries = this.stream.readUInt32();
    //      this.track.chunkOffsets = [];
    //      for (i = k = 0, ref = entries; k < ref; i = k += 1) {
    //        this.track.chunkOffsets[i] = this.stream.readUInt32();
    //      }
    //      return this.setupSeekPoints();
    //    });

    //    atom("moov.trak.tref.chap", function() {
    //      var entries, i, k, ref;
    //      entries = this.len >> 2;
    //      this.track.chapterTracks = [];
    //      for (i = k = 0, ref = entries; k < ref; i = k += 1) {
    //        this.track.chapterTracks[i] = this.stream.readUInt32();
    //      }
    //    });

    //    M4ADemuxer.prototype.setupSeekPoints = function() {
    //      var i, j, k, l, len1, offset, position, ref, ref1, results, sampleIndex, size, stscIndex, sttsIndex, sttsSample, timestamp;
    //      if (!((this.track.chunkOffsets != null) && (this.track.stsc != null) && (this.track.sampleSize != null) && (this.track.stts != null))) {
    //        return;
    //      }
    //      stscIndex = 0;
    //      sttsIndex = 0;
    //      sttsIndex = 0;
    //      sttsSample = 0;
    //      sampleIndex = 0;
    //      offset = 0;
    //      timestamp = 0;
    //      this.track.seekPoints = [];
    //      ref = this.track.chunkOffsets;
    //      results = [];
    //      for (i = k = 0, len1 = ref.length; k < len1; i = ++k) {
    //        position = ref[i];
    //        for (j = l = 0, ref1 = this.track.stsc[stscIndex].count; l < ref1; j = l += 1) {
    //          this.track.seekPoints.push({
    //            offset: offset,
    //            position: position,
    //            timestamp: timestamp
    //          });
    //          size = this.track.sampleSize || this.track.sampleSizes[sampleIndex++];
    //          offset += size;
    //          position += size;
    //          timestamp += this.track.stts[sttsIndex].duration;
    //          if (sttsIndex + 1 < this.track.stts.length && ++sttsSample === this.track.stts[sttsIndex].count) {
    //            sttsSample = 0;
    //            sttsIndex++;
    //          }
    //        }
    //        if (stscIndex + 1 < this.track.stsc.length && i + 1 === this.track.stsc[stscIndex + 1].first) {
    //          results.push(stscIndex++);
    //        } else {
    //          results.push(void 0);
    //        }
    //      }
    //      return results;
    //    };

    //    after("moov", function() {
    //      var k, len1, ref, track;
    //      if (this.mdatOffset != null) {
    //        this.stream.seek(this.mdatOffset - 8);
    //      }
    //      ref = this.tracks;
    //      for (k = 0, len1 = ref.length; k < len1; k++) {
    //        track = ref[k];
    //        if (!(track.type === "soun")) {
    //          continue;
    //        }
    //        this.track = track;
    //        break;
    //      }
    //      if (this.track.type !== "soun") {
    //        this.track = null;
    //        return this.emit("error", "No audio tracks in m4a file.");
    //      }
    //      this.emit("format", this.track.format);
    //      this.emit("duration", this.track.duration / this.track.timeScale * 1000 | 0);
    //      if (this.track.cookie) {
    //        this.emit("cookie", this.track.cookie);
    //      }
    //      return this.seekPoints = this.track.seekPoints;
    //    });

    //    atom("mdat", function() {
    //      var bytes, chunkSize, k, length, numSamples, offset, ref, sample, size;
    //      if (!this.startedData) {
    //        if (this.mdatOffset == null) {
    //          this.mdatOffset = this.stream.offset;
    //        }
    //        if (this.tracks.length === 0) {
    //          bytes = Math.min(this.stream.remainingBytes(), this.len);
    //          this.stream.advance(bytes);
    //          this.len -= bytes;
    //          return;
    //        }
    //        this.chunkIndex = 0;
    //        this.stscIndex = 0;
    //        this.sampleIndex = 0;
    //        this.tailOffset = 0;
    //        this.tailSamples = 0;
    //        this.startedData = true;
    //      }
    //      if (!this.readChapters) {
    //        this.readChapters = this.parseChapters();
    //        if (this["break"] = !this.readChapters) {
    //          return;
    //        }
    //        this.stream.seek(this.mdatOffset);
    //      }
    //      offset = this.track.chunkOffsets[this.chunkIndex] + this.tailOffset;
    //      length = 0;
    //      if (!this.stream.available(offset - this.stream.offset)) {
    //        this["break"] = true;
    //        return;
    //      }
    //      this.stream.seek(offset);
    //      while (this.chunkIndex < this.track.chunkOffsets.length) {
    //        numSamples = this.track.stsc[this.stscIndex].count - this.tailSamples;
    //        chunkSize = 0;
    //        for (sample = k = 0, ref = numSamples; k < ref; sample = k += 1) {
    //          size = this.track.sampleSize || this.track.sampleSizes[this.sampleIndex];
    //          if (!this.stream.available(length + size)) {
    //            break;
    //          }
    //          length += size;
    //          chunkSize += size;
    //          this.sampleIndex++;
    //        }
    //        if (sample < numSamples) {
    //          this.tailOffset += chunkSize;
    //          this.tailSamples += sample;
    //          break;
    //        } else {
    //          this.chunkIndex++;
    //          this.tailOffset = 0;
    //          this.tailSamples = 0;
    //          if (this.stscIndex + 1 < this.track.stsc.length && this.chunkIndex + 1 === this.track.stsc[this.stscIndex + 1].first) {
    //            this.stscIndex++;
    //          }
    //          if (offset + length !== this.track.chunkOffsets[this.chunkIndex]) {
    //            break;
    //          }
    //        }
    //      }
    //      if (length > 0) {
    //        this.emit("data", this.stream.readBuffer(length));
    //        return this["break"] = this.chunkIndex === this.track.chunkOffsets.length;
    //      } else {
    //        return this["break"] = true;
    //      }
    //    });

    //    M4ADemuxer.prototype.parseChapters = function() {
    //      var bom, id, k, len, len1, nextTimestamp, point, ref, ref1, ref2, ref3, title, track;
    //      if (!(((ref = this.track.chapterTracks) != null ? ref.length : void 0) > 0)) {
    //        return true;
    //      }
    //      id = this.track.chapterTracks[0];
    //      ref1 = this.tracks;
    //      for (k = 0, len1 = ref1.length; k < len1; k++) {
    //        track = ref1[k];
    //        if (track.id === id) {
    //          break;
    //        }
    //      }
    //      if (track.id !== id) {
    //        this.emit("error", "Chapter track does not exist.");
    //      }
    //      if (this.chapters == null) {
    //        this.chapters = [];
    //      }
    //      while (this.chapters.length < track.seekPoints.length) {
    //        point = track.seekPoints[this.chapters.length];
    //        if (!this.stream.available(point.position - this.stream.offset + 32)) {
    //          return false;
    //        }
    //        this.stream.seek(point.position);
    //        len = this.stream.readUInt16();
    //        title = null;
    //        if (!this.stream.available(len)) {
    //          return false;
    //        }
    //        if (len > 2) {
    //          bom = this.stream.peekUInt16();
    //          if (bom === 0xfeff || bom === 0xfffe) {
    //            title = this.stream.readString(len, "utf16-bom");
    //          }
    //        }
    //        if (title == null) {
    //          title = this.stream.readString(len, "utf8");
    //        }
    //        nextTimestamp = (ref2 = (ref3 = track.seekPoints[this.chapters.length + 1]) != null ? ref3.timestamp : void 0) != null ? ref2 : track.duration;
    //        this.chapters.push({
    //          title: title,
    //          timestamp: point.timestamp / track.timeScale * 1000 | 0,
    //          duration: (nextTimestamp - point.timestamp) / track.timeScale * 1000 | 0
    //        });
    //      }
    //      this.emit("chapters", this.chapters);
    //      return true;
    //    };

    //    atom("moov.udta.meta", function() {
    //      this.metadata = {};
    //      return this.stream.advance(4);
    //    });

    //    after("moov.udta.meta", function() {
    //      return this.emit("metadata", this.metadata);
    //    });

    //    meta = function(field, name, fn) {
    //      return atom("moov.udta.meta.ilst." + field + ".data", function() {
    //        this.stream.advance(8);
    //        this.len -= 8;
    //        return fn.call(this, name);
    //      });
    //    };

    //    string = function(field) {
    //      return this.metadata[field] = this.stream.readString(this.len, "utf8");
    //    };

    //    meta("©alb", "album", string);

    //    meta("©arg", "arranger", string);

    //    meta("©art", "artist", string);

    //    meta("©ART", "artist", string);

    //    meta("aART", "albumArtist", string);

    //    meta("catg", "category", string);

    //    meta("©com", "composer", string);

    //    meta("©cpy", "copyright", string);

    //    meta("cprt", "copyright", string);

    //    meta("©cmt", "comments", string);

    //    meta("©day", "releaseDate", string);

    //    meta("desc", "description", string);

    //    meta("©gen", "genre", string);

    //    meta("©grp", "grouping", string);

    //    meta("©isr", "ISRC", string);

    //    meta("keyw", "keywords", string);

    //    meta("©lab", "recordLabel", string);

    //    meta("ldes", "longDescription", string);

    //    meta("©lyr", "lyrics", string);

    //    meta("©nam", "title", string);

    //    meta("©phg", "recordingCopyright", string);

    //    meta("©prd", "producer", string);

    //    meta("©prf", "performers", string);

    //    meta("purd", "purchaseDate", string);

    //    meta("purl", "podcastURL", string);

    //    meta("©swf", "songwriter", string);

    //    meta("©too", "encoder", string);

    //    meta("©wrt", "composer", string);

    //    meta("covr", "coverArt", function(field) {
    //      return this.metadata[field] = this.stream.readBuffer(this.len);
    //    });

    //    genres = ["Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop", "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "AlternRock", "Bass", "Soul", "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychadelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk", "Folk/Rock", "National Folk", "Swing", "Fast Fusion", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "A Capella", "Euro-House", "Dance Hall"];

    //    meta("gnre", "genre", function(field) {
    //      return this.metadata[field] = genres[this.stream.readUInt16() - 1];
    //    });

    //    meta("tmpo", "tempo", function(field) {
    //      return this.metadata[field] = this.stream.readUInt16();
    //    });

    //    meta("rtng", "rating", function(field) {
    //      var rating;
    //      rating = this.stream.readUInt8();
    //      return this.metadata[field] = rating === 2 ? "Clean" : rating !== 0 ? "Explicit" : "None";
    //    });

    //    diskTrack = function(field) {
    //      this.stream.advance(2);
    //      this.metadata[field] = this.stream.readUInt16() + " of " + this.stream.readUInt16();
    //      return this.stream.advance(this.len - 6);
    //    };

    //    meta("disk", "diskNumber", diskTrack);

    //    meta("trkn", "trackNumber", diskTrack);

    //    bool = function(field) {
    //      return this.metadata[field] = this.stream.readUInt8() === 1;
    //    };

    //    meta("cpil", "compilation", bool);

    //    meta("pcst", "podcast", bool);

    //    meta("pgap", "gapless", bool);

    //    return M4ADemuxer;

    //  })(Demuxer);

    //  module.exports = M4ADemuxer;
  }
}
