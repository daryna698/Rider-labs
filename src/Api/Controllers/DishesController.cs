using Api.Dtos;
using Application.Dishes.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("dishes")]
[ApiController]
public class DishesController(IDishService dishService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DishDto>>> GetDishes(CancellationToken cancellationToken)
    {
        var dishes = await dishService.GetDishes(cancellationToken);
        return Ok(dishes.Select(DishDto.FromDomainModel).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DishDto>> GetDish(Guid id, CancellationToken cancellationToken)
    {
        var dish = await dishService.GetDish(id, cancellationToken);
        if (dish is null) return NotFound();

        return Ok(DishDto.FromDomainModel(dish));
    }

    [HttpPost]
    public async Task<ActionResult<DishDto>> CreateDish([FromBody] CreateDishDto request, CancellationToken cancellationToken)
    {
        try
        {
            var dish = await dishService.Add(request.Name, request.Price, request.WeightGrams, request.IsVegetarian, request.Category, cancellationToken);
            var dto = DishDto.FromDomainModel(dish);
            return CreatedAtAction(nameof(GetDish), new { id = dto.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return Conflict(ex.Message); 
        }
    }

    [Route("{id:guid}")]
    [HttpPut]
    public async Task<ActionResult<DishDto>> UpdateDish(Guid id, [FromBody] UpdateDishDto request, CancellationToken cancellationToken)
    {
        try
        {
            var dish = await dishService.Update(id, request.Name, request.Price, request.WeightGrams, request.IsVegetarian, request.Category, cancellationToken);
            if (dish is null) return NotFound();

            return Ok(DishDto.FromDomainModel(dish));
        }
        catch (ArgumentException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [Route("{id:guid}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteDish(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await dishService.Delete(id, cancellationToken);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
