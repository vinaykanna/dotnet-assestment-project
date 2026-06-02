using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApi.DTOs;
using NewsApi.Services;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/favourite-articles")]
public class FavouriteArticlesController(IFavouriteArticlesService favouriteArticlesService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFavourites()
    {
        var favourites = await favouriteArticlesService.GetFavourites();
        return Ok(favourites);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateFavourite(FavouriteArticleDto favouriteArticleDto)
    {
        var userId = User.GetUserId();
        var favourite = await favouriteArticlesService.CreateFavourite(favouriteArticleDto, userId);
        return Ok(favourite);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteFavourite(Guid id)
    {
        var userId = User.GetUserId();
        await favouriteArticlesService.DeleteFavourite(id, userId);
        return NoContent();
    }
}