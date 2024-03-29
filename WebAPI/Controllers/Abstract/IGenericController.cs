﻿using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Abstract
{
    public interface IGenericController<T> where T : class, IEntity, new()
    {
        Task<IActionResult> Add(T t);
        Task<IActionResult> Remove(T t);
        Task<IActionResult> Update(T t);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> RemoveById(int id);
    }
}
