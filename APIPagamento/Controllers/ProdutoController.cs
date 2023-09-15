using APIPagamento.Context;
using APIPagamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProdutoController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Produto
        [HttpGet]
        public async Task<IActionResult> GetAllProdutos()
        {
            var produtos = await _context.produtos.ToListAsync();

            return Ok(produtos);
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            var produto = await _context.produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // POST: api/Produto
        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoById", new { id = produto.ProdutoId }, produto);
        }

        // PUT: api/Produto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            _context.produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.produtos.Any(produto => produto.ProdutoId == id);
        }
    }
}
