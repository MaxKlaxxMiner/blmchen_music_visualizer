/*
* some functions for MP4 files
*/

#include "mp4ff.h"
#include "faad.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

const char *mp4AudioNames[] =
{
  "MPEG-1 Audio Layers 1,2 or 3",
  "MPEG-2 low biterate (MPEG-1 extension) - MP3",
  "MPEG-2 AAC Main Profile",
  "MPEG-2 AAC Low Complexity profile",
  "MPEG-2 AAC SSR profile",
  "MPEG-4 audio (MPEG-4 AAC)",
  0
};

/* MPEG-4 Audio types from 14496-3 Table 1.5.1 (from mp4.h)*/
const char *mpeg4AudioNames[] =
{
  "!!!!MPEG-4 Audio track Invalid !!!!!!!",
  "MPEG-4 AAC Main profile",
  "MPEG-4 AAC Low Complexity profile",
  "MPEG-4 AAC SSR profile",
  "MPEG-4 AAC Long Term Prediction profile",
  "MPEG-4 AAC Scalable",
  "MPEG-4 CELP",
  "MPEG-4 HVXC",
  "MPEG-4 Text To Speech",
  "MPEG-4 Main Synthetic profile",
  "MPEG-4 Wavetable Synthesis profile",
  "MPEG-4 MIDI Profile",
  "MPEG-4 Algorithmic Synthesis and Audio FX profile"
};

/*
* find AAC track
*/

int getAACTrack(mp4ff_t *infile)
{
  int i, rc;
  int numTracks = mp4ff_total_tracks(infile);

  printf("total-tracks: %d\n", numTracks);
  for (i = 0; i<numTracks; i++)
  {
    unsigned char*	buff = 0;
    int			buff_size = 0;
    mp4AudioSpecificConfig mp4ASC;

    printf("testing-track: %d\n", i);
    mp4ff_get_decoder_config(infile, i, &buff, (unsigned int*)&buff_size);
    if (buff)
    {
      rc = NeAACDecAudioSpecificConfig(buff, buff_size, &mp4ASC);
      free(buff);
      if (rc < 0)
        continue;
      return(i);
    }
  }
  return(-1);
}

uint32_t read_callback(void *user_data, void *buffer, uint32_t length)
{
  return fread(buffer, 1, length, (FILE*)user_data);
}

uint32_t seek_callback(void *user_data, uint64_t position)
{
  return fseek((FILE*)user_data, (uint32_t)position, SEEK_SET);
}

mp4ff_callback_t *getMP4FF_cb(FILE *mp4file)
{
  mp4ff_callback_t* mp4cb = (mp4ff_callback_t*)malloc(sizeof(mp4ff_callback_t));
  mp4cb->read = read_callback;
  mp4cb->seek = seek_callback;
  mp4cb->user_data = mp4file;
  return mp4cb;
}

void getMP4info(char* filename, FILE* mp4file)
{
  mp4ff_callback_t*	mp4cb;
  mp4ff_t*		infile;
  int mp4track;

  mp4cb = getMP4FF_cb(mp4file);
  if ((infile = mp4ff_open_read_metaonly(mp4cb)) &&
    ((mp4track = getAACTrack(infile)) >= 0))
  {
  }
  if (infile) mp4ff_close(infile);
  if (mp4cb) free(mp4cb);
}
