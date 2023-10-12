using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
	public class Foto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Il campo deve essere obbligatorio")]
		[StringLength(30, ErrorMessage = "Il titolo non può avere piu di 30 caratteri")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Inserisci una descrizione")]
		public string Description { get; set; }

        public string? Pathimg { get; set; }

        public byte[]? Imagefile { get; set; }

        public string ImagesrcFile => Imagefile is null ? (Pathimg is null ? "" : Pathimg) : $"data:image/png;base64, {Convert.ToBase64String(Imagefile)}";

        public bool Visible { get; set; }

		public List<Category>? Categories { get; set; }

		public Foto() { }

		public Foto(int id, string title, string pathimg, bool visible)
		{
			Id = id;
			Title = title;
			Pathimg = pathimg;
			Visible = visible;
		}
	}
}
