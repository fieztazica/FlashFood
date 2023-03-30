using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class PagingParameterModel
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 2;

        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public class PagingResult
    {
        public int totalCount;
        public int pageSize;
        public int currentPage;
        public int totalPages;
        public bool previousPage;
        public bool nextPage;
        public List<MealViewModel> items;
    }
}