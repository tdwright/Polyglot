using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace PolyglotFramework
{
    /// <summary>
    /// PolyglotEngine manages modules and runs the system. Public methods are designed to be called from one of the GUIs.
    /// </summary>
    public class PolyglotEngine
    {
        /// <summary>
        /// AcquisitionModules is a collection of all the available modules implementing IAcquisition
        /// </summary>
        [ImportMany(typeof(IAcquisition))]
        public IEnumerable<IAcquisition> AcquisitionModules;

        /// <summary>
        /// PointerModules is a collection of all the available modules implementing IPointer
        /// </summary>
        [ImportMany(typeof(IPointer))]
        public IEnumerable<IPointer> PointerModules;

        /// <summary>
        /// OutputModules is a collection of all the available modules implementing IOutput
        /// </summary>
        [ImportMany(typeof(IOutput))]
        public IEnumerable<IOutput> OutputModules;

        /// <summary>
        /// TransformationModules is a collection of all the available modules implementing ITransformation
        /// </summary>
        [ImportMany(typeof(ITransformation))]
        public IEnumerable<ITransformation> TransformationModules;
        private ITransformation TransformationModule;

        public event EventHandler<EngineStoppedEventArgs> Stopped;

        private PolyglotEngine()
        {
            try
            {
                AggregateCatalog aggregatecatalogue = new AggregateCatalog();
                aggregatecatalogue.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
                string ModuleDir = AppDomain.CurrentDomain.BaseDirectory + "Modules";
                if (Directory.Exists(ModuleDir))
                {
                    Console.WriteLine("Modules directory found");
                    aggregatecatalogue.Catalogs.Add(new DirectoryCatalog(ModuleDir));
                    foreach (string subdirectory in Directory.GetDirectories(ModuleDir))
                    {
                        Console.WriteLine("Module subdirectory found: {0}", Path.GetDirectoryName(subdirectory));
                        aggregatecatalogue.Catalogs.Add(new DirectoryCatalog(subdirectory));
                    }
                }
                else
                {
                    Console.WriteLine("Modules directory NOT found");
                    Console.WriteLine(ModuleDir);
                }
                CompositionContainer container = new CompositionContainer(aggregatecatalogue);
                container.ComposeParts(this);

                foreach (IAcquisition module in AcquisitionModules)
                {
                    Console.WriteLine(module.ModuleName);
                }

            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine(fnfex.Message);
            }
            catch (CompositionException cex)
            {
                Console.WriteLine(cex.Message);
                throw new ModuleImplementationException(cex.Message);
            }
        }

        /// <summary>
        /// Static factory method for PolyglotEngine
        /// </summary>
        /// <returns>Returns a PolyglotEngine instance</returns>
        static public PolyglotEngine NewEngine()
        {
            return new PolyglotEngine();
        }

        /// <summary>
        /// After selecting which modules are to be used, run chains them together and (would you beleive it!) runs the newly composed system.
        /// </summary>
        /// <param name="AcquisitionIndex">
        ///     The index of the selected Acquisition module within the collection of available Acquisition modules
        /// </param>
        /// <param name="PointerIndex">
        ///     The index of the selected Pointer module within the collection of available Pointer modules
        /// </param>
        /// <param name="TransformationIndex">
        ///     The index of the selected Transformation module within the collection of available Transformation modules
        /// </param>
        /// <param name="OutputIndex">
        ///     The index of the selected Output module within the collection of available Output modules
        /// </param>
        public void Run(int AcquisitionIndex, int PointerIndex, int TransformationIndex, int OutputIndex)
        {
            this.TransformationModule = TransformationModules.ElementAt<ITransformation>(TransformationIndex);
            this.TransformationModule.LoadModules(
                    AcquisitionModules.ElementAt<IAcquisition>(AcquisitionIndex),
                    PointerModules.ElementAt<IPointer>(PointerIndex),
                    OutputModules.ElementAt<IOutput>(OutputIndex)
                );
            this.TransformationModule.Stopped += TransformationModule_Stopped;

            this.TransformationModule.ModuleActivate();
        }

        void TransformationModule_Stopped(object sender, EngineStoppedEventArgs e)
        {
            this.Stop();
        }

        public void Stop()
        {
            if(this.TransformationModule!=null) this.TransformationModule.ModuleDeactivate();
            this.Stopped(this, new EngineStoppedEventArgs());
        }
    }
}
