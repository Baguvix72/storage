using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCsoftStorage.CustomClass;

namespace MVCsoftStorage.Models
{
    public class PagintationModel
    {
        public PagintationModel(int? newPage, int countPost)
        {
            setMaxPage(countPost);

            CurrentPage = newPage ?? 1;

            if (!(CurrentPage > 0 && CurrentPage <= MaxPage))
                CurrentPage = 1;

            setFirstElement();

            fillPaginator();
        }

        #region Public Prop
        public int FirstElement { get; private set; }
        public int MaxPage { get; private set; }
        public int CurrentPage { get; private set; }
        public List<int> PaginatorTable { get; private set; } = new List<int>();
        public string Category { get; set; } = null;
        public string Controller { get; set; } = "Default";
        #endregion

        #region Private Method
        void setMaxPage(int countPost)
        {
            MaxPage = countPost / ViewsSettings.elementPage;
            if (countPost % ViewsSettings.elementPage != 0)
                MaxPage += 1;
        }

        void setFirstElement()
        {
            FirstElement = ViewsSettings.elementPage * CurrentPage - ViewsSettings.elementPage;
        }

        void fillPaginator()
        {
            int fixLeftCount = ViewsSettings.MaxNumberPagin / 2;
            if (ViewsSettings.MaxNumberPagin % 2 != 0)
                fixLeftCount++;

            int counter = addNumberBegin(0, 0, fixLeftCount);
            counter = addNumberEnd(counter);

            if (counter < ViewsSettings.MaxNumberPagin + 1)
                counter = addNumberBegin(fixLeftCount, counter, ViewsSettings.MaxNumberPagin);
        }

        int addNumberBegin(int module, int counter, int maxNumber)
        {
            while (CurrentPage - module > 0 && counter < maxNumber)
            {
                PaginatorTable.Insert(0, CurrentPage - module);
                module++;
                counter++;
            }
            return counter;
        }

        int addNumberEnd(int counter, int module = 1)
        {
            while (CurrentPage + module <= MaxPage && counter < ViewsSettings.MaxNumberPagin)
            {
                PaginatorTable.Add(CurrentPage + module);
                module++;
                counter++;
            }
            return counter;
        }
        #endregion
    }
}