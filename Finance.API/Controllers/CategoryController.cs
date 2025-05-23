﻿using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertCategoryRequestVM request)
        {
            try
            {
                bool result = await _service.Upsert(request);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _service.GetById(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
