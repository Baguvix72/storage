using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCsoftStorage.Models;

namespace MVCsoftStorage.Class
{
    public class ListContent
    {
        private List<prePost> content;
        private int countPost;

        public ListContent(List<prePost> _content)
        {
            content = _content;
            countPost = _content.Count();
        }

        private int GetCountLine(int elementsInLine)
        {
            int countLine = countPost / elementsInLine;
            if (countPost % elementsInLine != 0)
                countLine += 1;
            return countLine;
        }

        public List<List<prePost>> GetCardPage(int elementsInLine)
        {
            int indexPost = 0;
            int countBlock = GetCountLine(elementsInLine);

            List<List<prePost>> pageBlock = new List<List<prePost>>();

            for (int i = 0; i < countBlock; i++)
            {
                List<prePost> blockPost = new List<prePost>();
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