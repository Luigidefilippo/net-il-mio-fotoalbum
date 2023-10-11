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

		[Required(ErrorMessage = "Campo obbligatorio")]
		public string Pathimg { get; set; }

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
