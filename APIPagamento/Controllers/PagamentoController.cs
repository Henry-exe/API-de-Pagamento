using APIPagamento.Context;
using APIPagamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly SqlContext _context;

        public PagamentoController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Pagamento
        [HttpGet]
        public async Task<IActionResult> GetAllPagamentos()
        {
            var pagamentos = await _context.Pagamentos.ToListAsync();

            return Ok(pagamentos);
        }

        // GET: api/Pagamento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPagamentoById(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return Ok(pagamento);
        }

        // POST: api/Pagamento
        [HttpPost]
        public async Task<IActionResult> CreatePagamento([FromBody] Pagamento pagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagamentoById", new { id = pagamento.PagamentoId }, pagamento);
        }

        // PUT: api/Pagamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePagamento(int id, [FromBody] Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
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

        // DELETE: api/Pagamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(int id)
        {
            return _context.Pagamentos.Any(pagamento => pagamento.PagamentoId == id);
        }
    }
}