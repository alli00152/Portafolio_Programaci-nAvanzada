namespace ufide.mascotas.Models.Entities
{
    public class FabricaFelina : IFabricaMascotas
    {
        public Mascota CrearMascota(string nombre, int mesNacimiento, int anioNacimiento)
        {
            return new Gato(nombre, mesNacimiento, anioNacimiento);
        }

        public string CrearFicha()
        {
            return "Ficha felina";
        }
    }
}
