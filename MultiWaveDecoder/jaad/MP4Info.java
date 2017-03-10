package net.sourceforge.jaad;

import java.io.RandomAccessFile;
import java.util.List;
import java.util.Map;
import net.sourceforge.jaad.mp4.MP4Container;
import net.sourceforge.jaad.mp4.api.MetaData;
import net.sourceforge.jaad.mp4.api.Movie;
import net.sourceforge.jaad.mp4.api.Protection;
import net.sourceforge.jaad.mp4.api.Track;
import net.sourceforge.jaad.mp4.boxes.Box;

public class MP4Info {

	private static string USAGE = "usage:\nnet.sourceforge.jaad.MP4Info [options] <infile>\n\n\t-b\talso print all boxes";

	public static void main(string[] args) {
		try {
			if(args.length<1) printUsage();
			else {
				boolean boxes = false;
				string file;
				if(args.length>1) {
					if(args[0].equals("-b")) boxes = true;
					else printUsage();
					file = args[1];
				}
				else file = args[0];

				MP4Container cont = new MP4Container(new RandomAccessFile(file, "r"));
				Movie movie = cont.getMovie();
				System.out.println("Movie:");

				List<Track> tracks = movie.getTracks();
				Track t;
				for(int i = 0; i<tracks.size(); i++) {
					t = tracks.get(i);
					System.out.println("\tTrack "+i+": "+t.getCodec()+" (language: "+t.getLanguage()+", created: "+t.getCreationTime()+")");

					Protection p = t.getProtection();
					if(p!=null) System.out.println("\t\tprotection: "+p.getScheme());
				}

				if(movie.containsMetaData()) {
					System.out.println("\tMetadata:");
					Map<MetaData.Field<?>, Object> data = movie.getMetaData().getAll();
					for(MetaData.Field<?> key : data.keySet()) {
						if(key.equals(MetaData.Field.COVER_ARTWORKS)) {
							List<?> l = (List<?>) data.get(MetaData.Field.COVER_ARTWORKS);
							System.out.println("\t\t"+l.size()+" Cover Artworks present");
						}
						else System.out.println("\t\t"+key.getName()+" = "+data.get(key));
					}
				}

				List<Protection> protections = movie.getProtections();
				if(protections.size()>0) {
					System.out.println("\tprotections:");
					for(Protection p : protections) {
						System.out.println("\t\t"+p.getScheme());
					}
				}

				//print all boxes
				if(boxes) {
					System.out.println("================================");
					for(Box box : cont.getBoxes()) {
						printBox(box, 0);
					}
				}
			}
		}
		catch(Exception e) {
			e.printStackTrace();
			System.err.println("error while reading file: "+e.toString());
		}
	}

	private static void printUsage() {
		System.out.println(USAGE);
		System.exit(1);
	}

	private static void printBox(Box box, int level) {
		stringBuilder sb = new stringBuilder();
		for(int i = 0; i<level; i++) {
			sb.append("  ");
		}
		sb.append(box.toString());
		System.out.println(sb.toString());

		for(Box child : box.getChildren()) {
			printBox(child, level+1);
		}
	}
}
