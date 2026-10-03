namespace ufide.mascotas.Models.Entities
{
    public class FabricaReptil : IFabricaMascotas
    {
        public Mascota CrearMascota(string nombre, int mesNacimiento, int anioNacimiento)
        {
            return new Tortuga(nombre, mesNacimiento, anioNacimiento);
        }

        public string CrearFicha()
        {
            return "Ficha reptil";
        }
    }
}
