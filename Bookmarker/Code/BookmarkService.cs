using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookmarker.Code.Serializer;

namespace Bookmarker.Code
{
    public class BookmarkService
    {
        #region Properties

        //string = base64encoding
        private SolutionBookmarkPositions _bookmarkPositions = null;
        private SolutionBookmarkPositions BookmarkPositions
        {
            get
            {
                return _bookmarkPositions ?? (_bookmarkPositions = BookmarkStorage.GetInstance().BookmarkPositions);
            }
        }
        
        #endregion

        #region Constructor

        protected BookmarkService()
        { }

        #endregion

        #region Instance

        public static BookmarkService GetInstance()
        {
            return new BookmarkService();
        }

        #endregion

        #region Methods

        public void SaveBookmarkPosition(string key, int position)
        {
            if (DocumentPositionsExist(key))
            {
                UpdateDocumentPositions(key, position);
            }
            else
            { 
                InsertDocumentPositions(key, position);
            }
        }

        public void RemoveBookmarkPosition(string key, int position)
        {
            DocumentBookmarkPositions documentPositions = GetDocumentPositions(key);
            documentPositions.BookmarkPositions.Remove(position);
            BookmarkStorage.GetInstance().WritePositions(this.BookmarkPositions);
        }

        public DocumentBookmarkPositions GetDocumentPositions(string key)
        {
            return BookmarkPositions.Positions.FirstOrDefault(b => b.DocumentPath.Equals(key));
        }

        public void InsertDocumentPositions(string key, int position)
        {
            if (!String.IsNullOrEmpty(key) && position > 0)
            {
                DocumentBookmarkPositions documentPositions = new DocumentBookmarkPositions();
                documentPositions.DocumentPath = key;
                documentPositions.BookmarkPositions = new List<int>(){ position };
                this.BookmarkPositions.Positions.Add(documentPositions);
                BookmarkStorage.GetInstance().WritePositions(this.BookmarkPositions);
            }
        }
        
        public void UpdateDocumentPositions(string key, int position)
        {
            if(!String.IsNullOrEmpty(key) && position >= 0)
            {
                DocumentBookmarkPositions documentPositions = GetDocumentPositions(key);
                if(documentPositions != null && documentPositions.BookmarkPositions != null && !documentPositions.BookmarkPositions.Contains(position))
                { 
                    documentPositions.BookmarkPositions.Add(position);
                    BookmarkStorage.GetInstance().WritePositions(this.BookmarkPositions);
                }
            }
        }

        public bool DocumentPositionsExist(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                return this.BookmarkPositions.Positions.FindIndex(i => i.DocumentPath.Equals(key)) >= 0;
            }
            return false;
        }

        public bool PositionExistsInDocumentPositions(string key, int position)
        {
            bool exist = false;
            if(!String.IsNullOrEmpty(key))
            {
                DocumentBookmarkPositions documentPositions = GetDocumentPositions(key);
                exist = documentPositions != null && documentPositions.BookmarkPositions.Contains(position);
            }
            return exist;
        }

        public bool SolutionBookmarkPositionsEmpty()
        {
            return !this.BookmarkPositions.Positions.Any();
        }

        #endregion
    }
}
