using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public interface IBox
  {
    IBox getParent();

    /// <summary>
    /// Returns the size of this box including its header.
    /// </summary>
    /// <returns>this box's size</returns>
    long getSize();

    /// <summary>
    /// Returns the type of this box as a 4CC converted to a long.
    /// </summary>
    /// <returns>this box's type</returns>
    BoxType getType();

    /// <summary>
    /// Returns the offset of this box in the stream/file. This is needed as a seek point for random access.
    /// </summary>
    /// <returns>this box's offset</returns>
    long getOffset();

    /// <summary>
    /// Returns the name of this box as a human-readable string (e.g. "Track Header Box").
    /// </summary>
    /// <returns>this box's name</returns>
    string getName();

    /// <summary>
    /// Indicates if this box has children.
    /// </summary>
    /// <returns>true if this box contains at least one child</returns>
    bool hasChildren();

    /// <summary>
    /// Indicated if the box has a child with the given type.
    /// </summary>
    /// <param name="type">the type of child box to look for</param>
    /// <returns>true if this box contains at least one child with the given type</returns>
    bool hasChild(BoxType type);

    /// <summary>
    /// Returns an ordered and unmodifiable list of all direct children of this box. The list does not contain the children's children.
    /// </summary>
    /// <returns>this box's children</returns>
    IBox[] getChildren();

    /// <summary>
    /// Returns an ordered and unmodifiable list of all direct children of this box with the specified type. 
    /// The list does not contain the children's children. If there is no child with the given type, the list will be empty.
    /// </summary>
    /// <param name="type">the type of child boxes to look for</param>
    /// <returns>this box's children with the given type</returns>
    IBox[] getChildren(BoxType type);

    /// <summary>
    /// Returns the child box with the specified type. If this box has no child with the given type, null is returned. To check if there is such a child <code>hasChild(type)</code> can be used.
    /// If more than one child exists with the same type, the first one will always be returned. A list of all children with that type can be received via <code>getChildren(type)</code>.
    /// </summary>
    /// <param name="type">the type of child box to look for</param>
    /// <returns>the first child box with the given type, or null if none is found</returns>
    IBox getChild(BoxType type);
  }
}
