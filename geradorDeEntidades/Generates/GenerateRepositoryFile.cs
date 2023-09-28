namespace geradorDeEntidades.Generates;
public class GenerateRepositoryFile
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
            sw.WriteLine($"import {{ {FirstCharToUpper(Entityname)} }} from '@domain/{Entityname}/entities/{Entityname}';");
            sw.WriteLine($"export const {Entityname}Schema = new EntitySchema<{FirstCharToUpper(Entityname)}>({{");
            sw.WriteLine("}");
        }
    }

    public void GenerateRepository()
    {
        string fileRepository = Entityname + "-typeorm.repository.ts";
        string pathRepository = Path.Combine(TypeormPath.FullName, fileRepository);
        using (StreamWriter sw = File.CreateText(pathRepository))
        {
            sw.WriteLine($"import {{ I{FirstCharToUpper(Entityname)} }}from '@domain/{Entityname}/repository/i{Entityname}.repository';");
            sw.WriteLine($"import {{CrudRepository}} from '../abstractions/crud.repository';");
            sw.WriteLine($"import {{ {FirstCharToUpper(Entityname)} }} from '@domain/{Entityname}/entities/{Entityname}';");
            sw.WriteLine($"export class {FirstCharToUpper(Entityname)}TypeormRepository extends CrudRepository<{FirstCharToUpper(Entityname)}> implements I{Entityname}Repository {{");
            sw.WriteLine($"   override entityName = '{FirstCharToUpper(Entityname)}';");
            sw.WriteLine("}");
        }
    }

    string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Insira um nome válido");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}
