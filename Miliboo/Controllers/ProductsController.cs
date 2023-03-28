using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public partial class ProductsController : ControllerBase
    {
        private readonly IDataRepository<Product> dataRepository;
        private readonly MilibooDBContext _context;

        public ProductsController(IDataRepository<Product> dataRepo, MilibooDBContext context) {
            dataRepository = dataRepo;
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await dataRepository.GetByIdAsync(id);

            if (product == null || product.Value == null)  //marche pas sinon 
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("{name}")]
        [ActionName("GetProductByName")]
        public async Task<object> GetUtilisateurByEmail(string name)
        {
            var products = await _context.Product
                .Where(p => p.ProductName.ToLower().Contains(name.ToLower()))
                .Join(
         _context.Colors,
         p => p.ColorsNavigation.ColorId,
         c => c.ColorId,
         (p, c) => new {
             p,
             ColorName = c.ColorName,
             HexaCode = c.ColorHexaCode
         }).Join(
            _context.Photos,
            pc => pc.p.ProductId,
            pp => pp.ProductPhoto.ProductId,
            (pc, pp) => new {
                pc.p,
                Join = new
                {
                    colorName = pc.ColorName,
                    link = pp.Link,
                    hexacode = pc.HexaCode
                }
            })
                .ToListAsync();

            if (products.Count == 0)
            {
                return NotFound();
            }
            
            return products;
        }

        [HttpGet]
        public async Task<object> GetProductsWithColorAndPhoto() {
            return await _context.Product.Join(
         _context.Colors,
         p => p.ColorsNavigation.ColorId,
         c => c.ColorId,
         (p, c) => new {
             p,
             ColorName = c.ColorName,
             HexaCode = c.ColorHexaCode
         }).Join(
            _context.Photos,
            pc => pc.p.ProductId,
            pp => pp.ProductPhoto.ProductId,
            (pc, pp) => new {
                pc.p,
                Join = new {
                    colorName = pc.ColorName,
                    link = pp.Link,
                    hexacode = pc.HexaCode
                }
            })
         .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<object> GetProductPhotoColorByIdAsync(int id) {
            var productWithColorsAndPhotos = await _context.Product
        .Join(
            _context.Colors,
            p => p.ColorsNavigation.ColorId,
            c => c.ColorId,
            (p, c) => new {
                Product = p,
                ColorName = c.ColorName,
                HexaCode = c.ColorHexaCode
            })
        .Join(
            _context.Photos,
            pc => pc.Product.ProductId,
            pp => pp.ProductPhoto.ProductId,
            (pc, pp) => new {
                Product = pc.Product,
                ColorName = pc.ColorName,
                PhotoUrl = pp.Link,
                Hexacode = pc.HexaCode
            })
        .Join(
            _context.ProductType,
            p => p.Product.ProductTypesNavigation.ProductTypeId,
            pt => pt.ProductTypeId,
            (p, pt) => new {
                Product = p.Product,
                ColorName = p.ColorName,
                PhotoUrl = p.PhotoUrl,
                Hexacode = p.Hexacode,
                TypeName = pt.ProductTypeName,
                ProductTypeId = pt.ProductTypeId
            })
        .Where(pc => pc.Product.ProductId == id)
        .Select(pc => new {
            pc.Product,
            Join = new {
                colorName = pc.ColorName,
                link = pc.PhotoUrl,
                hexacode = pc.Hexacode,
                typeName = pc.TypeName,
                productTypeId = pc.ProductTypeId
            }
        })
        .FirstOrDefaultAsync();

            if (productWithColorsAndPhotos == null) {
                return NotFound();
            }

            return Ok(productWithColorsAndPhotos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product objt) {
            if (id != objt.ProductId) {
                return BadRequest();
            }

            var objToUpdate = await dataRepository.GetByIdAsync(id);

            if (objToUpdate == null) {
                return NotFound();
            }
            else {
                await dataRepository.UpdateAsync(objToUpdate.Value, objt);
                return Ok(objt);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product obj) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(obj);

            return CreatedAtAction("GetProductById", new { id = obj.ProductId }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) {
            var obj = await dataRepository.GetByIdAsync(id);
            if (obj == null) {
                return NotFound();
            }
            await dataRepository.DeleteAsync(obj.Value);
            return Ok(obj);
        }
    }
}
