using System;

namespace PolyglotFramework
{
    [Serializable]
    public class ModeNotSupportedException : Exception
    {
        public ModeNotSupportedException(string message, ModuleMode incompatibleMode, IActivatableModule module)
            : base(message)
        {
            IncompatibleMode = incompatibleMode;
            Module = module;
        }

        public ModuleMode IncompatibleMode { get; private set; }
        public IActivatableModule Module { get; private set; }
    }

    [Serializable]
    public class NoDataYetException : Exception
    {
        public NoDataYetException(string message, IActivatableModule module)
            : base(message)
        {
            Module = module;
        }

        public IActivatableModule Module { get; private set; }
    }

    [Serializable]
    public class ModuleImplementationException : Exception
    {
        public ModuleImplementationException(string message)
            :base (message)
        {
        }
    }

    // Some exceptions (those with very specific application) live in other classes:
    // ~ WebCamMissingException in WebCamForm.cs
}
