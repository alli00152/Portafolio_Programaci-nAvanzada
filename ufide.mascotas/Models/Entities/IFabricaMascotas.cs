namespace ufide.mascotas.Models.Entities
{
    // Abstract Factory: cada familia (canina, felina, reptil) crea
    // la mascota y la ficha que le corresponde.
    public interface IFabricaMascotas
    {
        Mascota CrearMascota(string nombre, int mesNacimiento, int anioNacimiento);
        string CrearFicha();
    }
}
