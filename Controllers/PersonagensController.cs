using Microsoft.AspNetCore.Mvc;
using crudGus.Data;
using crudGus.Models;
using Microsoft.EntityFrameworkCore;
using crudGus.Models.DTOs;

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
    public async Task<IActionResult> AddPersonagem([FromBody] CreatedPersonagemDTO personagemDTO)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var personagem = new Personagem
        {
            Nome = personagemDTO.Nome,
            Tipo = personagemDTO.Tipo
        };

        _appDbContext.Personagens.Add(personagem);
        await _appDbContext.SaveChangesAsync();

        return Created("Personagem criado com sucesso", personagem);
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
    public async Task<ActionResult<Personagem>> UpdatePersonagem(int id, [FromBody] UpdatedPersonagemDTO updatedPersonagem)
    {
        var personagem = await _appDbContext.Personagens.FindAsync(id);
        if (personagem is null)
            return NotFound("Personagem não encontrado");

        //_appDbContext.Entry(personagem).CurrentValues.SetValues(updatedPersonagem);
        if (updatedPersonagem.Nome is not null)
            personagem.Nome = updatedPersonagem.Nome;
        if (updatedPersonagem.Tipo is not null)
            personagem.Tipo = updatedPersonagem.Tipo;

        await _appDbContext.SaveChangesAsync();

        return Ok(personagem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonagem(int id)
    {
        var personagem = await _appDbContext.Personagens.FindAsync(id);
        if (personagem is null)
            return NotFound("Não encontrado");

        _appDbContext.Personagens.Remove(personagem);
        await _appDbContext.SaveChangesAsync();
        return StatusCode(200, "Deletado com sucesso");
    }
}