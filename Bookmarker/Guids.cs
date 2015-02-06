// Guids.cs
// MUST match guids.h
using System;

namespace Bookmarker
{
    static class GuidList
    {
        public const string guidBookmarkerPkgString = "199ca38b-2561-4613-a57f-68d8ba60448f";
        public const string guidBookmarkerCmdSetString = "0858704e-548b-46d9-9f2b-49605b6f3d2d";

        public static readonly Guid guidBookmarkerCmdSet = new Guid(guidBookmarkerCmdSetString);
    };
}