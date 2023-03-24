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
    public class ProductsController : ControllerBase
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
        public async Task<ActionResult<Product>> GetUtilisateurByEmail(string name)
        {
            //var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(c => c.Mail == email);
            var product = await dataRepository.GetByStringAsync(name);


            if (product == null || product.Value == null) //marche pas avec changement
            {
                return NotFound();
            }

            return product;
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
            (pc, pp) => new { pc.p, pc.ColorName, pp.Link, Hexacode = pc.HexaCode })
         .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<object> GetProductPhotoColorByIdAsync(int id) {
            var productWithColorsAndPhotos = await _context.Product.Join(
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
         .Where(pc => pc.Product.ProductId == id)
         .Select(pc => new {
             pc.Product,
             pc.ColorName,
             pc.PhotoUrl,
             pc.Hexacode
         })
         .FirstOrDefaultAsync();

            if (productWithColorsAndPhotos == null) {
                return NotFound();
            }
            return productWithColorsAndPhotos;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutProduct(int id, Product product)
         {
             if (id != product.ProductId)
             {
                 return BadRequest();
             }

             _context.Entry(product).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!ProductExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }

         // POST: api/Products
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<Product>> PostProduct(Product product)
         {
             _context.Product.Add(product);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
         }

         // DELETE: api/Products/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteProduct(int id)
         {
             var product = await _context.Product.FindAsync(id);
             if (product == null)
             {
                 return NotFound();
             }

             _context.Product.Remove(product);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool ProductExists(int id)
         {
             return _context.Product.Any(e => e.ProductId == id);
         }*/
    }
}
