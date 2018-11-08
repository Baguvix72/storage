using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Class
{
    public class ListItem
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Poster { get; set; }

    }

    public class ListContent<T>
    {
        List<T> content;
        int countPost;

        public ListContent(List<T> _content)
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

        public List<List<T>> GetCardPage(int elementsInLine)
        {
            int indexPost = 0;
            int countBlock = GetCountLine(elementsInLine);

            List<List<T>> pageBlock = new List<List<T>>();

            for (int i = 0; i < countBlock; i++)
            {
                List<T> blockPost = new List<T>();
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