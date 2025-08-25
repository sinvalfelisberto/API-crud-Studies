using Microsoft.AspNetCore.Mvc;
using crudGus.Data;
using crudGus.Models;
using Microsoft.EntityFrameworkCore;

namespace crudGus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonagensController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public PersonagensController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonagem(Personagem personagem)
    {
        _appDbContext.Personagens.Add(personagem);
        await _appDbContext.SaveChangesAsync();

        return Ok(personagem);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Personagem>>> GetAllPersonagens()
    {
        var personagens = await _appDbContext.Personagens.ToListAsync();
        return Ok(personagens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Personagem>> GetPersonagemById(int id)
    {
        var personagem = await _appDbContext.Personagens.FindAsync(id);
        if (personagem is null)
            return NotFound("Não encontrado");
        return Ok(personagem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Personagem>> UpdatePersonagem(int id, Personagem updatedPersonagem)
    {
        var personagem = await _appDbContext.Personagens.FindAsync(id);
        if (personagem is null)
            return NotFound("Não encontrado");

        personagem.Nome = updatedPersonagem.Nome;
        personagem.Tipo = updatedPersonagem.Tipo;

        await _appDbContext.SaveChangesAsync();
        return Ok(personagem);
    }
}