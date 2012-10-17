using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns a "deep copied" instance of any object which supports serialisation
        /// </summary>
        /// <typeparam name="T">The type of object to be DeepCloned</typeparam>
        /// <param name="a">The object to be DeepCloned</param>
        /// <returns>A deep copy (new instance in memory) of the source object</returns>
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void CopyStream(this Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }


    }
}
