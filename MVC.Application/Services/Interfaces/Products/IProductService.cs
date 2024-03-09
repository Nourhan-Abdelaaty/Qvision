using MVC.Domain.Helper;
using MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Application.Services.Interfaces.Products
{
    public interface IProductService
    {
        Task<ResponseResult> Create(Product model);
        Task<ResponseResult> Update(Product model);
        Task<ResponseResult> Delete(int id);
        Task<ResponseResult> GetById(int id);
        Task<ResponseResult> GetAll();
        Task<ResponseResult> PagenationData(int page, int pageSize);
        Task<int> CountData();
   }
}
