using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace physical_persons_api.Helpers
{
    public static class HttpContextExstensions
    {
        public async static Task InsertParametersPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if(httpContext ==null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString());
        }
    }
}
