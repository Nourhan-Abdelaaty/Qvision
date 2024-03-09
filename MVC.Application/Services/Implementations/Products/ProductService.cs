using Application.Services.Interfaces;
using MVC.Application.Services.Interfaces.Products;
using MVC.Domain.Helper;
using MVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Application.Services.Implementations.Products;
    public class ProductService : IProductService
{
    private readonly IUnitOfWork UnitOFWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        UnitOFWork = unitOfWork;
    }
    public async Task<ResponseResult> Create(Product model)
    {
        try
        {
            if (model == null)
                return new ResponseResult { IsSuccess = false, Message = "SendNull" };

            if (model.Name == null || model.Name.Trim() == "")
                return new ResponseResult { IsSuccess = false, Message = "Name is required" };

            if (model.ExpirationDate < DateTime.Now)
                return new ResponseResult { IsSuccess = false, Message = "Expiration date must be in the future" };

            await UnitOFWork.Repository<Product>().AddAsync(model);
            await UnitOFWork.CompleteAsync();
            return new ResponseResult
            {
                IsSuccess = true,
                Message = "AddSuccess",
                Obj = new { Id = model.Id, Name = model.Name }
            };
        }
        catch(Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
        }
    }
    public async Task<ResponseResult> Update(Product model)
    {
        try
        {
            #region Valdation
            if (model == null)
                return new ResponseResult { IsSuccess = false, Message = "SendNull" };

            if (model.Id <= 0)
                return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };

            if (model.Name == null || model.Name.Trim() == "")
                return new ResponseResult { IsSuccess = false, Message = "Name is required" };

            if (model.ExpirationDate < DateTime.Now)
                return new ResponseResult { IsSuccess = false, Message = "Expiration date must be in the future" };
            #endregion

            #region Modify
            model.LastModifiedDate = DateTime.Now;
            #endregion
            var i = await UnitOFWork.Repository<Product>().UpdateAsync(model);
            var x = await UnitOFWork.CompleteAsync();

            return new ResponseResult
            {
                IsSuccess = true,
                Message = "UpdateSuccess",
                Obj =
                new
                {
                    Id = model.Id,
                    NameAr = model.Name,
                }
            };
        }
        catch(Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
            throw;
        }
    }
    public async Task<ResponseResult> Delete(int id)
    {
        try
        {
            if (id <= 0)
                return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };
            var model = UnitOFWork.Repository<Product>().FirstOrDefault(e => e.Id == id);
            if (model == null)
                return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };

            await UnitOFWork.Repository<Product>().DeleteAsync(id);
            await UnitOFWork.CompleteAsync();
            return new ResponseResult { IsSuccess = true, Message = "DeleteSuccess", Obj = model };
        }
        catch(Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
        }

    }
    public async Task<ResponseResult> GetById(int id)
    {
        if (id <= 0)
            return new ResponseResult { IsSuccess = false, Message = "IdRequierd" };
        try
        {
            var model = UnitOFWork.Repository<Product>()
               .FirstOrDefault(e => e.Id == id);
            if (model == null)
                return new ResponseResult { IsSuccess = false, Message = "NotFound", Obj = null };

            return new ResponseResult { IsSuccess = true, Message = "Success", Obj = model };
        }
        catch (Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
            throw;
        }
    }
    public async Task<ResponseResult> GetAll()
    {
        try
        {
            var list = await UnitOFWork.Repository<Product>().ToListAsync();
            return new ResponseResult { IsSuccess = true, Message = "Success", Obj = list};
        }
        catch (Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
            throw;
        }
    } 
    public async Task<ResponseResult> PagenationData(int page , int pageSize )
    {
        try
        {
            var list = await UnitOFWork.Repository<Product>().PagenationData(page ,pageSize);
            return new ResponseResult { IsSuccess = true, Message = "Success", Obj = list.Obj};
        }
        catch (Exception ex)
        {
            return new ResponseResult { IsSuccess = false, Message = ex.Message };
            throw;
        }
    }
    public async Task<int> CountData()
    {
        try
        {
            var Count = await UnitOFWork.Repository<Product>().CountAsync();
            return Count ;
        }
        catch (Exception ex)
        {
            return 0;
            throw;
        }
    }

}

