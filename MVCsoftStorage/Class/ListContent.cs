using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Class
{
    public class ListContent
    {
        List<preposts> content;
        int countPost;

        public ListContent(List<preposts> _content)
        {
            content = _content;
            countPost = _content.Count();
        }

        int GetCountLine(int elementsInLine)
        {
            int countLine = countPost / elementsInLine;
            if (countPost % elementsInLine != 0)
                countLine += 1;
            return countLine;
        }

        public List<List<preposts>> GetCardPage(int elementsInLine)
        {
            int indexPost = 0;
            int countBlock = GetCountLine(elementsInLine);

            List<List<preposts>> pageBlock = new List<List<preposts>>();

            for (int i = 0; i < countBlock; i++)
            {
                List<preposts> blockPost = new List<preposts>();
                for (int j = 0; j < elementsInLine; j++)
                {
                    if (indexPost == countPost)
                        break;
                    blockPost.Add(content.ElementAt(indexPost));
                    indexPost++;
                }
                pageBlock.Add(blockPost);
            }
            return pageBlock;
        }
    }
}