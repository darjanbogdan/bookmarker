using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarker.Code
{
    public static class BookmarkNameResolver
    {
        #region Fields
        //These characters have special meaning  to the SendKeys class
        private static string[] specialCharacters = new string[] { "+", "^", "%", "~", "(", ")", "{", "}" };
        private static string[] forbiddenCharacters = new string[] { "[", "]" };
        #endregion

        #region Methods

        public static string ResolveName(string name)
        {
            List<string> resolvedName = new List<string>();
            foreach (char character in name.ToCharArray())
            {
                resolvedName.Add(GetValidCharacterAsString(character));
            }
            return String.Join("", resolvedName.ToArray());
        }

        private static string GetValidCharacterAsString(char character)
        {
            string validCharacter = character.ToString();
            if (specialCharacters.Contains(validCharacter))
            {
                validCharacter = String.Format("{{{0}}}", validCharacter);
            }
            else if (forbiddenCharacters.Contains(validCharacter))
            {
                //We don't want to have "]" and "[" due to problems with theirs escaping and passing to the SendKeys.SendWait method
                validCharacter = String.Empty;
            }
            return validCharacter;
        }

        #endregion
    }
}
