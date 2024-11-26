namespace MoviesApi.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }
        #region One2Many-Genre
        [ForeignKey(nameof(Genre))]
        public byte GenreId { get; set; }
        public Genre? Genre { get; set; }
        
        #endregion



    }
}
