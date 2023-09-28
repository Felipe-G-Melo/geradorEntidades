namespace geradorDeEntidades.Generates;
public class GenerateRepositoryFile : Utils
{
    public string Entityname { get; private set; }
    public DirectoryInfo TypeormPath { get; private set; }
    public DirectoryInfo TypeormSchemaPath { get; private set; }

    public GenerateRepositoryFile(string entityName, DirectoryInfo typeormPath, DirectoryInfo typeormSchemaPath)
    {
        Entityname = entityName;
        TypeormPath = typeormPath;
        TypeormSchemaPath = typeormSchemaPath;
    }

    public void GenerateSchemaFile()
    {
        string fileSchema = Entityname + ".schema.ts";
        string pathSchema = Path.Combine(TypeormSchemaPath.FullName, fileSchema);
        using (StreamWriter sw = File.CreateText(pathSchema))
        {
            sw.WriteLine($"import {{ EntitySchema }} from 'typeorm';");
            sw.WriteLine($"import {{ {ClearChar(Entityname)} }} from '@domain/{Entityname}/entities/{Entityname}';");
            sw.WriteLine($"\nexport const {NotClearFirstChar(Entityname)}Schema = new EntitySchema<{ClearChar(Entityname)}>({{");
            sw.WriteLine("});");
        }
    }

    public void GenerateRepository()
    {
        string fileRepository = Entityname + "-typeorm.repository.ts";
        string pathRepository = Path.Combine(TypeormPath.FullName, fileRepository);
        using (StreamWriter sw = File.CreateText(pathRepository))
        {
            sw.WriteLine($"import {{ I{ClearChar(Entityname)}Repository }}from '@domain/{Entityname}/repository/i{Entityname}.repository';");
            sw.WriteLine($"import {{CrudRepository}} from '../abstractions/crud.repository';");
            sw.WriteLine($"import {{ {ClearChar(Entityname)} }} from '@domain/{Entityname}/entities/{Entityname}';");
            sw.WriteLine($"\nexport class {ClearChar(Entityname)}TypeormRepository extends CrudRepository<{ClearChar(Entityname)}> implements I{ClearChar(Entityname)}Repository {{");
            sw.WriteLine($"   override entityName = '{ClearChar(Entityname)}';");
            sw.WriteLine("}");
        }
    }
}
