namespace MoviesApi.Data.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        #region Many2One-Movie
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>(); 
        #endregion
    }
}
