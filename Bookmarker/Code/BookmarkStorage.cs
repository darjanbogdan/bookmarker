using Bookmarker.Code.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarker.Code
{
    public class BookmarkStorage
    {
        #region Fields

        private readonly string positionsFolderPath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData,
            Environment.SpecialFolderOption.Create),
            @"Bookmarker\"
        );

        private readonly string positionsFileName = "BookmarkerPositions.json";
        private string positionsFullName;
        #endregion

        #region Properties
      
        private static SolutionBookmarkPositions _bookmarkPositions = null;
        public SolutionBookmarkPositions BookmarkPositions
        {
            get { return _bookmarkPositions; }
        }

        #endregion

        #region Constructor

        protected BookmarkStorage()
        {
            positionsFullName = String.Format("{0}{1}", positionsFolderPath, positionsFileName);
        }

        #endregion

        #region Instance

        public static BookmarkStorage GetInstance()
        {
            return new BookmarkStorage();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            Directory.CreateDirectory(positionsFolderPath);
            if (_bookmarkPositions == null)
                _bookmarkPositions = ReadPositions();       
        }

        public void WritePositions(SolutionBookmarkPositions positions)
        { 
            if(positions != null)
            {
                FileStream fileStream = new FileStream(positionsFullName, FileMode.OpenOrCreate, FileAccess.Write);
                using(StreamWriter writer = new StreamWriter(fileStream))
                { 
                    string json = JsonConvert.SerializeObject(positions);
                    if(!String.IsNullOrEmpty(json))
                    {
                        writer.Write(json);
                    }
                }
            }
        }

        public SolutionBookmarkPositions ReadPositions()
        {
            FileStream fileStream = new FileStream(positionsFullName, FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string json = reader.ReadToEnd();
                SolutionBookmarkPositions bookmarkPositions = JsonConvert.DeserializeObject<SolutionBookmarkPositions>(json);
                if (bookmarkPositions != null)
                {
                    if (bookmarkPositions.Positions == null)
                        bookmarkPositions.Positions = new List<DocumentBookmarkPositions>();
                }
                else
                {
                    bookmarkPositions = CreateSolutionBookmarkPositions();
                }
                return bookmarkPositions;
            }
        }

        private SolutionBookmarkPositions CreateSolutionBookmarkPositions()
        {
            SolutionBookmarkPositions bookmarks = new SolutionBookmarkPositions();
            bookmarks.Positions = new List<DocumentBookmarkPositions>();
            return bookmarks;
        }

        #endregion
    }
}
