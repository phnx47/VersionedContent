using System;
using System.IO;
using System.Reflection;

namespace VersionedContent
{
    /// <summary>
    /// Helper for get timestamp from calling dll
    /// </summary>
    public static class TimestampBuild
    {
        /// <summary>
        /// Get Timestamp Build
        /// </summary>
        public static int Get()
        {
            var filePath = Assembly.GetCallingAssembly().Location;

            /* for netstandart
             *  var filePath = PlatformServices.Default.Application.ApplicationBasePath
                + Path.DirectorySeparatorChar
                + PlatformServices.Default.Application.ApplicationName + ".dll";
             */

            const int cPeHeaderOffset = 60;
            const int cLinkerTimestampOffset = 8;
            var b = new byte[2048];
            using (Stream s = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                s.Read(b, 0, 2048);
            }

            var i = BitConverter.ToInt32(b, cPeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(b, i + cLinkerTimestampOffset);
            return secondsSince1970;
        }
    }
}