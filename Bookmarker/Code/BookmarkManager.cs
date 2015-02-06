using Bookmarker.Application;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bookmarker.Code.Extensions;

namespace Bookmarker.Code
{
    public class BookmarkManager
    {
        #region Constants
        private const string F2Key = "{F2}";
        private const string EnterKey = "{Enter}";
        #endregion

        #region Fields
        private BookmarkNameWindow renameWindow;
        #endregion

        #region Properties

        private EnvDTE80.DTE2 _dteObject = null;
        public EnvDTE80.DTE2 DTEObject
        {
            get { return _dteObject; }
            set { _dteObject = value; }
        }

        private IVsCommandWindow _commandWindow = null;
        public IVsCommandWindow CommandWindow
        {
            get { return _commandWindow; }
            set { _commandWindow = value; }
        }

        private TextDocument _textDocument = null;
        private TextDocument TextDocument
        {
            get { return _textDocument; }
            set { _textDocument = value; }
        }

        private TextSelection _textSelection = null;
        private TextSelection TextSelection
        {
            get { return _textSelection; }
            set { _textSelection = value; }
        }

        #endregion

        #region Constructor
        public BookmarkManager()
        {
            renameWindow = new BookmarkNameWindow();
            renameWindow.Saved += renameWindow_Saved;
        }

        #endregion

        #region Method

        public void StartRenamingProcess()
        {
            BookmarkService rep = BookmarkService.GetInstance();
            string fullName64key = this.TextDocument.Parent.FullName.Base64Encode();
            int position = this.TextSelection.TopPoint.Line;
            bool positionExists = rep.PositionExistsInDocumentPositions(fullName64key, position);
            if (positionExists)
            { 
                rep.RemoveBookmarkPosition(fullName64key, position);
                this.TextSelection.ClearBookmark();
            }
            else
            {
                rep.SaveBookmarkPosition(fullName64key, position);
                this.TextSelection.SetBookmark();
                HandleRenaming();
            }
        }

        private string GetSelectedText()
        {
            string selectedText = String.Empty;
            if (this.TextSelection != null)
            {
                selectedText = this.TextSelection.Text.Trim();
            }
            return selectedText;
        }

        private void HandleRenaming()
        {
            string selectedText = GetSelectedText();
            if (!String.IsNullOrEmpty(selectedText))
            {
                Rename(selectedText);
            }
            else
            {
                renameWindow.ShowDialog();
            }
        }

        private void Rename(string bookmarkName)
        {
            if(!String.IsNullOrEmpty(bookmarkName))
            { 
                try
                {
                    this.CommandWindow.ExecuteCommand("View.BookmarkWindow");
                    this.CommandWindow.ExecuteCommand("OtherContextMenus.BookmarkWindow.Rename");
                    System.Threading.Thread.Sleep(250);
                    SendKeys.SendWait(String.Format("{0}{1}{2}", F2Key, BookmarkNameResolver.ResolveName(bookmarkName), EnterKey));
                }
                catch { }
            }
        }

        public void Initialize()
        {
            this.TextDocument = (TextDocument)this.DTEObject.ActiveDocument.Object(String.Empty) as TextDocument;
            this.TextSelection = this.TextDocument != null ? (TextSelection)this.TextDocument.Selection as TextSelection : null;
            if(BookmarkService.GetInstance().SolutionBookmarkPositionsEmpty())
            { 
                this.TextDocument.ClearBookmarks();
            }
        }

        #endregion

        #region Events

        void renameWindow_Saved(object sender, string e)
        {
            Rename(e);
        }

        #endregion
    }
}
