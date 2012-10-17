using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework
{
    /// <summary>
    /// All modules (Aquisition, Pointer, Transformation, Output) must implement at least a ModuleName and ModuleID
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Should contain a descriptive name of the module. Is displayed in the composer.
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// Must be a unique ID. Should be a GUID. Should not change between version of a module. Used to store preferences.
        /// </summary>
        string ModuleID { get; }

    }

    /// <summary>
    /// Most modules (all except Output) should implement methods to be activated and deactivated
    /// </summary>
    public interface IActivatableModule : IModule
    {

        /// <summary>
        /// Called when the system starts if the module has been selected.
        /// </summary>
        void ModuleActivate();

        /// <summary>
        /// Called when the system stops if the module is part of the running system.
        /// </summary>
        void ModuleDeactivate();
    }

    /// <summary>
    /// Acquisition modules are used to capture images
    /// </summary>
    public interface IAcquisition : IActivatableModule
    {
        /// <summary>
        /// NewImage events are raised when the ModuleMode is set to Active and a new image is captured.
        /// </summary>
        event EventHandler<NewImageEventArgs> NewImage;

        /// <summary>
        /// Mode dictates whether the module triggers a NewImage event is raised ("Active"). If Passive, new images are only returned after a call to GetImage()
        /// </summary>
        ModuleMode Mode { get; set; }

        /// <summary>
        /// To be used when the module is in Passive mode.
        /// </summary>
        /// <returns>Bitmap of the most recently acquired image</returns>
        Bitmap GetImage();
    }

    /// <summary>
    /// Position modules are used to supply the current position of interest. Position modules also offer a single output channel.
    /// </summary>
    public interface IPointer : IActivatableModule
    {
        /// <summary>
        /// NewPosition events are raised when the ModuleMode is set to Active and a new position is captured.
        /// </summary>
        event EventHandler<NewPositionEventArgs> NewPosition;

        /// <summary>
        /// PointerUp events are fired when the ModuleMode is set to Active and the pointer is "raised" (e.g. mouse button released)
        /// </summary>
        event EventHandler<PointerStateChangeEventArgs> PointerStateChange;

        /// <summary>
        /// Mode dictates whether the module triggers a NewPoint event is raised ("Active"). If Passive, new coordinates are only returned after a call to GetPoint()
        /// </summary>
        ModuleMode Mode { get; set; }

        /// <summary>
        /// To be used when the module is in Passive mode.
        /// </summary>
        /// <returns>Point of the most recently acquired position</returns>
        ProportionPoint GetPosition();

        /// <summary>
        /// Some pointer hardware (tablets, haptic mice) support vibrotactile feedback. This method sets the intensity of such an output channel. Modules that do not support an output channel may simply return.
        /// </summary>
        /// <param name="intensity">A value between 0f and 1f</param>
        void SetIntensity(float intensity);
    }

    public interface ITransformation : IActivatableModule
    {
        bool Ready { get; }

        void LoadModules(IAcquisition AcquisitionModule, IPointer PointerModule, IOutput OutputModule);

        event EventHandler<EngineStoppedEventArgs> Stopped;
    }

    public interface IOutput : IModule
    {
        /// <summary>
        /// Volume of this output. Should be between 0f and 1f.
        /// </summary>
        float Amplitude { get; set; }

        /// <summary>
        /// Frequency of this output. Must be a >0 (negative values will have absolute values taken, 0 will trigger exception)
        /// </summary>
        float Frequency { get; set; }

        /// <summary>
        /// Azimuthal angle relative to nasion. Left ear is -90f, right ear is 90f. All values accepted, but will map onto this effective range.
        /// </summary>
        float Angle { get; set; }

        void Play();

        void Stop();
    }

    /// <summary>
    /// Enum containing the possible modes an acquisition or pointer module may operate in.
    /// </summary>
    public enum ModuleMode
    {
        /// <summary>Module will actively raise events in response to changes.</summary>
        Active,

        /// <summary>Module will return data in response to method calls.</summary>
        Passive
    };
}
