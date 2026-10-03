namespace ufide.mascotas.Models.Entities
{
    public class FabricaCanina : IFabricaMascotas
    {
        public Mascota CrearMascota(string nombre, int mesNacimiento, int anioNacimiento)
        {
            return new Perro(nombre, mesNacimiento, anioNacimiento);
        }

        public string CrearFicha()
        {
            return "Ficha canina";
        }
    }
}
