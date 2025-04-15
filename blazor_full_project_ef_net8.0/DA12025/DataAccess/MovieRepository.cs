using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MovieRepository
{
    protected readonly AppDbContext _appDbContext;

    public MovieRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IList<Movie> GetAll()
    {
        return _appDbContext.Set<Movie>().AsQueryable<Movie>().ToList();
    }

    public Movie? Get(Func<Movie, bool> filter)
    {
        return _appDbContext.Set<Movie>().FirstOrDefault(filter);
    }

    public void Add(Movie movie)
    {
        try
        {
            _appDbContext.Set<Movie>().Add(movie);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }

    public void Delete(Movie movie)
    {
        try
        {
            _appDbContext.Set<Movie>().Remove(movie);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }

    public void Update(Movie movie)
    {
        try
        {
            _appDbContext.Update(movie);
            _appDbContext.Entry<Movie>(movie).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }
}