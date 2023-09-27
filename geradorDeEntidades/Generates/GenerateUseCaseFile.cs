namespace geradorDeEntidades.Generates;
public class GenerateUseCaseFile
{
    public string EntityName { get; private set; }
    public DirectoryInfo UseCasePath { get; private set; }

    public GenerateUseCaseFile(
        string entityName,
        DirectoryInfo useCasePath)
    {
        EntityName = entityName;
        UseCasePath = useCasePath;
    }

    public void CreateUseCaseFile()
    {
        string useCaseName = $"create-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);
        using(StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)}Input }} from '@domain/{EntityName}/input/{EntityName}-input';");
            sw.WriteLine($"import {{ I{FirstCharToUpper(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}-repository';");
            sw.WriteLine($"import {{ ICreateExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Create{FirstCharToUpper(EntityName)} implements ICreateExecute<{FirstCharToUpper(EntityName)}Input, {FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {EntityName}Repository: I{FirstCharToUpper(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(input: {FirstCharToUpper(EntityName)}Input): Promise<{FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"         const {EntityName} = {FirstCharToUpper(EntityName)}.Create(input);");
            sw.WriteLine($"         return await this.{EntityName}Repository.Create({EntityName});");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void UpdatUseCaseFile()
    {
        string useCaseName = $"update-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);
        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)}Input }} from '@domain/{EntityName}/input/{EntityName}-input';");
            sw.WriteLine($"import {{ I{FirstCharToUpper(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}-repository';");
            sw.WriteLine($"import {{ IUpdateExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Update{FirstCharToUpper(EntityName)} implements IUpdateExecute<{FirstCharToUpper(EntityName)}Input, {FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {EntityName}Repository: I{FirstCharToUpper(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(input: {FirstCharToUpper(EntityName)}Input, id:number): Promise<{FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"         await this.{EntityName}Repository.GetById(id);");
            sw.WriteLine($"         const {EntityName} = {FirstCharToUpper(EntityName)}.Create(input, id);");
            sw.WriteLine($"         return await this.{EntityName}Repository.Update({EntityName});");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void GetAllUseCaseFile()
    {
        string useCaseName = $"get-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ FilterObject, OrderObject }} from '@domain/basic/irepository';");
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ I{FirstCharToUpper(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}-repository';");
            sw.WriteLine($"import {{ IGetAllExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Get{FirstCharToUpper(EntityName)} implements IGetAllExecute<{FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {EntityName}Repository: I{FirstCharToUpper(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(page: number, recordsPerPage: number, filter?: FilterObject, order?: OrderObject): Promise<{{total: number; data: {FirstCharToUpper(EntityName)}[] }}> {{");
            sw.WriteLine($"         return await this.{EntityName}Repository.GetAll(page, recordsPerPage, filter, order);");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void GetByIdUseCaseFile()
    {
        string useCaseName = $"get-{EntityName}-by-id.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {FirstCharToUpper(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ I{FirstCharToUpper(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}-repository';");
            sw.WriteLine($"import {{ IGetByIdExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Get{FirstCharToUpper(EntityName)}ById implements IGetByIdExecute<{FirstCharToUpper(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {EntityName}Repository: I{FirstCharToUpper(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(id:number): Promise<{{total: number; data: {FirstCharToUpper(EntityName)}[] }}> {{");
            sw.WriteLine($"         return await this.{EntityName}Repository.GetById(id);");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void DeleteUseCaseFile()
    {
        string useCaseName = $"delete-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ I{FirstCharToUpper(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}-repository';");
            sw.WriteLine($"import {{ IDeleteExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Get{FirstCharToUpper(EntityName)}ById implements IDeleteExecute {{");
            sw.WriteLine($"  constructor(private readonly {EntityName}Repository: I{FirstCharToUpper(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(id:number): Promise<void> {{");
            sw.WriteLine($"         await this.{EntityName}Repository.GetById(id);");
            sw.WriteLine($"         await this.{EntityName}Repository.Remove(id);");
            sw.WriteLine("  }");
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
