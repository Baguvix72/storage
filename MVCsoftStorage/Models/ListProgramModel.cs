using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCsoftStorage.CustomClass;

namespace MVCsoftStorage.Models
{
    public class ListProgramModel
    {
        public ListProgramModel(List<ProgramModel> content, PagintationModel paginat)
        {
            MakeList(content);
            Pagin = paginat;
        }

        public List<List<ProgramModel>> Get { get; private set; } = new List<List<ProgramModel>>();
        public PagintationModel Pagin { get; private set; }

        int GetCountLine(int count)
        {
            int countLine = count / ViewsSettings.elementLine;
            if (count % ViewsSettings.elementLine != 0)
                countLine += 1;
            return countLine;
        }

        void MakeList(List<ProgramModel> content)
        {
            int countBlock = GetCountLine(content.Count());
            int indexPost = 0;

            for (int i = 0; i < countBlock; i++)
            {
                List<ProgramModel> blockPost = new List<ProgramModel>();
                for (int j = 0; j < ViewsSettings.elementLine; j++)
                {
                    if (indexPost == content.Count())
                        break;
                    blockPost.Add(content.ElementAt(indexPost));
                    indexPost++;
                }
                Get.Add(blockPost);
            }
        }
    }
}