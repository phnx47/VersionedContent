using System;
using System.IO;
using System.Reflection;
using System.Web.Caching;
using System.Web.Mvc;

namespace VersionedContent
{
    /// <summary>
    ///     Extensions for UrlHelper
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        ///     Versioned content
        /// </summary>
        public static string VersionedContent(this UrlHelper helper, string contentPath)
        {
            var context = helper.RequestContext.HttpContext;
            if (context.Cache[contentPath] != null)
                return context.Cache[contentPath] as string;

            var now = DateTime.UtcNow;
            var physicalPath = context.Server.MapPath(contentPath);
            var version = "?v=" + (long)(now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            var translatedContentPath = helper.Content(contentPath);
            var versionedContentPath = translatedContentPath + version;
            context.Cache.Add(physicalPath, version, null, now.AddDays(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
            context.Cache[contentPath] = versionedContentPath;
            return versionedContentPath;
        }
    }
}