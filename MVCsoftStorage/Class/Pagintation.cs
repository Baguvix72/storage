using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Class
{
    public class Pagintation
    {
        int countPost;
        int maxPage;
        int firstElementPage;
        int elementPage = ViewsSettings.elementPage;
        int paginatMax = ViewsSettings.countNumberPagin;
        List<int> pageList = new List<int>();

        public Pagintation(int? newPage)
        {
            DBContext db = new DBContext();
            countPost = db.preposts.Count();

            MaxPage = 0;    // можно указывать что угодно, значение вычисляется самостоятельно
            CurrentPage = newPage ?? 1;

            if (!(CurrentPage > 0 && CurrentPage <= maxPage))
                CurrentPage = 1;

            firstElement = 0;  // можно указывать что угодно, значение вычисляется самостоятельно
            pageNumberList = new List<int>();  // можно указывать что угодно, значение вычисляется самостоятельно
        }

        public int firstElement
        {
            get { return firstElementPage; }
            private set
            {
                firstElementPage = elementPage * CurrentPage - elementPage;
            }
        }

        public int MaxPage
        {
            get { return maxPage; }
            private set
            {
                maxPage = countPost / elementPage;
                if (countPost % elementPage != 0)
                    maxPage += 1;
            }
        }

        public int CurrentPage { get; private set; }

        public List<int> pageNumberList
        {
            get { return pageList; }
            private set
            {
                int fixLeftCount = paginatMax / 2;
                if (paginatMax % 2 != 0)
                    fixLeftCount++;

                int counter = addNumberBegin(0, 0, fixLeftCount);
                counter = addNumberEnd(counter);

                if (counter < paginatMax + 1)
                    counter = addNumberBegin(fixLeftCount, counter, paginatMax);
            }
        }

        int addNumberBegin(int module, int counter, int maxNumber)
        {
            while (CurrentPage - module > 0 && counter < maxNumber)
            {
                pageList.Insert(0, CurrentPage - module);
                module++;
                counter++;
            }
            return counter;
        }

        int addNumberEnd(int counter, int module = 1)
        {
            while (CurrentPage + module <= maxPage && counter < paginatMax)
            {
                pageList.Add(CurrentPage + module);
                module++;
                counter++;
            }
            return counter;
        }
    }
}