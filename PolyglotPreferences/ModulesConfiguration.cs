using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PolyglotFramework.Preferences
{
    public class ModulesConfiguration
    {
        [XmlElement(ElementName="Acquisition")]
        public ModuleMetaData AcquisitionModule;

        [XmlElement(ElementName ="Pointer")]
        public ModuleMetaData PointerModule;

        [XmlElement(ElementName="Transformation")]
        public ModuleMetaData TransformationModule;

        [XmlElement(ElementName="Output")]
        public ModuleMetaData OutputModule;

        public bool SaveConfiguration(string filename)
        {
            bool success = true;
            XmlSerializer s = new XmlSerializer(typeof(ModulesConfiguration));
            try
            {
                using (TextWriter tw = new StreamWriter(filename))
                {
                    s.Serialize(tw, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }
            return success;
        }

        public static ModulesConfiguration LoadConfiguration(string filename)
        {
            XmlSerializer s = new XmlSerializer(typeof(ModulesConfiguration));
            ModulesConfiguration config;
            using (TextReader tr = new StreamReader(filename))
            {
                config = (ModulesConfiguration)s.Deserialize(tr);
            }
            return config;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ModulesConfiguration);
        }

        public bool Equals(ModulesConfiguration mc)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(mc, null)) return false;

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, mc)) return true;

            // If types are different, instances can't be equal
            if (this.GetType() != mc.GetType()) return false;

            // finally let equality be determined by IDs.
            return (
                (mc.AcquisitionModule.ModuleID == this.AcquisitionModule.ModuleID) &&
                (mc.PointerModule.ModuleID == this.PointerModule.ModuleID) &&
                (mc.TransformationModule.ModuleID == this.TransformationModule.ModuleID) &&
                (mc.OutputModule.ModuleID == this.OutputModule.ModuleID)
                );
        }

        public static bool operator ==(ModulesConfiguration lhs, ModulesConfiguration rhs)
        {
            // Check for null on left side. 
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true. 
                    return true;
                }

                // Only the left side is null. 
                return false;
            }
            // Equals handles case of null on right side. 
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ModulesConfiguration lhs, ModulesConfiguration rhs)
        {
            return !(lhs == rhs);
        }

    }

    public struct ModuleMetaData
    {
        [XmlText]
        public string ModuleName;

        [XmlAttribute("id")]
        public string ModuleID;
    }
}
