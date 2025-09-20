using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _appDbContext;

    public MovieRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<Movie> GetMovies()
    {
        return _appDbContext.Set<Movie>().AsQueryable<Movie>().ToList();
    }

    public Movie? GetMovie(Func<Movie, bool> filter)
    {
        return _appDbContext.Set<Movie>().FirstOrDefault(filter);
    }

    public void AddMovie(Movie movie)
    {
        try
        {
            _appDbContext.Set<Movie>().Add(movie);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot add the movie to database", e);
        }
    }

    public void UpdateMovie(Movie movie)
    {
        try
        {
            _appDbContext.Update(movie);
            _appDbContext.Entry(movie).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot update the movie", e);
        }
    }

    public void DeleteMovie(Movie movie)
    {
        try
        {
            _appDbContext.Set<Movie>().Remove(movie);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot delete the movie", e);
        }
    }
}