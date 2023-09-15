using APIPagamento.Context;
using APIPagamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APIPagamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly SqlContext _context;

        public PedidoController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<IActionResult> GetAllPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(pedido => pedido.PedidoProdutos) 
                .Include(pedido => pedido.Pagamento) 
                .ToListAsync();

            return Ok(pedidos);
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            var pedido = await _context.Pedidos
                .Include(pedido => pedido.PedidoProdutos)
                .Include(pedido => pedido.Pagamento)
                .FirstOrDefaultAsync(pedido => pedido.IdPedido == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoById", new { id = pedido.IdPedido }, pedido);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(pedido => pedido.IdPedido == id);
        }
    }
}
