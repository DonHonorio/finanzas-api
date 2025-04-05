using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzasApp.Data;       // Ajusta el namespace según tu proyecto

namespace FinanzasApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly FinanzasContext _context;

        // Inyectamos el DbContext a través del constructor
        public TransactionsController(FinanzasContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            // Recupera los primeros 5 registros de la tabla Transactions
            var transactions = await _context.Transactions
                .Take(5)
                .ToListAsync();

            return Ok(transactions);
        }
    }
}
