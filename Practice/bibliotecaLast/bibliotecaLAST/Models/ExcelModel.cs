namespace bibliotecaLAST.Models
{
  
        public class ExcelModel
        {
            public string ID { get; set; }
            public string Nombre { get; set; }
            public string Accion { get; set; }
            public string Libro { get; set; }

            public ExcelModel()
            {
            }

            public ExcelModel(string id, string nombre, string accion, string libro)
            {
                ID = id;
                Nombre = nombre;
                Accion = accion;
                Libro = libro;
            }

            public override string ToString()
            {
                return $"ID: {ID}, Nombre: {Nombre}, Acción: {Accion}, Libro: {Libro}";
            }
        }
    
}
