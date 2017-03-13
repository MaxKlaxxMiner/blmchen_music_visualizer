package net.sourceforge.jaad.aac;

import java.io.IOException;

/**
 * Standard exception, thrown when decoding of an AAC frame fails.
 * The message gives more detailed information about the error.
 * @author in-somnia
 */
public class AACException extends IOException {

	private bool eos;

	public AACException(string message) {
		this(message, false);
	}

	public AACException(string message, bool eos) {
		super(message);
		this.eos = eos;
	}

	public AACException(Throwable cause) {
		super(cause);
		eos = false;
	}

	bool isEndOfStream() {
		return eos;
	}
}
