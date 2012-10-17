using System;
using System.Drawing;

namespace PolyglotFramework
{

    /// <summary>
    /// NewImageEventArgs are used by Acquisition modules when in Active mode. They have a payload consisting of the bitmap image.
    /// </summary>
    public class NewImageEventArgs : EventArgs
    {
        public NewImageEventArgs(Bitmap image)
        {
            Image = image;
        }
        public Bitmap Image { get; private set; }
    }

    /// <summary>
    /// NewPositionEventArgs are used by Pointer modules when in Active mode and new coordinates are available. They have a payload consisting of the position point.
    /// </summary>
    public class NewPositionEventArgs : EventArgs
    {
        public NewPositionEventArgs(ProportionPoint position)
        {
            Position = position;
        }
        public ProportionPoint Position { get; private set; }
    }

    /// <summary>
    /// NewPositionEventArgs are used by Pointer modules when in Active mode and the pointer is lifted or lowered. They have a payload consisting of the position state (PointerDown = true when lowered).
    /// </summary>
    public class PointerStateChangeEventArgs : EventArgs
    {
        public PointerStateChangeEventArgs(bool pointerDown)
        {
            PointerDown = pointerDown;
        }
        public bool PointerDown { get; private set; }
    }

    /// <summary>
    /// Fired when a form-based module is closed
    /// </summary>
    public class ModuleFormClosedEventArgs : EventArgs { }

    /// <summary>
    /// Fired when the engine / transformation module stops
    /// </summary>
    public class EngineStoppedEventArgs : EventArgs { }
}