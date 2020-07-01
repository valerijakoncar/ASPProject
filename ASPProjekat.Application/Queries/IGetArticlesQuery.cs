﻿using ASPProjekat.Application.DataTransfer;
using ASPProjekat.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPProjekat.Application.Queries
{
    public interface IGetArticlesQuery : IQuery<ArticleSearch, PagedResponse<ArticleDto>>
    {
    }
}
