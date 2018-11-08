using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCsoftStorage.Models;

namespace MVCsoftStorage.Class
{
    public class Pagintation
    {
        private PostContext db = new PostContext();
        private int currentPage;
        private int countPost;
        private int maxPage;
        private int firstElementPage;
        private int elementPage = ViewsSettings.elementPage;
        private int paginatMax = ViewsSettings.countNumberPagin;
        private List<int> pageList = new List<int>();

        public Pagintation(int? newPage)
        {
            countPost = db.preposts.Count();
            setMaxPage();
            currentPage = newPage ?? 1;
            checkPage();
            setFirstElement();
            setListPage();
        }

        public int firstElement
        {
            get { return firstElementPage; }
        }

        private void setFirstElement()
        {
            firstElementPage = elementPage * currentPage - elementPage;
        }

        private void setMaxPage()
        {
            maxPage = countPost / elementPage;
            if (countPost % elementPage != 0)
                maxPage += 1;
        }

        public int MaxPage
        {
            get { return maxPage; }
        }

        public int CurrentPage
        {
            get { return currentPage; }
        }

        private void setListPage()
        {
            int fixLeftCount = paginatMax / 2;
            if (paginatMax % 2 != 0)
                fixLeftCount++;

            int counter = addNumberBegin(0, 0, fixLeftCount);
            counter = addNumberEnd(counter);

            if (counter < paginatMax + 1)
                counter = addNumberBegin(fixLeftCount, counter, paginatMax);
        }

        private int addNumberBegin(int module, int counter, int maxNumber)
        {
            while (currentPage - module > 0 && counter < maxNumber)
            {
                pageList.Insert(0, currentPage - module);
                module++;
                counter++;
            }
            return counter;
        }

        private int addNumberEnd(int counter, int module = 1)
        {
            while (currentPage + module <= maxPage && counter < paginatMax)
            {
                pageList.Add(currentPage + module);
                module++;
                counter++;
            }
            return counter;
        }

        public List<int> pageNumberList
        {
            get { return pageList; }
        }

        private void checkPage()
        {
            if (!(currentPage > 0 && currentPage <= maxPage))
            {
                currentPage = 1;
            }
        }
    }
}