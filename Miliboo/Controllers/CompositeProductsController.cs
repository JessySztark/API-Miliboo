﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompositeProductsController : ControllerBase
    {
        private readonly IDataRepository<CompositeProduct> _repository;

        public CompositeProductsController(IDataRepository<CompositeProduct> dataRepo)
        {
            _repository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompositeProduct>>> GetCompositeProduct()
        {
            return await _repository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CompositeProduct>> GetCompositeProductByID(int id)
        {
            var compositeProduct = await _repository.GetByIdAsync(id);

            if (compositeProduct == null || compositeProduct.Value == null)
            {
                return NotFound();
            }

            return compositeProduct;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompositeProduct(int id, CompositeProduct objt)
        {
            if (id != objt.CompositeID)
            {
                return BadRequest();
            }

            var objToUpdate = await _repository.GetByIdAsync(id);

            if (objToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CompositeProduct>> PostCompositeProduct(CompositeProduct obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(obj);

            return CreatedAtAction("GetCompositeProductById", new { id = obj.CompositeID }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompositeProduct(int id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
