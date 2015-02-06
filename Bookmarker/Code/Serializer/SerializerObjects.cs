using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarker.Code.Serializer
{
    /// <summary>
    /// Positions of all bookmarks in a solution
    /// </summary>
    [Serializable]
    public class SolutionBookmarkPositions
    {
        [JsonProperty("document-bookmark-positions")]
        public List<DocumentBookmarkPositions> Positions { get; set; }
    }
        
    /// <summary>
    /// Bookmark positions in a document
    /// </summary>
    [Serializable]
    public class DocumentBookmarkPositions
    {
        [JsonProperty("document-path")]
        public string DocumentPath { get; set; }
        [JsonProperty("bookmark-positions")]
        public List<int> BookmarkPositions { get; set; }
    }
}
