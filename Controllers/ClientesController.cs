using Microsoft.AspNetCore.Mvc;
using FIAPOracleEF.Database;
using FIAPOracleEF.Models;

namespace FIAPApi.Controllers
{
    /// <summary>
    /// Controller para operações de CRUD de clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Inicializa o controller de clientes.
        /// </summary>
        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        [HttpPost]
        public IActionResult CreateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados do cliente são inválidos.");
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        /// <summary>
        /// Retorna um cliente pelo seu ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Retorna clientes filtrados por Perfil do Investidor (usando LINQ).
        /// </summary>
        /// <param name="perfil">O perfil do investidor (ex: Conservador, Moderado, Agressivo).</param>
        [HttpGet("por-perfil/{perfil}")]
        public IActionResult GetClientesByPerfil(string perfil)
        {
            if (string.IsNullOrEmpty(perfil))
            {
                return BadRequest("O perfil de investimento é obrigatório para a pesquisa.");
            }

            var clientes = _context.Clientes
                .Where(c => c.Perfil.ToLower() == perfil.ToLower())
                .ToList();

            if (!clientes.Any())
            {
                return NotFound($"Nenhum cliente encontrado com o perfil '{perfil}'.");
            }

            return Ok(clientes);
        }

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || cliente.Id != id)
            {
                return BadRequest("Dados do cliente inválidos.");
            }

            var existingCliente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (existingCliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            existingCliente.Nome = cliente.Nome;
            existingCliente.Idade = cliente.Idade;
            existingCliente.Perfil = cliente.Perfil;
            existingCliente.LiquidezDisponivel = cliente.LiquidezDisponivel;
            existingCliente.Objetivo = cliente.Objetivo;

            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
