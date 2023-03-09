using Microsoft.AspNetCore.Mvc;

namespace Practica_Elmer
{
    public interface IGenericMethods<TEntityModel> where TEntityModel : class
    {
        Task<IActionResult> Insertar(TEntityModel modelo);
        Task<IActionResult> Actualizar(TEntityModel modelo);
        Task<IActionResult> Eliminar(int id);
        Task<IActionResult> Obtener(int id);
        Task<List<TEntityModel>> ObtenerTodos();
    }
}
