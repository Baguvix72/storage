using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Class
{
    public static class ViewsSettings
    {
        public static int elementPage = 18;      //Общее количество карточек с постами на странице списка постов
        public static int elementLine = 3;       //Количество карточек с постами на одной строке
        public static int countNumberPagin = 7;  //Количество показываемых страниц в пагинаторе, лучше нечетное число
    }
}