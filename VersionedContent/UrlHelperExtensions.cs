using System;
using System.Web.Caching;
using System.Web.Mvc;

namespace VersionedContent
{
    /// <summary>
    /// Extensions for UrlHelper
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Versioned content
        /// </summary>
        public static string VersionedContent(this UrlHelper helper, string contentPath)
        {
            var context = helper.RequestContext.HttpContext;
            if (context.Cache[contentPath] != null)
                return context.Cache[contentPath] as string;

            var physicalPath = context.Server.MapPath(contentPath);
            var version = "?v=" + TimestampBuild.Get();
            var translatedContentPath = helper.Content(contentPath);
            var versionedContentPath = translatedContentPath + version;
            context.Cache.Add(physicalPath, version, null, DateTime.UtcNow.AddDays(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
            context.Cache[contentPath] = versionedContentPath;
            return versionedContentPath;
        }
    }
}